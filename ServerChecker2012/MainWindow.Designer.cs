namespace ServerChecker2012
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel ButtonContainer;
            System.Windows.Forms.Panel ButtonLeftContainer;
            System.Windows.Forms.Panel ButtonRightContainer;
            System.Windows.Forms.Panel PaddingPanel;
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
            this.EditButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.ServerList = new System.Windows.Forms.ListView();
            this.IndexColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IPColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PortColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PIDColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProcStatColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.QueryStatColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContextMenu_ = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ServerControlMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuNewServer = new System.Windows.Forms.ToolStripMenuItem();
            this.disableALLQueriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            ButtonContainer = new System.Windows.Forms.Panel();
            ButtonLeftContainer = new System.Windows.Forms.Panel();
            ButtonRightContainer = new System.Windows.Forms.Panel();
            PaddingPanel = new System.Windows.Forms.Panel();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            ButtonContainer.SuspendLayout();
            ButtonLeftContainer.SuspendLayout();
            ButtonRightContainer.SuspendLayout();
            PaddingPanel.SuspendLayout();
            this.ContextMenu_.SuspendLayout();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonContainer
            // 
            ButtonContainer.BackColor = System.Drawing.SystemColors.Control;
            ButtonContainer.Controls.Add(ButtonLeftContainer);
            ButtonContainer.Controls.Add(ButtonRightContainer);
            ButtonContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            ButtonContainer.Location = new System.Drawing.Point(6, 115);
            ButtonContainer.Name = "ButtonContainer";
            ButtonContainer.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            ButtonContainer.Size = new System.Drawing.Size(565, 37);
            ButtonContainer.TabIndex = 0;
            // 
            // ButtonLeftContainer
            // 
            ButtonLeftContainer.Controls.Add(this.EditButton);
            ButtonLeftContainer.Controls.Add(this.NewButton);
            ButtonLeftContainer.Dock = System.Windows.Forms.DockStyle.Left;
            ButtonLeftContainer.Location = new System.Drawing.Point(0, 6);
            ButtonLeftContainer.Name = "ButtonLeftContainer";
            ButtonLeftContainer.Size = new System.Drawing.Size(190, 31);
            ButtonLeftContainer.TabIndex = 1;
            // 
            // EditButton
            // 
            this.EditButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.EditButton.Enabled = false;
            this.EditButton.Location = new System.Drawing.Point(110, 0);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(80, 31);
            this.EditButton.TabIndex = 1;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditServer);
            // 
            // NewButton
            // 
            this.NewButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.NewButton.Location = new System.Drawing.Point(0, 0);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(80, 31);
            this.NewButton.TabIndex = 0;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // ButtonRightContainer
            // 
            ButtonRightContainer.Controls.Add(this.StartButton);
            ButtonRightContainer.Controls.Add(this.StopButton);
            ButtonRightContainer.Dock = System.Windows.Forms.DockStyle.Right;
            ButtonRightContainer.Location = new System.Drawing.Point(375, 6);
            ButtonRightContainer.Name = "ButtonRightContainer";
            ButtonRightContainer.Size = new System.Drawing.Size(190, 31);
            ButtonRightContainer.TabIndex = 0;
            // 
            // StartButton
            // 
            this.StartButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(0, 0);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(80, 31);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartServer);
            // 
            // StopButton
            // 
            this.StopButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(110, 0);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(80, 31);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopServer);
            // 
            // PaddingPanel
            // 
            PaddingPanel.BackColor = System.Drawing.SystemColors.Control;
            PaddingPanel.Controls.Add(ButtonContainer);
            PaddingPanel.Controls.Add(this.ServerList);
            PaddingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            PaddingPanel.Location = new System.Drawing.Point(0, 24);
            PaddingPanel.Name = "PaddingPanel";
            PaddingPanel.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            PaddingPanel.Size = new System.Drawing.Size(577, 158);
            PaddingPanel.TabIndex = 4;
            // 
            // ServerList
            // 
            this.ServerList.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.ServerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IndexColumn,
            this.NameColumn,
            this.IPColumn,
            this.PortColumn,
            this.PIDColumn,
            this.ProcStatColumn,
            this.QueryStatColumn});
            this.ServerList.ContextMenuStrip = this.ContextMenu_;
            this.ServerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerList.FullRowSelect = true;
            this.ServerList.GridLines = true;
            this.ServerList.Location = new System.Drawing.Point(6, 0);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(565, 152);
            this.ServerList.TabIndex = 2;
            this.ServerList.UseCompatibleStateImageBehavior = false;
            this.ServerList.View = System.Windows.Forms.View.Details;
            this.ServerList.SelectedIndexChanged += new System.EventHandler(this.ServerList_SelectedIndexChanged);
            // 
            // IndexColumn
            // 
            this.IndexColumn.Text = "Index";
            this.IndexColumn.Width = 38;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 170;
            // 
            // IPColumn
            // 
            this.IPColumn.Text = "IP";
            this.IPColumn.Width = 93;
            // 
            // PortColumn
            // 
            this.PortColumn.Text = "Port";
            this.PortColumn.Width = 42;
            // 
            // PIDColumn
            // 
            this.PIDColumn.Text = "PID";
            this.PIDColumn.Width = 38;
            // 
            // ProcStatColumn
            // 
            this.ProcStatColumn.Text = "Process Status";
            this.ProcStatColumn.Width = 84;
            // 
            // QueryStatColumn
            // 
            this.QueryStatColumn.Text = "Query Status";
            this.QueryStatColumn.Width = 90;
            // 
            // ContextMenu_
            // 
            this.ContextMenu_.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuStart,
            this.ContextMenuStop,
            this.ContextMenuRestart,
            this.toolStripMenuItem2,
            this.ContextMenuEdit,
            this.ContextMenuDisable,
            this.ContextMenuDelete});
            this.ContextMenu_.Name = "ContextMenu";
            this.ContextMenu_.Size = new System.Drawing.Size(156, 142);
            // 
            // ContextMenuStart
            // 
            this.ContextMenuStart.Enabled = false;
            this.ContextMenuStart.Name = "ContextMenuStart";
            this.ContextMenuStart.Size = new System.Drawing.Size(155, 22);
            this.ContextMenuStart.Text = "Start";
            this.ContextMenuStart.Click += new System.EventHandler(this.StartServer);
            // 
            // ContextMenuStop
            // 
            this.ContextMenuStop.Name = "ContextMenuStop";
            this.ContextMenuStop.Size = new System.Drawing.Size(155, 22);
            this.ContextMenuStop.Text = "Stop";
            this.ContextMenuStop.Visible = false;
            this.ContextMenuStop.Click += new System.EventHandler(this.StopServer);
            // 
            // ContextMenuRestart
            // 
            this.ContextMenuRestart.Name = "ContextMenuRestart";
            this.ContextMenuRestart.Size = new System.Drawing.Size(155, 22);
            this.ContextMenuRestart.Text = "Restart";
            this.ContextMenuRestart.Visible = false;
            this.ContextMenuRestart.Click += new System.EventHandler(this.StartServer);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 6);
            // 
            // ContextMenuEdit
            // 
            this.ContextMenuEdit.Enabled = false;
            this.ContextMenuEdit.Name = "ContextMenuEdit";
            this.ContextMenuEdit.Size = new System.Drawing.Size(155, 22);
            this.ContextMenuEdit.Text = "Edit";
            this.ContextMenuEdit.Click += new System.EventHandler(this.EditServer);
            // 
            // ContextMenuDisable
            // 
            this.ContextMenuDisable.Enabled = false;
            this.ContextMenuDisable.Name = "ContextMenuDisable";
            this.ContextMenuDisable.Size = new System.Drawing.Size(155, 22);
            this.ContextMenuDisable.Text = "Disable Queries";
            this.ContextMenuDisable.Click += new System.EventHandler(this.ContextMenuDisable_Click);
            // 
            // ContextMenuDelete
            // 
            this.ContextMenuDelete.Enabled = false;
            this.ContextMenuDelete.Name = "ContextMenuDelete";
            this.ContextMenuDelete.Size = new System.Drawing.Size(155, 22);
            this.ContextMenuDelete.Text = "Delete";
            this.ContextMenuDelete.Click += new System.EventHandler(this.DeleteServer);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(175, 6);
            // 
            // ServerControlMenu
            // 
            this.ServerControlMenu.DropDown = this.ContextMenu_;
            this.ServerControlMenu.Name = "ServerControlMenu";
            this.ServerControlMenu.Size = new System.Drawing.Size(94, 20);
            this.ServerControlMenu.Text = "Server Control";
            // 
            // MenuBar
            // 
            this.MenuBar.BackColor = System.Drawing.SystemColors.MenuBar;
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.ServerControlMenu});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(577, 24);
            this.MenuBar.TabIndex = 3;
            this.MenuBar.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuNewServer,
            this.disableALLQueriesToolStripMenuItem,
            toolStripMenuItem1,
            this.FileMenuQuit});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FileMenu.ShortcutKeyDisplayString = "F";
            this.FileMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.FileMenu.Size = new System.Drawing.Size(33, 20);
            this.FileMenu.Text = "File";
            // 
            // FileMenuNewServer
            // 
            this.FileMenuNewServer.Name = "FileMenuNewServer";
            this.FileMenuNewServer.Size = new System.Drawing.Size(178, 22);
            this.FileMenuNewServer.Text = "New Server";
            this.FileMenuNewServer.Click += new System.EventHandler(this.NewServer);
            // 
            // disableALLQueriesToolStripMenuItem
            // 
            this.disableALLQueriesToolStripMenuItem.Name = "disableALLQueriesToolStripMenuItem";
            this.disableALLQueriesToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.disableALLQueriesToolStripMenuItem.Text = "Disable ALL Queries";
            // 
            // FileMenuQuit
            // 
            this.FileMenuQuit.Name = "FileMenuQuit";
            this.FileMenuQuit.Size = new System.Drawing.Size(178, 22);
            this.FileMenuQuit.Text = "Quit";
            this.FileMenuQuit.Click += new System.EventHandler(this.FileMenuQuit_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 182);
            this.Controls.Add(PaddingPanel);
            this.Controls.Add(this.MenuBar);
            this.MainMenuStrip = this.MenuBar;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 200);
            this.Name = "MainWindow";
            this.Text = "ServerChecker 2012";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            ButtonContainer.ResumeLayout(false);
            ButtonLeftContainer.ResumeLayout(false);
            ButtonRightContainer.ResumeLayout(false);
            PaddingPanel.ResumeLayout(false);
            this.ContextMenu_.ResumeLayout(false);
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ServerList;
        private System.Windows.Forms.ColumnHeader IndexColumn;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader IPColumn;
        private System.Windows.Forms.ColumnHeader PortColumn;
        private System.Windows.Forms.ColumnHeader ProcStatColumn;
        private System.Windows.Forms.ColumnHeader PIDColumn;
        private System.Windows.Forms.ColumnHeader QueryStatColumn;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.ContextMenuStrip ContextMenu_;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuStop;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuRestart;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuDelete;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuStart;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenuNewServer;
        private System.Windows.Forms.ToolStripMenuItem FileMenuQuit;
        private System.Windows.Forms.ToolStripMenuItem ServerControlMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuDisable;
        private System.Windows.Forms.ToolStripMenuItem disableALLQueriesToolStripMenuItem;



    }
}

