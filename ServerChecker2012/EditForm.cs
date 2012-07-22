using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ServerChecker2012
{
    public partial class EditForm : Form
    {
        static Regex unicode = new Regex(@"[^\u0000-\u007F]");
        static string PublicIP = "127.0.0.0";
        static int ProcessorCount = Environment.ProcessorCount;
        static EditForm()
        {
            foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    PublicIP = ip.ToString();
                    return;
                }
            }
        }
        static Color NormalColour = SystemColors.WindowText;
        static Color FadeColour = SystemColors.GrayText;
        static Color ErrorColour = Color.Red;

        public EditForm()
        {
            InitializeComponent();
            var quitems = QueryType.Items;
            quitems.Clear();
            quitems.Add("None");
            quitems.Add("Source");
            // setup processor affinity box
            if (ProcessorCount > 8)
                ProcessorAffinity.ColumnWidth = 17;
            else if (ProcessorCount > 4)
                ProcessorAffinity.ColumnWidth = 35;
            else
                ProcessorAffinity.ColumnWidth = 70;
            ProcessorAffinity.Items.Clear();
            for (var i = 1; i <= ProcessorCount; ++i)
                ProcessorAffinity.Items.Add(i.ToString(i < 10 ? " 0" : "00"));
        }

        void ResetMarkers(Color target)
        {
            NameBox.ForeColor = target;
            IPBox.ForeColor = target;
            PortBox.ForeColor = target;
            ParamBox.ForeColor = target;
            StartupTime.ForeColor = target;
            ShutdownTime.ForeColor = target;
            QueryInterval.ForeColor = target;
            QueryStrikes.ForeColor = target;
        }

        void TextBoxCheck(object tbox, string original)
        {
            TextBox box = (TextBox) tbox;
            box.ForeColor = (box.Text == original) ? FadeColour : NormalColour;
        }
        void NumericCheck(object nbox, int original)
        {
            NumericUpDown box = (NumericUpDown) nbox;
            box.ForeColor = (box.Value == original) ? FadeColour : NormalColour;
        }

        ServerData server = null;
		public ServerData CurrentServer { get { return server; } }

        public void PrepareNew()
        {
			this.Text = "New Server";
			SaveButton.Enabled = true;
            server = null;
            ResetMarkers(NormalColour);
            NameBox.Text = "";
            IPBox.Text = PublicIP;
            PortBox.Value = 27015;
            ExeBox.Text = "";
			ExePicker.InitialDirectory = "C:/";
            StartupTime.Value = 30;
            ShutdownTime.Value = 5;
            QueryInterval.Value = 15;
            QueryStrikes.Value = 3;
            ProcessorAffinity.ClearSelected();
            QueryType.SelectedIndex = 1;
        }
        
        public void PrepareEdit(ServerData server)
        {
            if (server == null)
                throw new ArgumentNullException("server");
			this.Text = "Edit Server";
			SaveButton.Enabled = true;
            this.server = server;
            ResetMarkers(FadeColour);
            NameBox.Text = server.Name;
            IPBox.Text = server.IPAddress;
            PortBox.Value = server.Port;
            ExeBox.Text = server.Executable;
			if (System.IO.File.Exists(ExeBox.Text))
	            ExePicker.InitialDirectory = System.IO.Path.GetDirectoryName(ExeBox.Text);
			else
				ExePicker.InitialDirectory = "C:/";
            ParamBox.Text = server.Parameters;
            StartupTime.Value = server.TimingData.Startup;
            ShutdownTime.Value = server.TimingData.Shutdown;
            QueryInterval.Value = server.TimingData.Interval;
            QueryStrikes.Value = server.TimingData.Strikes;
            switch (server.Query)
            {
                case ServerChecker2012.QueryType.NONE:
                    this.QueryType.SelectedIndex = 0;
                    break;
                case ServerChecker2012.QueryType.SOURCE:
                    this.QueryType.SelectedIndex = 1;
                    break;
                default:
                    this.QueryType.SelectedIndex = 0;
                    break;
            }
            Int64 affinity = server.ProcessAffinity;
            for (var i = 0; i < ProcessorCount; ++i)
                if ((affinity & 1 << i) > 0)
                    ProcessorAffinity.SetSelected(i, true);

        }

		public void CreateNew(ushort id)
		{
			if (server != null)
				throw new InvalidOperationException("Cannot call CreateNew while an Edit Server operation is in progress!");

			ServerData.TimerData timer = new ServerData.TimerData();
			timer.Interval = (ushort) this.QueryInterval.Value;
			timer.Strikes  = (ushort) this.QueryStrikes.Value;
			timer.Startup  = (ushort) this.StartupTime.Value;
			timer.Shutdown = (ushort) this.ShutdownTime.Value;

			server = new ServerData (id,
			                        this.NameBox.Text,
			                        this.IPBox.Text,
			                        (ushort) this.PortBox.Value,
			                        this.ExeBox.Text,
			                        this.ParamBox.Text,
			                        CalculateAffinity(),
			                        CalculateQueryType(),
			                        timer);
		}

		public void DoEdit()
		{
			if (server == null)
				throw new InvalidOperationException("Cannot call DoEdit while a New Server operation is in progress!");

			server.Name = this.NameBox.Text;
			server.IPAddress = this.IPBox.Text;
			server.Port = (ushort) this.PortBox.Value;
			server.Executable = this.ExeBox.Text;
			server.Parameters = this.ParamBox.Text;
			server.ProcessAffinity = CalculateAffinity();
			server.Query = CalculateQueryType();
			server.TimingData.Startup  = (ushort) this.StartupTime.Value;
			server.TimingData.Shutdown = (ushort) this.ShutdownTime.Value;
			server.TimingData.Interval = (ushort) this.QueryInterval.Value;
			server.TimingData.Strikes  = (ushort) this.QueryStrikes.Value;
		}


		long CalculateAffinity()
		{
			uint affinity = 0;
			uint one = 1; // fuck you mono
			foreach (int i in this.ProcessorAffinity.SelectedIndices)
				affinity |= (one << i);
			return affinity;
		}
		ServerChecker2012.QueryType CalculateQueryType()
		{
			ServerChecker2012.QueryType query;
			switch ((string) this.QueryType.SelectedValue)
			{
				case "Source":
					query = ServerChecker2012.QueryType.SOURCE;
					break;
				default:
					query = ServerChecker2012.QueryType.NONE;
					break;
			}
			return query;
		}

        private void ExeButton_Click(object sender, EventArgs e)
        {
            var res = ExePicker.ShowDialog();
            if (res == DialogResult.OK)
            {
                ExeBox.Text = ExePicker.FileName;
            }
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            if (server == null)
                return;
            TextBoxCheck(sender, server.Name);
        }

        private void IPBox_TextChanged(object sender, EventArgs e)
		{
			if (server == null)
				return;
			IPAddress _;
			TextBox box = (TextBox)sender;
			string text = box.Text;
			if (unicode.IsMatch(text) || !IPAddress.TryParse(text, out _))
			{
				box.ForeColor = ErrorColour;
				SaveButton.Enabled = false;
			}
			else
			{
				TextBoxCheck(sender, server.IPAddress);
			}
        }

        private void PortBox_ValueChanged(object sender, EventArgs e)
        {
            if (server == null)
                return;
            NumericCheck(sender, server.Port);
        }

        private void ParamBox_TextChanged(object sender, EventArgs e)
        {
            if (server == null)
                return;
            TextBoxCheck(sender, server.Parameters);
        }

        private void StartupTime_ValueChanged(object sender, EventArgs e)
        {
            if (server == null)
                return;
            NumericCheck(sender, server.TimingData.Startup);
        }

        private void ShutdownTime_ValueChanged(object sender, EventArgs e)
        {
            if (server == null)
                return;
            NumericCheck(sender, server.TimingData.Shutdown);
        }

        private void QueryInterval_ValueChanged(object sender, EventArgs e)
        {
            if (server == null)
                return;
            NumericCheck(sender, server.TimingData.Interval);
        }

        private void QueryStrikes_ValueChanged(object sender, EventArgs e)
        {
            if (server == null)
                return;
            NumericCheck(sender, server.TimingData.Strikes);
        }
    }
}
