namespace Text_Editor
{
    partial class SettingsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAcceptBoth = new System.Windows.Forms.RadioButton();
            this.rbCtrlShiftZ = new System.Windows.Forms.RadioButton();
            this.rbCtrlY = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.numRecentFilesMax = new System.Windows.Forms.NumericUpDown();
            this.lblMaxRecent = new System.Windows.Forms.Label();
            this.chkSaveRecentFiles = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRecentFilesMax)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(333, 400);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(325, 372);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Keyboard Shortcuts";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAcceptBoth);
            this.groupBox1.Controls.Add(this.rbCtrlShiftZ);
            this.groupBox1.Controls.Add(this.rbCtrlY);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Redo";
            // 
            // rbAcceptBoth
            // 
            this.rbAcceptBoth.AutoSize = true;
            this.rbAcceptBoth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbAcceptBoth.Location = new System.Drawing.Point(22, 89);
            this.rbAcceptBoth.Name = "rbAcceptBoth";
            this.rbAcceptBoth.Size = new System.Drawing.Size(96, 20);
            this.rbAcceptBoth.TabIndex = 3;
            this.rbAcceptBoth.TabStop = true;
            this.rbAcceptBoth.Text = "Accept both";
            this.rbAcceptBoth.UseVisualStyleBackColor = true;
            this.rbAcceptBoth.CheckedChanged += new System.EventHandler(this.RedoRadioButton_CheckedChanged);
            // 
            // rbCtrlShiftZ
            // 
            this.rbCtrlShiftZ.AutoSize = true;
            this.rbCtrlShiftZ.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbCtrlShiftZ.Location = new System.Drawing.Point(22, 63);
            this.rbCtrlShiftZ.Name = "rbCtrlShiftZ";
            this.rbCtrlShiftZ.Size = new System.Drawing.Size(97, 20);
            this.rbCtrlShiftZ.TabIndex = 2;
            this.rbCtrlShiftZ.TabStop = true;
            this.rbCtrlShiftZ.Text = "Ctrl+Shift+Z";
            this.rbCtrlShiftZ.UseVisualStyleBackColor = true;
            this.rbCtrlShiftZ.CheckedChanged += new System.EventHandler(this.RedoRadioButton_CheckedChanged);
            // 
            // rbCtrlY
            // 
            this.rbCtrlY.AutoSize = true;
            this.rbCtrlY.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbCtrlY.Location = new System.Drawing.Point(22, 37);
            this.rbCtrlY.Name = "rbCtrlY";
            this.rbCtrlY.Size = new System.Drawing.Size(65, 20);
            this.rbCtrlY.TabIndex = 1;
            this.rbCtrlY.TabStop = true;
            this.rbCtrlY.Text = "Ctrl+Y";
            this.rbCtrlY.UseVisualStyleBackColor = true;
            this.rbCtrlY.CheckedChanged += new System.EventHandler(this.RedoRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Keyboard shortcut to use for redo:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.numRecentFilesMax);
            this.tabPage2.Controls.Add(this.lblMaxRecent);
            this.tabPage2.Controls.Add(this.chkSaveRecentFiles);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(325, 372);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Recent Files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // numRecentFilesMax
            // 
            this.numRecentFilesMax.Enabled = false;
            this.numRecentFilesMax.Location = new System.Drawing.Point(250, 27);
            this.numRecentFilesMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRecentFilesMax.Name = "numRecentFilesMax";
            this.numRecentFilesMax.Size = new System.Drawing.Size(69, 23);
            this.numRecentFilesMax.TabIndex = 2;
            this.numRecentFilesMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMaxRecent
            // 
            this.lblMaxRecent.AutoSize = true;
            this.lblMaxRecent.Enabled = false;
            this.lblMaxRecent.Location = new System.Drawing.Point(22, 29);
            this.lblMaxRecent.Name = "lblMaxRecent";
            this.lblMaxRecent.Size = new System.Drawing.Size(224, 15);
            this.lblMaxRecent.TabIndex = 1;
            this.lblMaxRecent.Text = "Maximum amount of recent files to save:";
            // 
            // chkSaveRecentFiles
            // 
            this.chkSaveRecentFiles.AutoSize = true;
            this.chkSaveRecentFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkSaveRecentFiles.Location = new System.Drawing.Point(6, 6);
            this.chkSaveRecentFiles.Name = "chkSaveRecentFiles";
            this.chkSaveRecentFiles.Size = new System.Drawing.Size(200, 20);
            this.chkSaveRecentFiles.TabIndex = 0;
            this.chkSaveRecentFiles.Text = "Save file &history in Notepad.NET";
            this.chkSaveRecentFiles.UseVisualStyleBackColor = true;
            this.chkSaveRecentFiles.CheckedChanged += new System.EventHandler(this.chkSaveRecentFiles_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(185, 418);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(266, 418);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(357, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRecentFilesMax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numRecentFilesMax;
        private System.Windows.Forms.Label lblMaxRecent;
        private System.Windows.Forms.CheckBox chkSaveRecentFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbCtrlY;
        private System.Windows.Forms.RadioButton rbAcceptBoth;
        private System.Windows.Forms.RadioButton rbCtrlShiftZ;
    }
}