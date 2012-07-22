using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ServerChecker2012
{
    public partial class MainWindow : Form
    {
        EditForm EditForm = new EditForm();
        public MainWindow()
        {
            InitializeComponent();
        }
        List<ServerData> servers;
        public void LoadServers(ref List<ServerData> servers)
        {
            this.servers = servers;
            foreach (ServerData data in servers)
            {
                var server = new ServerListViewItem(data);
                server.ServerUpdated += new EventHandler(ServerList_ServerUpdated);
                ServerList.Items.Add(server);
            }
        }

        private void UpdateButtons()
		{
			if (InvokeRequired)
			{
				Invoke(new Action(UpdateButtons));
				return;
			}
			bool btn_start = false, btn_stop = false;
			bool qry_enable = false, qry_disable = false;
			foreach (ServerListViewItem row in ServerList.SelectedItems)
			{
				if (row.Status == ProcessStatus.INACTIVE)
					btn_start = true;
				else
					btn_stop = true;
				if (row.Server.QueriesDisabled)
					qry_disable = true;
				else
					qry_enable = true;
			}
			bool no_sel = !btn_start && !btn_stop;
			bool multi_sel = ServerList.SelectedItems.Count > 1;

			ContextMenuStart.Visible = btn_start || no_sel;
			ContextMenuStart.Enabled = btn_start;

			StartButton.Enabled = !no_sel;
			if (btn_stop)
				StartButton.Text = "Restart";
			else
				StartButton.Text = "Start";

			ContextMenuStop.Visible = btn_stop;
			ContextMenuRestart.Visible = btn_stop;
			StopButton.Enabled = btn_stop;

			/*
             * Note to self:
             *  ContextMenuStart is separate from ContextMenuRestart INTENTIONALLY!
             *  They occupy different positions on the menu so just changing the label
             *   wouldn't work.
             */

			if (qry_enable && qry_disable)
				ContextMenuDisable.CheckState = CheckState.Indeterminate;
			else if (qry_disable)
				ContextMenuDisable.CheckState = CheckState.Checked;
			else
				ContextMenuDisable.CheckState = CheckState.Unchecked;

			// You can only edit a single offline server.
			EditButton.Enabled = !no_sel && !multi_sel && !btn_stop;
			ContextMenuEdit.Enabled = EditButton.Enabled;

			if (no_sel)
			{
				NewButton.Text = "New";
				NewButton.Enabled = true;
				ContextMenuDelete.Enabled = false;
				ContextMenuDisable.Enabled = false;
			}
			else
			{
				NewButton.Text = "Delete";
				// You can't delete online servers
				bool enabled = !btn_stop;
				NewButton.Enabled = enabled;
				ContextMenuDelete.Enabled = enabled;
				ContextMenuDisable.Enabled = enabled;
			}
        }

        private void ServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void ServerList_ServerUpdated(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void StartServer(object sender, EventArgs e)
        {
            foreach (ServerListViewItem data in ServerList.SelectedItems)
            {
                data.Server.Start();
            }
            UpdateButtons();
        }

        private void StopServer(object sender, EventArgs e)
        {
            foreach (ServerListViewItem data in ServerList.SelectedItems)
            {
                data.Server.Stop();
            }
            UpdateButtons();
        }

        private void EditServer(object sender, EventArgs e)
        {
			var items = ServerList.SelectedItems;
			if (items.Count != 1)
				return;
			ServerData server = ((ServerListViewItem) items[0]).Server;
            EditForm.PrepareEdit(server);
			if (EditForm.ShowDialog(this) != DialogResult.OK)
				return;
			EditForm.DoEdit();
			Program.SaveData();
        }
        private void DeleteServer(object sender, EventArgs e)
		{
			var items = ServerList.SelectedItems;
			int count = items.Count;
			string text;
			if (count == 1)
				text = "Are you sure you want to delete this server?";
			else
				text = "Are you sure you want to delete these servers?";
			if (MessageBox.Show(this,
	            text,
	            "Alert!",
	            MessageBoxButtons.YesNo,
	            MessageBoxIcon.Warning,
	            MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				return;
			}
			foreach (ServerListViewItem data in items)
			{
				// just in case
				data.Server.Stop();
				ServerList.Items.Remove(data);
				servers.Remove(data.Server);
				Program.PushInfo(String.Format("Server #{0} '{1}' was deleted", data.Server.ID, data.Server.Name), "Main Program");
			}
			Program.SaveData();
        }

        private void NewServer(object sender, EventArgs e)
        {
            EditForm.PrepareNew();
			if (EditForm.ShowDialog(this) != DialogResult.OK)
				return;
			EditForm.CreateNew(++Program.LastServerID);
			var data = EditForm.CurrentServer;
			servers.Add(data);
		    var server = new ServerListViewItem(data);
	        server.ServerUpdated += new EventHandler(ServerList_ServerUpdated);
	        ServerList.Items.Add(server);
			Program.SaveData();
        }

        private void FileMenuQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Make sure no servers are running before exiting
            foreach (ServerListViewItem row in ServerList.Items)
            {
                if (row.Status == ProcessStatus.RUNNING)
                {
                    if (MessageBox.Show(this,
                        "Are you sure you want to shut down all running servers?",
                        "Alert!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    return;
                }
            }
        }


        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (ServerData server in servers)
            {
                server.Stop();
            }
            // Give everything time to shut down
            System.Threading.Thread.Sleep(100);
            Application.Exit();
        }

        // Oops
        private void NewButton_Click(object sender, EventArgs e)
        {
            if (ServerList.SelectedItems.Count == 0)
                NewServer(sender, e);
            else
                DeleteServer(sender, e);
        }

        private void ContextMenuDisable_Click(object sender, EventArgs e)
        {
            foreach (ServerListViewItem data in ServerList.SelectedItems)
            {
                data.Server.QueriesDisabled = !data.Server.QueriesDisabled;
            }
            UpdateButtons();
        }

    }
    class ServerListViewItem : ListViewItem
    {
        delegate void PingCBack(string Ping);
		delegate void ProcCBack(string PID, string Status);
		delegate void InfoCBack();
        PingCBack mPingCBack;
        ProcCBack mProcCBack;
		InfoCBack mInfoCBack;

		ServerData data;

        public ProcessStatus Status { get; private set; }
        public ServerData Server { get { return data;  } }

        public event EventHandler ServerUpdated;

        public ServerListViewItem(ServerData data)
        {
            this.data = data;
            Text = data.ID.ToString();
            var row  = SubItems;
            var name = row.Add(data.Name);
            var ip   = row.Add(data.IPAddress);
            var port = row.Add(data.Port.ToString());
            var pid  = row.Add("");
            var stat = row.Add("Offline");
            var ping = row.Add("N/a");
            mPingCBack = (string Ping) =>
            {
                ping.Text = Ping;
            };
            mProcCBack = (string PID, string Status) =>
            {
                pid.Text = PID;
                stat.Text = Status;
            };
			mInfoCBack = () =>
			{
				name.Text = data.Name;
				ip.Text = data.IPAddress;
				port.Text = data.Port.ToString();
			};
            data.PingChanged += new ServerPingUpdateEventHandler(OnServerPing);
            data.ProcessChanged += new ServerProcessUpdateEventHandler(OnServerProc);
			data.InfoChanged += new EventHandler(OnServerInfo);
        }
        void FireUpdateEvent()
        {
            EventHandler Event = ServerUpdated;
            if (Event != null)
                Event(this, EventArgs.Empty);
        }
        void OnServerPing(Object server, ServerPingUpdateEventArgs args)
        {
            long ping = args.LastQuery;
            string text;
            if (ping >= 0)
            {
                text = ping.ToString("##0ms");
            }
            else if (ping == -1)
            {
                text = "No Response";
            }
            else if (ping == -2)
            {
                text = "N/a";
            }
            else
            {
                text = "Error";
            }
            object[] arg = new object[] { text };
            this.ListView.Invoke(mPingCBack, arg); 
        }
        void OnServerProc(Object server, ServerProcessUpdateEventArgs args)
        {
            if (this.Status != args.Status)
                FireUpdateEvent();
            this.Status = args.Status;

            String PID = args.ProcessID != -1 ? args.ProcessID.ToString() : "";
            String Status;
            switch (args.Status)
            {
                case ProcessStatus.INACTIVE:
                    Status = "Offline";
                    break;
                case ProcessStatus.STARTING:
                    Status = "Starting";
                    break;
                case ProcessStatus.RUNNING:
                    Status = "Running";
                    break;
                case ProcessStatus.FROZEN:
                    Status = "Frozen";
                    break;
                default:
                    Status = "Internal error";
                    break;
            }
            object[] arg = new object[] { PID, Status };
            this.ListView.Invoke(mProcCBack, arg); 
        }
		void OnServerInfo(Object sender, EventArgs args)
		{
			this.ListView.Invoke(mInfoCBack);
		}
    }
}
