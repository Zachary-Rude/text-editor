namespace Text_Editor
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem40 = new System.Windows.Forms.MenuItem();
            this.menuClear = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem44 = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.menuItem46 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuItem26 = new System.Windows.Forms.MenuItem();
            this.menuItem30 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem42 = new System.Windows.Forms.MenuItem();
            this.menuItem43 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem32 = new System.Windows.Forms.MenuItem();
            this.menuItem33 = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem29 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem27 = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainEditor = new Text_Editor.FixedRichTextBox();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuItem38 = new System.Windows.Forms.MenuItem();
            this.menuItem39 = new System.Windows.Forms.MenuItem();
            this.vistaMenu = new wyDay.Controls.VistaMenu(this.components);
            this.enableDisableTimer = new System.Windows.Forms.Timer(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vistaMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem9,
            this.menuItem17,
            this.menuItem23});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem40,
            this.menuItem41,
            this.menuItem5,
            this.menuItem6,
            this.menuItem44,
            this.menuItem45,
            this.menuItem46,
            this.menuItem7,
            this.menuItem8});
            this.menuItem1.Text = "&File";
            // 
            // menuItem2
            // 
            this.vistaMenu.SetImage(this.menuItem2, global::Text_Editor.Properties.Resources.baseline_create_black_24dp);
            this.menuItem2.Index = 0;
            this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItem2.Text = "&New";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.vistaMenu.SetImage(this.menuItem3, global::Text_Editor.Properties.Resources.baseline_open_in_new_black_24dp);
            this.menuItem3.Index = 1;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftN;
            this.menuItem3.Text = "New &Window";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.vistaMenu.SetImage(this.menuItem4, global::Text_Editor.Properties.Resources.baseline_file_open_black_24dp);
            this.menuItem4.Index = 2;
            this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem4.Text = "&Open...";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem40
            // 
            this.vistaMenu.SetImage(this.menuItem40, global::Text_Editor.Properties.Resources.baseline_history_black_24dp);
            this.menuItem40.Index = 3;
            this.menuItem40.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuClear});
            this.menuItem40.Text = "Recent Files";
            // 
            // menuClear
            // 
            this.menuClear.Index = 0;
            this.menuClear.Text = "Clear";
            this.menuClear.Click += new System.EventHandler(this.menuClear_Click);
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 4;
            this.menuItem41.Text = "-";
            // 
            // menuItem5
            // 
            this.vistaMenu.SetImage(this.menuItem5, global::Text_Editor.Properties.Resources.baseline_save_black_24dp);
            this.menuItem5.Index = 5;
            this.menuItem5.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuItem5.Text = "&Save";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.vistaMenu.SetImage(this.menuItem6, global::Text_Editor.Properties.Resources.baseline_save_as_black_24dp);
            this.menuItem6.Index = 6;
            this.menuItem6.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.menuItem6.Text = "Save &As...";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem44
            // 
            this.menuItem44.Index = 7;
            this.menuItem44.Text = "-";
            // 
            // menuItem45
            // 
            this.vistaMenu.SetImage(this.menuItem45, global::Text_Editor.Properties.Resources.baseline_description_black_24dp);
            this.menuItem45.Index = 8;
            this.menuItem45.Text = "Page Setup...";
            this.menuItem45.Click += new System.EventHandler(this.menuItem45_Click);
            // 
            // menuItem46
            // 
            this.vistaMenu.SetImage(this.menuItem46, global::Text_Editor.Properties.Resources.baseline_print_black_24dp);
            this.menuItem46.Index = 9;
            this.menuItem46.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuItem46.Text = "Print...";
            this.menuItem46.Click += new System.EventHandler(this.menuItem46_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 10;
            this.menuItem7.Text = "-";
            // 
            // menuItem8
            // 
            this.vistaMenu.SetImage(this.menuItem8, global::Text_Editor.Properties.Resources.baseline_close_black_24dp);
            this.menuItem8.Index = 11;
            this.menuItem8.Text = "E&xit";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10,
            this.menuItem19,
            this.menuItem11,
            this.menuItem12,
            this.menuItem13,
            this.menuItem14,
            this.menuItem25,
            this.menuItem26,
            this.menuItem30,
            this.menuItem15,
            this.menuItem16,
            this.menuItem42,
            this.menuItem43});
            this.menuItem9.Text = "&Edit";
            // 
            // menuItem10
            // 
            this.vistaMenu.SetImage(this.menuItem10, global::Text_Editor.Properties.Resources.baseline_undo_black_24dp);
            this.menuItem10.Index = 0;
            this.menuItem10.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.menuItem10.Text = "&Undo";
            this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem19
            // 
            this.vistaMenu.SetImage(this.menuItem19, global::Text_Editor.Properties.Resources.baseline_redo_black_24dp);
            this.menuItem19.Index = 1;
            this.menuItem19.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.menuItem19.Text = "&Redo";
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 2;
            this.menuItem11.Text = "-";
            // 
            // menuItem12
            // 
            this.vistaMenu.SetImage(this.menuItem12, global::Text_Editor.Properties.Resources.baseline_content_cut_black_24dp);
            this.menuItem12.Index = 3;
            this.menuItem12.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.menuItem12.Text = "Cu&t";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem13
            // 
            this.vistaMenu.SetImage(this.menuItem13, global::Text_Editor.Properties.Resources.baseline_content_copy_black_24dp);
            this.menuItem13.Index = 4;
            this.menuItem13.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuItem13.Text = "&Copy";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem14
            // 
            this.vistaMenu.SetImage(this.menuItem14, global::Text_Editor.Properties.Resources.baseline_content_paste_black_24dp);
            this.menuItem14.Index = 5;
            this.menuItem14.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.menuItem14.Text = "&Paste";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 6;
            this.menuItem25.Text = "-";
            // 
            // menuItem26
            // 
            this.vistaMenu.SetImage(this.menuItem26, global::Text_Editor.Properties.Resources.baseline_find_in_page_black_24dp);
            this.menuItem26.Index = 7;
            this.menuItem26.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuItem26.Text = "&Find...";
            this.menuItem26.Click += new System.EventHandler(this.menuItem26_Click);
            // 
            // menuItem30
            // 
            this.vistaMenu.SetImage(this.menuItem30, global::Text_Editor.Properties.Resources.baseline_find_replace_black_24dp);
            this.menuItem30.Index = 8;
            this.menuItem30.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.menuItem30.Text = "&Replace...";
            this.menuItem30.Click += new System.EventHandler(this.menuItem30_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 9;
            this.menuItem15.Text = "-";
            // 
            // menuItem16
            // 
            this.vistaMenu.SetImage(this.menuItem16, global::Text_Editor.Properties.Resources.baseline_select_all_black_24dp);
            this.menuItem16.Index = 10;
            this.menuItem16.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.menuItem16.Text = "Select &All";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem42
            // 
            this.menuItem42.Index = 11;
            this.menuItem42.Text = "-";
            // 
            // menuItem43
            // 
            this.vistaMenu.SetImage(this.menuItem43, global::Text_Editor.Properties.Resources.baseline_settings_black_24dp);
            this.menuItem43.Index = 12;
            this.menuItem43.Text = "&Preferences";
            this.menuItem43.Click += new System.EventHandler(this.menuItem43_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 2;
            this.menuItem17.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem32,
            this.menuItem33,
            this.menuItem34,
            this.menuItem31,
            this.menuItem18,
            this.menuItem29});
            this.menuItem17.Text = "&View";
            // 
            // menuItem32
            // 
            this.vistaMenu.SetImage(this.menuItem32, global::Text_Editor.Properties.Resources.baseline_zoom_in_black_24dp);
            this.menuItem32.Index = 0;
            this.menuItem32.Text = "Zoom &In";
            this.menuItem32.Click += new System.EventHandler(this.menuItem32_Click);
            // 
            // menuItem33
            // 
            this.vistaMenu.SetImage(this.menuItem33, global::Text_Editor.Properties.Resources.baseline_zoom_out_black_24dp);
            this.menuItem33.Index = 1;
            this.menuItem33.Text = "Zoom &Out";
            this.menuItem33.Click += new System.EventHandler(this.menuItem33_Click);
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 2;
            this.menuItem34.Shortcut = System.Windows.Forms.Shortcut.Ctrl0;
            this.menuItem34.Text = "&Restore Default Zoom";
            this.menuItem34.Click += new System.EventHandler(this.menuItem34_Click);
            // 
            // menuItem31
            // 
            this.menuItem31.Index = 3;
            this.menuItem31.Text = "-";
            // 
            // menuItem18
            // 
            this.vistaMenu.SetImage(this.menuItem18, global::Text_Editor.Properties.Resources.baseline_wrap_text_black_24dp);
            this.menuItem18.Index = 4;
            this.menuItem18.Text = "&Word Wrap";
            this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
            // 
            // menuItem29
            // 
            this.vistaMenu.SetImage(this.menuItem29, global::Text_Editor.Properties.Resources.baseline_text_fields_black_24dp);
            this.menuItem29.Index = 5;
            this.menuItem29.Text = "&Font...";
            this.menuItem29.Click += new System.EventHandler(this.menuItem29_Click);
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 3;
            this.menuItem23.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem27,
            this.menuItem28,
            this.menuItem24});
            this.menuItem23.Text = "&Help";
            // 
            // menuItem27
            // 
            this.vistaMenu.SetImage(this.menuItem27, global::Text_Editor.Properties.Resources.baseline_help_black_24dp);
            this.menuItem27.Index = 0;
            this.menuItem27.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItem27.Text = "&Help Topics";
            this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 1;
            this.menuItem28.Text = "-";
            // 
            // menuItem24
            // 
            this.vistaMenu.SetImage(this.menuItem24, global::Text_Editor.Properties.Resources.baseline_info_black_24dp);
            this.menuItem24.Index = 2;
            this.menuItem24.Shortcut = System.Windows.Forms.Shortcut.ShiftF1;
            this.menuItem24.Text = "&About Notepad.NET";
            this.menuItem24.Click += new System.EventHandler(this.menuItem24_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mainEditor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 457);
            this.panel1.TabIndex = 0;
            // 
            // mainEditor
            // 
            this.mainEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainEditor.DetectUrls = false;
            this.mainEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainEditor.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainEditor.Location = new System.Drawing.Point(0, 0);
            this.mainEditor.Name = "mainEditor";
            this.mainEditor.Size = new System.Drawing.Size(800, 457);
            this.mainEditor.TabIndex = 0;
            this.mainEditor.Text = "";
            this.mainEditor.WordWrap = false;
            this.mainEditor.TextChanged += new System.EventHandler(this.mainEditor_TextChanged);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem20,
            this.menuItem21,
            this.menuItem22,
            this.menuItem35,
            this.menuItem36,
            this.menuItem37,
            this.menuItem38,
            this.menuItem39});
            // 
            // menuItem20
            // 
            this.vistaMenu.SetImage(this.menuItem20, global::Text_Editor.Properties.Resources.baseline_undo_black_24dp);
            this.menuItem20.Index = 0;
            this.menuItem20.Text = "&Undo";
            this.menuItem20.Click += new System.EventHandler(this.menuItem10_Click);
            // 
            // menuItem21
            // 
            this.vistaMenu.SetImage(this.menuItem21, global::Text_Editor.Properties.Resources.baseline_redo_black_24dp);
            this.menuItem21.Index = 1;
            this.menuItem21.Text = "&Redo";
            this.menuItem21.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 2;
            this.menuItem22.Text = "-";
            // 
            // menuItem35
            // 
            this.vistaMenu.SetImage(this.menuItem35, global::Text_Editor.Properties.Resources.baseline_content_cut_black_24dp);
            this.menuItem35.Index = 3;
            this.menuItem35.Text = "Cu&t";
            this.menuItem35.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem36
            // 
            this.vistaMenu.SetImage(this.menuItem36, global::Text_Editor.Properties.Resources.baseline_content_copy_black_24dp);
            this.menuItem36.Index = 4;
            this.menuItem36.Text = "&Copy";
            this.menuItem36.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem37
            // 
            this.vistaMenu.SetImage(this.menuItem37, global::Text_Editor.Properties.Resources.baseline_content_paste_black_24dp);
            this.menuItem37.Index = 5;
            this.menuItem37.Text = "&Paste";
            this.menuItem37.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem38
            // 
            this.menuItem38.Index = 6;
            this.menuItem38.Text = "-";
            // 
            // menuItem39
            // 
            this.vistaMenu.SetImage(this.menuItem39, global::Text_Editor.Properties.Resources.baseline_select_all_black_24dp);
            this.menuItem39.Index = 7;
            this.menuItem39.Text = "Select &All";
            this.menuItem39.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // vistaMenu
            // 
            this.vistaMenu.ContainerControl = this;
            // 
            // enableDisableTimer
            // 
            this.enableDisableTimer.Tick += new System.EventHandler(this.enableDisableTimer_Tick);
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_EndPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 457);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(0, 496);
            this.Name = "Form1";
            this.Text = "Untitled - Notepad.NET";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vistaMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem23;
        private System.Windows.Forms.MenuItem menuItem24;
        private System.Windows.Forms.MenuItem menuItem25;
        private System.Windows.Forms.MenuItem menuItem27;
        private System.Windows.Forms.MenuItem menuItem28;
        private System.Windows.Forms.MenuItem menuItem29;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem20;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem22;
        private wyDay.Controls.VistaMenu vistaMenu;
        public FixedRichTextBox mainEditor;
        private System.Windows.Forms.MenuItem menuItem26;
        private System.Windows.Forms.MenuItem menuItem30;
        private System.Windows.Forms.MenuItem menuItem31;
        private System.Windows.Forms.MenuItem menuItem32;
        private System.Windows.Forms.MenuItem menuItem33;
        private System.Windows.Forms.MenuItem menuItem34;
        private System.Windows.Forms.MenuItem menuItem35;
        private System.Windows.Forms.MenuItem menuItem36;
        private System.Windows.Forms.MenuItem menuItem37;
        private System.Windows.Forms.MenuItem menuItem38;
        private System.Windows.Forms.MenuItem menuItem39;
        private System.Windows.Forms.Timer enableDisableTimer;
        private System.Windows.Forms.MenuItem menuItem40;
        private System.Windows.Forms.MenuItem menuItem41;
        private System.Windows.Forms.MenuItem menuClear;
        private System.Windows.Forms.MenuItem menuItem42;
        private System.Windows.Forms.MenuItem menuItem43;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.MenuItem menuItem44;
        private System.Windows.Forms.MenuItem menuItem45;
        private System.Windows.Forms.MenuItem menuItem46;
    }
}

