﻿namespace Text_Editor
{
	partial class SearchForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtSearchTerm = new System.Windows.Forms.TextBox();
			this.btnFindNext = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkMatchCase = new System.Windows.Forms.CheckBox();
			this.chkWholeWord = new System.Windows.Forms.CheckBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Fi&nd what:";
			// 
			// txtSearchTerm
			// 
			this.txtSearchTerm.Location = new System.Drawing.Point(80, 6);
			this.txtSearchTerm.Name = "txtSearchTerm";
			this.txtSearchTerm.Size = new System.Drawing.Size(163, 23);
			this.txtSearchTerm.TabIndex = 1;
			this.txtSearchTerm.TextChanged += new System.EventHandler(this.txtSearchTerm_TextChanged);
			// 
			// btnFindNext
			// 
			this.btnFindNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFindNext.Location = new System.Drawing.Point(249, 6);
			this.btnFindNext.Name = "btnFindNext";
			this.btnFindNext.Size = new System.Drawing.Size(75, 23);
			this.btnFindNext.TabIndex = 2;
			this.btnFindNext.Text = "&Find Next";
			this.btnFindNext.UseVisualStyleBackColor = true;
			this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(249, 35);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chkMatchCase
			// 
			this.chkMatchCase.AutoSize = true;
			this.chkMatchCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkMatchCase.Location = new System.Drawing.Point(12, 64);
			this.chkMatchCase.Name = "chkMatchCase";
			this.chkMatchCase.Size = new System.Drawing.Size(92, 20);
			this.chkMatchCase.TabIndex = 4;
			this.chkMatchCase.Text = "Match &case";
			this.chkMatchCase.UseVisualStyleBackColor = true;
			this.chkMatchCase.CheckedChanged += new System.EventHandler(this.chkMatchCase_CheckedChanged);
			// 
			// chkWholeWord
			// 
			this.chkWholeWord.AutoSize = true;
			this.chkWholeWord.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkWholeWord.Location = new System.Drawing.Point(12, 37);
			this.chkWholeWord.Name = "chkWholeWord";
			this.chkWholeWord.Size = new System.Drawing.Size(157, 20);
			this.chkWholeWord.TabIndex = 5;
			this.chkWholeWord.Text = "Match &whole word only";
			this.chkWholeWord.UseVisualStyleBackColor = true;
			this.chkWholeWord.CheckedChanged += new System.EventHandler(this.chkWholeWord_CheckedChanged);
			// 
			// timer1
			// 
			this.timer1.Interval = 10;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// SearchForm
			// 
			this.AcceptButton = this.btnFindNext;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(336, 95);
			this.Controls.Add(this.chkWholeWord);
			this.Controls.Add(this.chkMatchCase);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnFindNext);
			this.Controls.Add(this.txtSearchTerm);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SearchForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSearchTerm;
		private System.Windows.Forms.Button btnFindNext;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkMatchCase;
		private System.Windows.Forms.CheckBox chkWholeWord;
		private System.Windows.Forms.Timer timer1;
	}
}
