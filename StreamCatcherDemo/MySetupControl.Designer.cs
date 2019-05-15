namespace StreamCatcherDemo
{
    partial class MySetupControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MySetupControl));
            this.m_btnFileRecording = new System.Windows.Forms.Button();
            this.m_btnSnapShot = new System.Windows.Forms.Button();
            this.timerShowInfo = new System.Windows.Forms.Timer(this.components);
            this.m_checkAutoDeinterlace = new System.Windows.Forms.CheckBox();
            this.m_btnVideoQuality = new System.Windows.Forms.Button();
            this.timerCheckSignal = new System.Windows.Forms.Timer(this.components);
            this.m_btnVideoInput = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timerDeleteSQL = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_btnFileRecording
            // 
            this.m_btnFileRecording.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnFileRecording.Location = new System.Drawing.Point(9, 173);
            this.m_btnFileRecording.Name = "m_btnFileRecording";
            this.m_btnFileRecording.Size = new System.Drawing.Size(154, 43);
            this.m_btnFileRecording.TabIndex = 80;
            this.m_btnFileRecording.Text = "FILE RECORDING";
            this.m_btnFileRecording.UseVisualStyleBackColor = true;
            this.m_btnFileRecording.Click += new System.EventHandler(this.m_btnFileRecording_Click);
            // 
            // m_btnSnapShot
            // 
            this.m_btnSnapShot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSnapShot.Location = new System.Drawing.Point(169, 172);
            this.m_btnSnapShot.Name = "m_btnSnapShot";
            this.m_btnSnapShot.Size = new System.Drawing.Size(154, 43);
            this.m_btnSnapShot.TabIndex = 79;
            this.m_btnSnapShot.Text = "SNAPSHOT";
            this.m_btnSnapShot.UseVisualStyleBackColor = true;
            this.m_btnSnapShot.Click += new System.EventHandler(this.m_btnSnapShot_Click);
            // 
            // timerShowInfo
            // 
            this.timerShowInfo.Enabled = true;
            this.timerShowInfo.Interval = 500;
            this.timerShowInfo.Tick += new System.EventHandler(this.timerShowInfo_Tick);
            // 
            // m_checkAutoDeinterlace
            // 
            this.m_checkAutoDeinterlace.AutoSize = true;
            this.m_checkAutoDeinterlace.Location = new System.Drawing.Point(371, 185);
            this.m_checkAutoDeinterlace.Name = "m_checkAutoDeinterlace";
            this.m_checkAutoDeinterlace.Size = new System.Drawing.Size(155, 20);
            this.m_checkAutoDeinterlace.TabIndex = 74;
            this.m_checkAutoDeinterlace.Text = "AUTO DEINTERLACE";
            this.m_checkAutoDeinterlace.UseVisualStyleBackColor = true;
            this.m_checkAutoDeinterlace.Click += new System.EventHandler(this.m_checkAutoDeinterlace_Click);
            // 
            // m_btnVideoQuality
            // 
            this.m_btnVideoQuality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnVideoQuality.Location = new System.Drawing.Point(169, 123);
            this.m_btnVideoQuality.Name = "m_btnVideoQuality";
            this.m_btnVideoQuality.Size = new System.Drawing.Size(154, 43);
            this.m_btnVideoQuality.TabIndex = 73;
            this.m_btnVideoQuality.Text = " VIDEO QUALITY +";
            this.m_btnVideoQuality.UseVisualStyleBackColor = true;
            this.m_btnVideoQuality.Click += new System.EventHandler(this.m_btnVideoQuality_Click);
            // 
            // timerCheckSignal
            // 
            this.timerCheckSignal.Interval = 1000;
            this.timerCheckSignal.Tick += new System.EventHandler(this.timerCheckSignal_Tick);
            // 
            // m_btnVideoInput
            // 
            this.m_btnVideoInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnVideoInput.Location = new System.Drawing.Point(9, 123);
            this.m_btnVideoInput.Name = "m_btnVideoInput";
            this.m_btnVideoInput.Size = new System.Drawing.Size(154, 43);
            this.m_btnVideoInput.TabIndex = 71;
            this.m_btnVideoInput.Text = " VIDEO INPUT +";
            this.m_btnVideoInput.UseVisualStyleBackColor = true;
            this.m_btnVideoInput.Click += new System.EventHandler(this.m_btnVideoInput_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(371, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 84;
            this.button1.Text = "Deletesql";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timerDeleteSQL
            // 
            this.timerDeleteSQL.Interval = 50000;
            this.timerDeleteSQL.Tick += new System.EventHandler(this.timerDeleteSQL_Tick);
            // 
            // MySetupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 226);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_btnFileRecording);
            this.Controls.Add(this.m_btnSnapShot);
            this.Controls.Add(this.m_checkAutoDeinterlace);
            this.Controls.Add(this.m_btnVideoQuality);
            this.Controls.Add(this.m_btnVideoInput);
            this.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MySetupControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Yuan\'s Capture Card Demo Software";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MySetupControl_FormClosed);
            this.Load += new System.EventHandler(this.MySetupControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnFileRecording;
        private System.Windows.Forms.Button m_btnSnapShot;
      
        private System.Windows.Forms.Timer timerShowInfo;
        private System.Windows.Forms.CheckBox m_checkAutoDeinterlace;
        private System.Windows.Forms.Button m_btnVideoQuality;
        private System.Windows.Forms.Timer timerCheckSignal;
      
        private System.Windows.Forms.Button m_btnVideoInput;
       
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timerDeleteSQL;
    }
}