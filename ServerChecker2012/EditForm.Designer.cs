namespace ServerChecker2012
{
    partial class EditForm
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
            System.Windows.Forms.Label ipseprlabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            this.ExePicker = new System.Windows.Forms.OpenFileDialog();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.PortBox = new System.Windows.Forms.NumericUpDown();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.ExeBox = new System.Windows.Forms.TextBox();
            this.ExeButton = new System.Windows.Forms.Button();
            this.ParamBox = new System.Windows.Forms.TextBox();
            this.StartupTime = new System.Windows.Forms.NumericUpDown();
            this.QueryInterval = new System.Windows.Forms.NumericUpDown();
            this.ShutdownTime = new System.Windows.Forms.NumericUpDown();
            this.QueryStrikes = new System.Windows.Forms.NumericUpDown();
            this.ProcessorAffinity = new System.Windows.Forms.ListBox();
            this.QueryType = new System.Windows.Forms.ComboBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            ipseprlabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PortBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShutdownTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryStrikes)).BeginInit();
            this.SuspendLayout();
            // 
            // ipseprlabel
            // 
            ipseprlabel.AutoSize = true;
            ipseprlabel.Location = new System.Drawing.Point(175, 38);
            ipseprlabel.Name = "ipseprlabel";
            ipseprlabel.Size = new System.Drawing.Size(10, 13);
            ipseprlabel.TabIndex = 3;
            ipseprlabel.Text = ":";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(69, 13);
            label1.TabIndex = 7;
            label1.Text = "Server Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 38);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(41, 13);
            label2.TabIndex = 8;
            label2.Text = "IP/Port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 64);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(60, 13);
            label3.TabIndex = 9;
            label3.Text = "Executable";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 90);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(60, 13);
            label4.TabIndex = 10;
            label4.Text = "Parameters";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(247, 12);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(67, 13);
            label7.TabIndex = 13;
            label7.Text = "Startup Time";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(247, 64);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(73, 13);
            label8.TabIndex = 14;
            label8.Text = "Query Interval";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(247, 38);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(81, 13);
            label5.TabIndex = 19;
            label5.Text = "Shutdown Time";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(247, 90);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(70, 13);
            label6.TabIndex = 22;
            label6.Text = "Query Strikes";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(12, 117);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(54, 13);
            label9.TabIndex = 24;
            label9.Text = "Processor";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(20, 129);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(38, 13);
            label10.TabIndex = 25;
            label10.Text = "Affinity";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(247, 120);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(62, 13);
            label11.TabIndex = 26;
            label11.Text = "Query Type";
            // 
            // ExePicker
            // 
            this.ExePicker.DefaultExt = "exe";
            this.ExePicker.Filter = "Exe Files|*.exe|All files|*.*";
            this.ExePicker.InitialDirectory = "C:\\";
            this.ExePicker.Title = "Choose Server Executable";
            // 
            // NameBox
            // 
            this.NameBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.NameBox.Location = new System.Drawing.Point(88, 9);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(146, 20);
            this.NameBox.TabIndex = 0;
            this.NameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            // 
            // PortBox
            // 
            this.PortBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.PortBox.Location = new System.Drawing.Point(183, 35);
            this.PortBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(51, 20);
            this.PortBox.TabIndex = 2;
            this.PortBox.Value = new decimal(new int[] {
            27015,
            0,
            0,
            0});
            this.PortBox.ValueChanged += new System.EventHandler(this.PortBox_ValueChanged);
            // 
            // IPBox
            // 
            this.IPBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.IPBox.Location = new System.Drawing.Point(88, 35);
            this.IPBox.MaxLength = 45;
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(89, 20);
            this.IPBox.TabIndex = 1;
            this.IPBox.Text = "255.255.255.255";
            this.IPBox.TextChanged += new System.EventHandler(this.IPBox_TextChanged);
            // 
            // ExeBox
            // 
            this.ExeBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ExeBox.Location = new System.Drawing.Point(88, 61);
            this.ExeBox.Name = "ExeBox";
            this.ExeBox.ReadOnly = true;
            this.ExeBox.Size = new System.Drawing.Size(120, 20);
            this.ExeBox.TabIndex = 4;
            this.ExeBox.TabStop = false;
            // 
            // ExeButton
            // 
            this.ExeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExeButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ExeButton.Image = global::ServerChecker2012.Properties.Resources.folder_explore;
            this.ExeButton.Location = new System.Drawing.Point(214, 61);
            this.ExeButton.Name = "ExeButton";
            this.ExeButton.Size = new System.Drawing.Size(20, 20);
            this.ExeButton.TabIndex = 5;
            this.ExeButton.UseVisualStyleBackColor = true;
            this.ExeButton.Click += new System.EventHandler(this.ExeButton_Click);
            // 
            // ParamBox
            // 
            this.ParamBox.Location = new System.Drawing.Point(88, 87);
            this.ParamBox.Name = "ParamBox";
            this.ParamBox.Size = new System.Drawing.Size(146, 20);
            this.ParamBox.TabIndex = 6;
            this.ParamBox.TextChanged += new System.EventHandler(this.ParamBox_TextChanged);
            // 
            // StartupTime
            // 
            this.StartupTime.Location = new System.Drawing.Point(336, 9);
            this.StartupTime.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.StartupTime.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.StartupTime.Name = "StartupTime";
            this.StartupTime.Size = new System.Drawing.Size(42, 20);
            this.StartupTime.TabIndex = 17;
            this.StartupTime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.StartupTime.ValueChanged += new System.EventHandler(this.StartupTime_ValueChanged);
            // 
            // QueryInterval
            // 
            this.QueryInterval.Location = new System.Drawing.Point(336, 61);
            this.QueryInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.QueryInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.QueryInterval.Name = "QueryInterval";
            this.QueryInterval.Size = new System.Drawing.Size(42, 20);
            this.QueryInterval.TabIndex = 18;
            this.QueryInterval.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.QueryInterval.ValueChanged += new System.EventHandler(this.QueryInterval_ValueChanged);
            // 
            // ShutdownTime
            // 
            this.ShutdownTime.Location = new System.Drawing.Point(336, 35);
            this.ShutdownTime.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ShutdownTime.Name = "ShutdownTime";
            this.ShutdownTime.Size = new System.Drawing.Size(42, 20);
            this.ShutdownTime.TabIndex = 20;
            this.ShutdownTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ShutdownTime.ValueChanged += new System.EventHandler(this.ShutdownTime_ValueChanged);
            // 
            // QueryStrikes
            // 
            this.QueryStrikes.Location = new System.Drawing.Point(336, 87);
            this.QueryStrikes.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.QueryStrikes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.QueryStrikes.Name = "QueryStrikes";
            this.QueryStrikes.Size = new System.Drawing.Size(42, 20);
            this.QueryStrikes.TabIndex = 21;
            this.QueryStrikes.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.QueryStrikes.ValueChanged += new System.EventHandler(this.QueryStrikes_ValueChanged);
            // 
            // ProcessorAffinity
            // 
            this.ProcessorAffinity.ColumnWidth = 70;
            this.ProcessorAffinity.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ProcessorAffinity.FormattingEnabled = true;
            this.ProcessorAffinity.Items.AddRange(new object[] {
            ""});
            this.ProcessorAffinity.Location = new System.Drawing.Point(88, 114);
            this.ProcessorAffinity.MultiColumn = true;
            this.ProcessorAffinity.Name = "ProcessorAffinity";
            this.ProcessorAffinity.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.ProcessorAffinity.Size = new System.Drawing.Size(146, 30);
            this.ProcessorAffinity.TabIndex = 23;
            this.ProcessorAffinity.TabStop = false;
            // 
            // QueryType
            // 
            this.QueryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QueryType.FormattingEnabled = true;
            this.QueryType.Location = new System.Drawing.Point(315, 117);
            this.QueryType.Name = "QueryType";
            this.QueryType.Size = new System.Drawing.Size(62, 21);
            this.QueryType.TabIndex = 27;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(250, 159);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(127, 28);
            this.Cancel.TabIndex = 28;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveButton.Location = new System.Drawing.Point(15, 159);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(108, 28);
            this.SaveButton.TabIndex = 29;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(390, 199);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.QueryType);
            this.Controls.Add(label11);
            this.Controls.Add(label10);
            this.Controls.Add(label9);
            this.Controls.Add(this.ProcessorAffinity);
            this.Controls.Add(label6);
            this.Controls.Add(this.QueryStrikes);
            this.Controls.Add(this.ShutdownTime);
            this.Controls.Add(label5);
            this.Controls.Add(this.QueryInterval);
            this.Controls.Add(this.StartupTime);
            this.Controls.Add(label8);
            this.Controls.Add(label7);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.ParamBox);
            this.Controls.Add(this.ExeButton);
            this.Controls.Add(this.ExeBox);
            this.Controls.Add(this.IPBox);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(ipseprlabel);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.Text = "Edit Server";
            ((System.ComponentModel.ISupportInitialize)(this.PortBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShutdownTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QueryStrikes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ExePicker;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.NumericUpDown PortBox;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.TextBox ExeBox;
        private System.Windows.Forms.Button ExeButton;
        private System.Windows.Forms.TextBox ParamBox;
        private System.Windows.Forms.NumericUpDown StartupTime;
        private System.Windows.Forms.NumericUpDown QueryInterval;
        private System.Windows.Forms.NumericUpDown ShutdownTime;
        private System.Windows.Forms.NumericUpDown QueryStrikes;
        private System.Windows.Forms.ListBox ProcessorAffinity;
        private System.Windows.Forms.ComboBox QueryType;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button SaveButton;
    }
}