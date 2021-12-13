namespace FolderRenamer
{
    partial class mainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbRecursiveFolders = new System.Windows.Forms.CheckBox();
            this.chbFolderRename = new System.Windows.Forms.CheckBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.chbFileRename = new System.Windows.Forms.CheckBox();
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.imageListIcon = new System.Windows.Forms.ImageList(this.components);
            this.btnProcess = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer12 = new System.Windows.Forms.SplitContainer();
            this.splitContainer13 = new System.Windows.Forms.SplitContainer();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnPairSub = new System.Windows.Forms.Button();
            this.btnCreateFolders = new System.Windows.Forms.Button();
            this.chkAgressive = new System.Windows.Forms.CheckBox();
            this.tbCatalogs = new System.Windows.Forms.TabPage();
            this.splitContainer19 = new System.Windows.Forms.SplitContainer();
            this.splitContainer15 = new System.Windows.Forms.SplitContainer();
            this.tvCatalogs = new System.Windows.Forms.TreeView();
            this.splitContainer16 = new System.Windows.Forms.SplitContainer();
            this.pptGridCatalogs = new PropertyGridEx.PropertyGridEx();
            this.splitContainer17 = new System.Windows.Forms.SplitContainer();
            this.dtGridCatalogs = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.splitContainer18 = new System.Windows.Forms.SplitContainer();
            this.pictPosterCatalogs = new System.Windows.Forms.PictureBox();
            this.pictBackdropCatalogs = new System.Windows.Forms.PictureBox();
            this.lblRec = new System.Windows.Forms.Label();
            this.chkTmdbDo = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabCatlog = new System.Windows.Forms.TabPage();
            this.splitCatlog = new System.Windows.Forms.SplitContainer();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.tvFoldersCat = new System.Windows.Forms.TreeView();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.splitContainer9 = new System.Windows.Forms.SplitContainer();
            this.pptGrdDetailData = new PropertyGridEx.PropertyGridEx();
            this.tabDetail = new System.Windows.Forms.TabControl();
            this.tabTreeDetail = new System.Windows.Forms.TabPage();
            this.tvJsonDetail = new System.Windows.Forms.TreeView();
            this.tabImages = new System.Windows.Forms.TabPage();
            this.splitContainer14 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewImage = new System.Windows.Forms.DataGridView();
            this.Image = new System.Windows.Forms.DataGridViewImageColumn();
            this.splitContainer10 = new System.Windows.Forms.SplitContainer();
            this.pictPoster = new System.Windows.Forms.PictureBox();
            this.pictBackdrop = new System.Windows.Forms.PictureBox();
            this.btnSaveCatalog = new System.Windows.Forms.Button();
            this.chkTmdb = new System.Windows.Forms.CheckBox();
            this.chkCRC = new System.Windows.Forms.CheckBox();
            this.btnCatlog = new System.Windows.Forms.Button();
            this.catMonitor = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Pattern = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbRecursiveFoldersSearch = new System.Windows.Forms.CheckBox();
            this.btnSearchPatterns = new System.Windows.Forms.Button();
            this.btnAddToFile = new System.Windows.Forms.Button();
            this.btnAddToFolder = new System.Windows.Forms.Button();
            this.btnAddToAll = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lblFile = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.lblAll = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wordsToRemove = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.wordsToRemoveFolder = new System.Windows.Forms.TextBox();
            this.wordsToRemoveFile = new System.Windows.Forms.TextBox();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listFolders = new System.Windows.Forms.ListBox();
            this.cleanNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanNamesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pairSubtitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.searchPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBase = new System.Windows.Forms.Panel();
            this.splitContainer11 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).BeginInit();
            this.splitContainer12.Panel1.SuspendLayout();
            this.splitContainer12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).BeginInit();
            this.splitContainer13.Panel1.SuspendLayout();
            this.splitContainer13.Panel2.SuspendLayout();
            this.splitContainer13.SuspendLayout();
            this.tbCatalogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer19)).BeginInit();
            this.splitContainer19.Panel1.SuspendLayout();
            this.splitContainer19.Panel2.SuspendLayout();
            this.splitContainer19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer15)).BeginInit();
            this.splitContainer15.Panel1.SuspendLayout();
            this.splitContainer15.Panel2.SuspendLayout();
            this.splitContainer15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer16)).BeginInit();
            this.splitContainer16.Panel1.SuspendLayout();
            this.splitContainer16.Panel2.SuspendLayout();
            this.splitContainer16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer17)).BeginInit();
            this.splitContainer17.Panel1.SuspendLayout();
            this.splitContainer17.Panel2.SuspendLayout();
            this.splitContainer17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridCatalogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer18)).BeginInit();
            this.splitContainer18.Panel1.SuspendLayout();
            this.splitContainer18.Panel2.SuspendLayout();
            this.splitContainer18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictPosterCatalogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBackdropCatalogs)).BeginInit();
            this.tabCatlog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCatlog)).BeginInit();
            this.splitCatlog.Panel1.SuspendLayout();
            this.splitCatlog.Panel2.SuspendLayout();
            this.splitCatlog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).BeginInit();
            this.splitContainer9.Panel1.SuspendLayout();
            this.splitContainer9.Panel2.SuspendLayout();
            this.splitContainer9.SuspendLayout();
            this.tabDetail.SuspendLayout();
            this.tabTreeDetail.SuspendLayout();
            this.tabImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer14)).BeginInit();
            this.splitContainer14.Panel1.SuspendLayout();
            this.splitContainer14.Panel2.SuspendLayout();
            this.splitContainer14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).BeginInit();
            this.splitContainer10.Panel1.SuspendLayout();
            this.splitContainer10.Panel2.SuspendLayout();
            this.splitContainer10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictPoster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBackdrop)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).BeginInit();
            this.splitContainer11.Panel1.SuspendLayout();
            this.splitContainer11.Panel2.SuspendLayout();
            this.splitContainer11.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(130, 4);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(62, 20);
            this.txtFolderName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start Folder:";
            // 
            // chbRecursiveFolders
            // 
            this.chbRecursiveFolders.AutoSize = true;
            this.chbRecursiveFolders.Location = new System.Drawing.Point(15, 8);
            this.chbRecursiveFolders.Name = "chbRecursiveFolders";
            this.chbRecursiveFolders.Size = new System.Drawing.Size(111, 17);
            this.chbRecursiveFolders.TabIndex = 2;
            this.chbRecursiveFolders.Text = "Recursive Folders";
            this.chbRecursiveFolders.UseVisualStyleBackColor = true;
            // 
            // chbFolderRename
            // 
            this.chbFolderRename.AutoSize = true;
            this.chbFolderRename.Location = new System.Drawing.Point(15, 27);
            this.chbFolderRename.Name = "chbFolderRename";
            this.chbFolderRename.Size = new System.Drawing.Size(98, 17);
            this.chbFolderRename.TabIndex = 3;
            this.chbFolderRename.Text = "Folder Rename";
            this.chbFolderRename.UseVisualStyleBackColor = true;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(225, 3);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(75, 22);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.Text = "Select";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // chbFileRename
            // 
            this.chbFileRename.AutoSize = true;
            this.chbFileRename.Location = new System.Drawing.Point(15, 46);
            this.chbFileRename.Name = "chbFileRename";
            this.chbFileRename.Size = new System.Drawing.Size(85, 17);
            this.chbFileRename.TabIndex = 5;
            this.chbFileRename.Text = "File Rename";
            this.chbFileRename.UseVisualStyleBackColor = true;
            // 
            // tvFolders
            // 
            this.tvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFolders.ImageIndex = 0;
            this.tvFolders.ImageList = this.imageListIcon;
            this.tvFolders.Location = new System.Drawing.Point(0, 0);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageIndex = 3;
            this.tvFolders.Size = new System.Drawing.Size(764, 265);
            this.tvFolders.TabIndex = 6;
            // 
            // imageListIcon
            // 
            this.imageListIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcon.ImageStream")));
            this.imageListIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcon.Images.SetKeyName(0, "__DISK__");
            this.imageListIcon.Images.SetKeyName(1, "__FOLDER__");
            this.imageListIcon.Images.SetKeyName(2, "__MEDIA__");
            this.imageListIcon.Images.SetKeyName(3, "__SUB__");
            this.imageListIcon.Images.SetKeyName(4, "__OTHER__");
            this.imageListIcon.Images.SetKeyName(5, "__DISK_SELECTED__");
            this.imageListIcon.Images.SetKeyName(6, "__FOLDER_SELECTED__");
            this.imageListIcon.Images.SetKeyName(7, "__MEDIA_SELECTED__");
            this.imageListIcon.Images.SetKeyName(8, "__SUB_SELECTED__");
            this.imageListIcon.Images.SetKeyName(9, "__OTHER_SELECTED__");
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(15, 88);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(116, 23);
            this.btnProcess.TabIndex = 7;
            this.btnProcess.Text = "Clean Names";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tbCatalogs);
            this.tabControl1.Controls.Add(this.tabCatlog);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(950, 326);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer12);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(942, 300);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Process";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer12
            // 
            this.splitContainer12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer12.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer12.IsSplitterFixed = true;
            this.splitContainer12.Location = new System.Drawing.Point(3, 3);
            this.splitContainer12.Name = "splitContainer12";
            this.splitContainer12.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer12.Panel1
            // 
            this.splitContainer12.Panel1.Controls.Add(this.splitContainer13);
            // 
            // splitContainer12.Panel2
            // 
            this.splitContainer12.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer12.Panel2MinSize = 20;
            this.splitContainer12.Size = new System.Drawing.Size(936, 294);
            this.splitContainer12.SplitterDistance = 265;
            this.splitContainer12.TabIndex = 14;
            // 
            // splitContainer13
            // 
            this.splitContainer13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer13.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer13.IsSplitterFixed = true;
            this.splitContainer13.Location = new System.Drawing.Point(0, 0);
            this.splitContainer13.Name = "splitContainer13";
            // 
            // splitContainer13.Panel1
            // 
            this.splitContainer13.Panel1.Controls.Add(this.tvFolders);
            // 
            // splitContainer13.Panel2
            // 
            this.splitContainer13.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer13.Panel2.Controls.Add(this.btnAbout);
            this.splitContainer13.Panel2.Controls.Add(this.btnProcess);
            this.splitContainer13.Panel2.Controls.Add(this.btnPairSub);
            this.splitContainer13.Panel2.Controls.Add(this.btnCreateFolders);
            this.splitContainer13.Panel2.Controls.Add(this.chkAgressive);
            this.splitContainer13.Panel2.Controls.Add(this.chbFileRename);
            this.splitContainer13.Panel2.Controls.Add(this.chbRecursiveFolders);
            this.splitContainer13.Panel2.Controls.Add(this.chbFolderRename);
            this.splitContainer13.Size = new System.Drawing.Size(936, 265);
            this.splitContainer13.SplitterDistance = 764;
            this.splitContainer13.TabIndex = 7;
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnAbout.ForeColor = System.Drawing.Color.White;
            this.btnAbout.Location = new System.Drawing.Point(15, 214);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(53, 23);
            this.btnAbout.TabIndex = 13;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Visible = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnPairSub
            // 
            this.btnPairSub.Location = new System.Drawing.Point(15, 146);
            this.btnPairSub.Name = "btnPairSub";
            this.btnPairSub.Size = new System.Drawing.Size(116, 23);
            this.btnPairSub.TabIndex = 12;
            this.btnPairSub.Text = "Pair Subtitles";
            this.btnPairSub.UseVisualStyleBackColor = true;
            this.btnPairSub.Visible = false;
            this.btnPairSub.Click += new System.EventHandler(this.btnPairSub_Click);
            // 
            // btnCreateFolders
            // 
            this.btnCreateFolders.Location = new System.Drawing.Point(15, 117);
            this.btnCreateFolders.Name = "btnCreateFolders";
            this.btnCreateFolders.Size = new System.Drawing.Size(116, 23);
            this.btnCreateFolders.TabIndex = 11;
            this.btnCreateFolders.Text = "Create Folders";
            this.btnCreateFolders.UseVisualStyleBackColor = true;
            this.btnCreateFolders.Click += new System.EventHandler(this.btnCreateFolders_Click);
            // 
            // chkAgressive
            // 
            this.chkAgressive.AutoSize = true;
            this.chkAgressive.Location = new System.Drawing.Point(15, 65);
            this.chkAgressive.Name = "chkAgressive";
            this.chkAgressive.Size = new System.Drawing.Size(102, 17);
            this.chkAgressive.TabIndex = 15;
            this.chkAgressive.Text = "Agressive Mode";
            this.chkAgressive.UseVisualStyleBackColor = true;
            // 
            // tbCatalogs
            // 
            this.tbCatalogs.Controls.Add(this.splitContainer19);
            this.tbCatalogs.Location = new System.Drawing.Point(4, 22);
            this.tbCatalogs.Name = "tbCatalogs";
            this.tbCatalogs.Padding = new System.Windows.Forms.Padding(3);
            this.tbCatalogs.Size = new System.Drawing.Size(942, 300);
            this.tbCatalogs.TabIndex = 4;
            this.tbCatalogs.Text = "Catalogs";
            this.tbCatalogs.UseVisualStyleBackColor = true;
            // 
            // splitContainer19
            // 
            this.splitContainer19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer19.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer19.IsSplitterFixed = true;
            this.splitContainer19.Location = new System.Drawing.Point(3, 3);
            this.splitContainer19.Name = "splitContainer19";
            // 
            // splitContainer19.Panel1
            // 
            this.splitContainer19.Panel1.Controls.Add(this.splitContainer15);
            // 
            // splitContainer19.Panel2
            // 
            this.splitContainer19.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer19.Panel2.Controls.Add(this.lblRec);
            this.splitContainer19.Panel2.Controls.Add(this.chkTmdbDo);
            this.splitContainer19.Panel2.Controls.Add(this.button3);
            this.splitContainer19.Panel2.Controls.Add(this.button1);
            this.splitContainer19.Size = new System.Drawing.Size(936, 294);
            this.splitContainer19.SplitterDistance = 822;
            this.splitContainer19.TabIndex = 1;
            // 
            // splitContainer15
            // 
            this.splitContainer15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer15.Location = new System.Drawing.Point(0, 0);
            this.splitContainer15.Name = "splitContainer15";
            // 
            // splitContainer15.Panel1
            // 
            this.splitContainer15.Panel1.Controls.Add(this.tvCatalogs);
            // 
            // splitContainer15.Panel2
            // 
            this.splitContainer15.Panel2.Controls.Add(this.splitContainer16);
            this.splitContainer15.Size = new System.Drawing.Size(822, 294);
            this.splitContainer15.SplitterDistance = 274;
            this.splitContainer15.TabIndex = 0;
            // 
            // tvCatalogs
            // 
            this.tvCatalogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCatalogs.ImageIndex = 0;
            this.tvCatalogs.ImageList = this.imageListIcon;
            this.tvCatalogs.Location = new System.Drawing.Point(0, 0);
            this.tvCatalogs.Name = "tvCatalogs";
            this.tvCatalogs.SelectedImageIndex = 0;
            this.tvCatalogs.Size = new System.Drawing.Size(274, 294);
            this.tvCatalogs.TabIndex = 0;
            this.tvCatalogs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCatalogs_AfterSelect);
            this.tvCatalogs.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCatalogs_NodeMouseClick);
            // 
            // splitContainer16
            // 
            this.splitContainer16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer16.Location = new System.Drawing.Point(0, 0);
            this.splitContainer16.Name = "splitContainer16";
            this.splitContainer16.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer16.Panel1
            // 
            this.splitContainer16.Panel1.Controls.Add(this.pptGridCatalogs);
            // 
            // splitContainer16.Panel2
            // 
            this.splitContainer16.Panel2.Controls.Add(this.splitContainer17);
            this.splitContainer16.Size = new System.Drawing.Size(544, 294);
            this.splitContainer16.SplitterDistance = 144;
            this.splitContainer16.TabIndex = 3;
            // 
            // pptGridCatalogs
            // 
            // 
            // 
            // 
            this.pptGridCatalogs.DocCommentDescription.AccessibleName = "";
            this.pptGridCatalogs.DocCommentDescription.AutoEllipsis = true;
            this.pptGridCatalogs.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.pptGridCatalogs.DocCommentDescription.Location = new System.Drawing.Point(3, 18);
            this.pptGridCatalogs.DocCommentDescription.Name = "";
            this.pptGridCatalogs.DocCommentDescription.Size = new System.Drawing.Size(0, 52);
            this.pptGridCatalogs.DocCommentDescription.TabIndex = 1;
            this.pptGridCatalogs.DocCommentImage = null;
            // 
            // 
            // 
            this.pptGridCatalogs.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.pptGridCatalogs.DocCommentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.pptGridCatalogs.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
            this.pptGridCatalogs.DocCommentTitle.Name = "";
            this.pptGridCatalogs.DocCommentTitle.Size = new System.Drawing.Size(0, 0);
            this.pptGridCatalogs.DocCommentTitle.TabIndex = 0;
            this.pptGridCatalogs.DocCommentTitle.UseMnemonic = false;
            this.pptGridCatalogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pptGridCatalogs.DrawFlatToolbar = true;
            this.pptGridCatalogs.HelpVisible = false;
            this.pptGridCatalogs.Location = new System.Drawing.Point(0, 0);
            this.pptGridCatalogs.Name = "pptGridCatalogs";
            this.pptGridCatalogs.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pptGridCatalogs.ShowCustomPropertiesSet = true;
            this.pptGridCatalogs.Size = new System.Drawing.Size(544, 144);
            this.pptGridCatalogs.TabIndex = 1;
            // 
            // 
            // 
            this.pptGridCatalogs.ToolStrip.AccessibleName = "Barra de Ferramentas";
            this.pptGridCatalogs.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.pptGridCatalogs.ToolStrip.AllowMerge = false;
            this.pptGridCatalogs.ToolStrip.AutoSize = false;
            this.pptGridCatalogs.ToolStrip.CanOverflow = false;
            this.pptGridCatalogs.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.pptGridCatalogs.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.pptGridCatalogs.ToolStrip.Location = new System.Drawing.Point(0, 1);
            this.pptGridCatalogs.ToolStrip.Name = "";
            this.pptGridCatalogs.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.pptGridCatalogs.ToolStrip.Size = new System.Drawing.Size(544, 25);
            this.pptGridCatalogs.ToolStrip.TabIndex = 1;
            this.pptGridCatalogs.ToolStrip.TabStop = true;
            this.pptGridCatalogs.ToolStrip.Text = "PropertyGridToolBar";
            // 
            // splitContainer17
            // 
            this.splitContainer17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer17.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer17.IsSplitterFixed = true;
            this.splitContainer17.Location = new System.Drawing.Point(0, 0);
            this.splitContainer17.Name = "splitContainer17";
            // 
            // splitContainer17.Panel1
            // 
            this.splitContainer17.Panel1.Controls.Add(this.dtGridCatalogs);
            // 
            // splitContainer17.Panel2
            // 
            this.splitContainer17.Panel2.Controls.Add(this.splitContainer18);
            this.splitContainer17.Size = new System.Drawing.Size(544, 146);
            this.splitContainer17.SplitterDistance = 110;
            this.splitContainer17.TabIndex = 3;
            // 
            // dtGridCatalogs
            // 
            this.dtGridCatalogs.AllowUserToAddRows = false;
            this.dtGridCatalogs.AllowUserToDeleteRows = false;
            this.dtGridCatalogs.AllowUserToResizeColumns = false;
            this.dtGridCatalogs.AllowUserToResizeRows = false;
            this.dtGridCatalogs.BackgroundColor = System.Drawing.Color.White;
            this.dtGridCatalogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridCatalogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn1});
            this.dtGridCatalogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridCatalogs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtGridCatalogs.Location = new System.Drawing.Point(0, 0);
            this.dtGridCatalogs.MultiSelect = false;
            this.dtGridCatalogs.Name = "dtGridCatalogs";
            this.dtGridCatalogs.RowHeadersVisible = false;
            this.dtGridCatalogs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtGridCatalogs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGridCatalogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dtGridCatalogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridCatalogs.ShowEditingIcon = false;
            this.dtGridCatalogs.ShowRowErrors = false;
            this.dtGridCatalogs.Size = new System.Drawing.Size(110, 146);
            this.dtGridCatalogs.TabIndex = 0;
            this.dtGridCatalogs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridCatalogs_CellContentClick);
            this.dtGridCatalogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridCatalogs_CellDoubleClick);
            this.dtGridCatalogs.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridCatalogs_CellEnter);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // splitContainer18
            // 
            this.splitContainer18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer18.Location = new System.Drawing.Point(0, 0);
            this.splitContainer18.Name = "splitContainer18";
            // 
            // splitContainer18.Panel1
            // 
            this.splitContainer18.Panel1.Controls.Add(this.pictPosterCatalogs);
            // 
            // splitContainer18.Panel2
            // 
            this.splitContainer18.Panel2.Controls.Add(this.pictBackdropCatalogs);
            this.splitContainer18.Size = new System.Drawing.Size(430, 146);
            this.splitContainer18.SplitterDistance = 202;
            this.splitContainer18.TabIndex = 1;
            // 
            // pictPosterCatalogs
            // 
            this.pictPosterCatalogs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictPosterCatalogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictPosterCatalogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictPosterCatalogs.Location = new System.Drawing.Point(0, 0);
            this.pictPosterCatalogs.Name = "pictPosterCatalogs";
            this.pictPosterCatalogs.Size = new System.Drawing.Size(202, 146);
            this.pictPosterCatalogs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictPosterCatalogs.TabIndex = 0;
            this.pictPosterCatalogs.TabStop = false;
            // 
            // pictBackdropCatalogs
            // 
            this.pictBackdropCatalogs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictBackdropCatalogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictBackdropCatalogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictBackdropCatalogs.Location = new System.Drawing.Point(0, 0);
            this.pictBackdropCatalogs.Name = "pictBackdropCatalogs";
            this.pictBackdropCatalogs.Size = new System.Drawing.Size(224, 146);
            this.pictBackdropCatalogs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictBackdropCatalogs.TabIndex = 1;
            this.pictBackdropCatalogs.TabStop = false;
            // 
            // lblRec
            // 
            this.lblRec.AutoSize = true;
            this.lblRec.Location = new System.Drawing.Point(12, 95);
            this.lblRec.Name = "lblRec";
            this.lblRec.Size = new System.Drawing.Size(0, 13);
            this.lblRec.TabIndex = 8;
            // 
            // chkTmdbDo
            // 
            this.chkTmdbDo.AutoSize = true;
            this.chkTmdbDo.Location = new System.Drawing.Point(13, 71);
            this.chkTmdbDo.Name = "chkTmdbDo";
            this.chkTmdbDo.Size = new System.Drawing.Size(57, 17);
            this.chkTmdbDo.TabIndex = 7;
            this.chkTmdbDo.Text = "TMDB";
            this.chkTmdbDo.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(10, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "RE-PROCESS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OPEN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnLoadCatalogs_Click);
            // 
            // tabCatlog
            // 
            this.tabCatlog.Controls.Add(this.splitCatlog);
            this.tabCatlog.Location = new System.Drawing.Point(4, 22);
            this.tabCatlog.Name = "tabCatlog";
            this.tabCatlog.Padding = new System.Windows.Forms.Padding(3);
            this.tabCatlog.Size = new System.Drawing.Size(942, 300);
            this.tabCatlog.TabIndex = 3;
            this.tabCatlog.Text = "Catlog";
            this.tabCatlog.UseVisualStyleBackColor = true;
            // 
            // splitCatlog
            // 
            this.splitCatlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCatlog.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitCatlog.IsSplitterFixed = true;
            this.splitCatlog.Location = new System.Drawing.Point(3, 3);
            this.splitCatlog.Name = "splitCatlog";
            this.splitCatlog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCatlog.Panel1
            // 
            this.splitCatlog.Panel1.Controls.Add(this.splitContainer7);
            // 
            // splitCatlog.Panel2
            // 
            this.splitCatlog.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitCatlog.Panel2.Controls.Add(this.catMonitor);
            this.splitCatlog.Panel2MinSize = 20;
            this.splitCatlog.Size = new System.Drawing.Size(936, 294);
            this.splitCatlog.SplitterDistance = 265;
            this.splitCatlog.TabIndex = 0;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.tvFoldersCat);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer7.Size = new System.Drawing.Size(936, 265);
            this.splitContainer7.SplitterDistance = 311;
            this.splitContainer7.TabIndex = 8;
            // 
            // tvFoldersCat
            // 
            this.tvFoldersCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFoldersCat.ImageIndex = 3;
            this.tvFoldersCat.ImageList = this.imageListIcon;
            this.tvFoldersCat.ItemHeight = 18;
            this.tvFoldersCat.Location = new System.Drawing.Point(0, 0);
            this.tvFoldersCat.Name = "tvFoldersCat";
            this.tvFoldersCat.SelectedImageIndex = 0;
            this.tvFoldersCat.Size = new System.Drawing.Size(311, 265);
            this.tvFoldersCat.TabIndex = 7;
            this.tvFoldersCat.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFoldersCat_AfterSelect);
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer8.IsSplitterFixed = true;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.splitContainer9);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer8.Panel2.Controls.Add(this.btnSaveCatalog);
            this.splitContainer8.Panel2.Controls.Add(this.chkTmdb);
            this.splitContainer8.Panel2.Controls.Add(this.chkCRC);
            this.splitContainer8.Panel2.Controls.Add(this.btnCatlog);
            this.splitContainer8.Size = new System.Drawing.Size(621, 265);
            this.splitContainer8.SplitterDistance = 507;
            this.splitContainer8.TabIndex = 2;
            // 
            // splitContainer9
            // 
            this.splitContainer9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer9.Location = new System.Drawing.Point(0, 0);
            this.splitContainer9.Name = "splitContainer9";
            this.splitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer9.Panel1
            // 
            this.splitContainer9.Panel1.Controls.Add(this.pptGrdDetailData);
            // 
            // splitContainer9.Panel2
            // 
            this.splitContainer9.Panel2.Controls.Add(this.tabDetail);
            this.splitContainer9.Size = new System.Drawing.Size(507, 265);
            this.splitContainer9.SplitterDistance = 130;
            this.splitContainer9.TabIndex = 2;
            // 
            // pptGrdDetailData
            // 
            // 
            // 
            // 
            this.pptGrdDetailData.DocCommentDescription.AccessibleName = "";
            this.pptGrdDetailData.DocCommentDescription.AutoEllipsis = true;
            this.pptGrdDetailData.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.pptGrdDetailData.DocCommentDescription.Location = new System.Drawing.Point(3, 18);
            this.pptGrdDetailData.DocCommentDescription.Name = "";
            this.pptGrdDetailData.DocCommentDescription.Size = new System.Drawing.Size(0, 52);
            this.pptGrdDetailData.DocCommentDescription.TabIndex = 1;
            this.pptGrdDetailData.DocCommentImage = null;
            // 
            // 
            // 
            this.pptGrdDetailData.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.pptGrdDetailData.DocCommentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.pptGrdDetailData.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
            this.pptGrdDetailData.DocCommentTitle.Name = "";
            this.pptGrdDetailData.DocCommentTitle.Size = new System.Drawing.Size(0, 0);
            this.pptGrdDetailData.DocCommentTitle.TabIndex = 0;
            this.pptGrdDetailData.DocCommentTitle.UseMnemonic = false;
            this.pptGrdDetailData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pptGrdDetailData.DrawFlatToolbar = true;
            this.pptGrdDetailData.HelpVisible = false;
            this.pptGrdDetailData.Location = new System.Drawing.Point(0, 0);
            this.pptGrdDetailData.Name = "pptGrdDetailData";
            this.pptGrdDetailData.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pptGrdDetailData.ShowCustomPropertiesSet = true;
            this.pptGrdDetailData.Size = new System.Drawing.Size(507, 130);
            this.pptGrdDetailData.TabIndex = 1;
            // 
            // 
            // 
            this.pptGrdDetailData.ToolStrip.AccessibleName = "Barra de Ferramentas";
            this.pptGrdDetailData.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.pptGrdDetailData.ToolStrip.AllowMerge = false;
            this.pptGrdDetailData.ToolStrip.AutoSize = false;
            this.pptGrdDetailData.ToolStrip.CanOverflow = false;
            this.pptGrdDetailData.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.pptGrdDetailData.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.pptGrdDetailData.ToolStrip.Location = new System.Drawing.Point(0, 1);
            this.pptGrdDetailData.ToolStrip.Name = "";
            this.pptGrdDetailData.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.pptGrdDetailData.ToolStrip.Size = new System.Drawing.Size(507, 25);
            this.pptGrdDetailData.ToolStrip.TabIndex = 1;
            this.pptGrdDetailData.ToolStrip.TabStop = true;
            this.pptGrdDetailData.ToolStrip.Text = "PropertyGridToolBar";
            this.pptGrdDetailData.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.pptGrdDetailData_SelectedGridItemChanged);
            // 
            // tabDetail
            // 
            this.tabDetail.Controls.Add(this.tabTreeDetail);
            this.tabDetail.Controls.Add(this.tabImages);
            this.tabDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetail.Location = new System.Drawing.Point(0, 0);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.SelectedIndex = 0;
            this.tabDetail.Size = new System.Drawing.Size(507, 131);
            this.tabDetail.TabIndex = 1;
            // 
            // tabTreeDetail
            // 
            this.tabTreeDetail.Controls.Add(this.tvJsonDetail);
            this.tabTreeDetail.Location = new System.Drawing.Point(4, 22);
            this.tabTreeDetail.Name = "tabTreeDetail";
            this.tabTreeDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabTreeDetail.Size = new System.Drawing.Size(499, 105);
            this.tabTreeDetail.TabIndex = 0;
            this.tabTreeDetail.Text = "Detail";
            this.tabTreeDetail.UseVisualStyleBackColor = true;
            // 
            // tvJsonDetail
            // 
            this.tvJsonDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvJsonDetail.Location = new System.Drawing.Point(3, 3);
            this.tvJsonDetail.Name = "tvJsonDetail";
            this.tvJsonDetail.Size = new System.Drawing.Size(493, 99);
            this.tvJsonDetail.TabIndex = 0;
            // 
            // tabImages
            // 
            this.tabImages.Controls.Add(this.splitContainer14);
            this.tabImages.Location = new System.Drawing.Point(4, 22);
            this.tabImages.Name = "tabImages";
            this.tabImages.Padding = new System.Windows.Forms.Padding(3);
            this.tabImages.Size = new System.Drawing.Size(499, 105);
            this.tabImages.TabIndex = 1;
            this.tabImages.Text = "Images";
            this.tabImages.UseVisualStyleBackColor = true;
            // 
            // splitContainer14
            // 
            this.splitContainer14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer14.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer14.IsSplitterFixed = true;
            this.splitContainer14.Location = new System.Drawing.Point(3, 3);
            this.splitContainer14.Name = "splitContainer14";
            // 
            // splitContainer14.Panel1
            // 
            this.splitContainer14.Panel1.Controls.Add(this.dataGridViewImage);
            // 
            // splitContainer14.Panel2
            // 
            this.splitContainer14.Panel2.Controls.Add(this.splitContainer10);
            this.splitContainer14.Size = new System.Drawing.Size(493, 99);
            this.splitContainer14.SplitterDistance = 110;
            this.splitContainer14.TabIndex = 2;
            // 
            // dataGridViewImage
            // 
            this.dataGridViewImage.AllowUserToAddRows = false;
            this.dataGridViewImage.AllowUserToDeleteRows = false;
            this.dataGridViewImage.AllowUserToResizeColumns = false;
            this.dataGridViewImage.AllowUserToResizeRows = false;
            this.dataGridViewImage.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewImage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewImage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Image});
            this.dataGridViewImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewImage.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewImage.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewImage.MultiSelect = false;
            this.dataGridViewImage.Name = "dataGridViewImage";
            this.dataGridViewImage.RowHeadersVisible = false;
            this.dataGridViewImage.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewImage.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewImage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewImage.ShowEditingIcon = false;
            this.dataGridViewImage.ShowRowErrors = false;
            this.dataGridViewImage.Size = new System.Drawing.Size(110, 99);
            this.dataGridViewImage.TabIndex = 0;
            this.dataGridViewImage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewImage_CellContentClick_1);
            this.dataGridViewImage.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewImage_CellClick);
            this.dataGridViewImage.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewImage_CellEnter);
            // 
            // Image
            // 
            this.Image.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Image.HeaderText = "";
            this.Image.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Image.Name = "Image";
            this.Image.ReadOnly = true;
            this.Image.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // splitContainer10
            // 
            this.splitContainer10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer10.Location = new System.Drawing.Point(0, 0);
            this.splitContainer10.Name = "splitContainer10";
            // 
            // splitContainer10.Panel1
            // 
            this.splitContainer10.Panel1.Controls.Add(this.pictPoster);
            // 
            // splitContainer10.Panel2
            // 
            this.splitContainer10.Panel2.Controls.Add(this.pictBackdrop);
            this.splitContainer10.Size = new System.Drawing.Size(379, 99);
            this.splitContainer10.SplitterDistance = 179;
            this.splitContainer10.TabIndex = 1;
            // 
            // pictPoster
            // 
            this.pictPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictPoster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictPoster.Location = new System.Drawing.Point(0, 0);
            this.pictPoster.Name = "pictPoster";
            this.pictPoster.Size = new System.Drawing.Size(179, 99);
            this.pictPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictPoster.TabIndex = 0;
            this.pictPoster.TabStop = false;
            // 
            // pictBackdrop
            // 
            this.pictBackdrop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictBackdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictBackdrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictBackdrop.Location = new System.Drawing.Point(0, 0);
            this.pictBackdrop.Name = "pictBackdrop";
            this.pictBackdrop.Size = new System.Drawing.Size(196, 99);
            this.pictBackdrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictBackdrop.TabIndex = 1;
            this.pictBackdrop.TabStop = false;
            // 
            // btnSaveCatalog
            // 
            this.btnSaveCatalog.Location = new System.Drawing.Point(14, 89);
            this.btnSaveCatalog.Name = "btnSaveCatalog";
            this.btnSaveCatalog.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCatalog.TabIndex = 4;
            this.btnSaveCatalog.Text = "Save";
            this.btnSaveCatalog.UseVisualStyleBackColor = true;
            this.btnSaveCatalog.Click += new System.EventHandler(this.btnSaveCatalog_Click);
            // 
            // chkTmdb
            // 
            this.chkTmdb.AutoSize = true;
            this.chkTmdb.Location = new System.Drawing.Point(14, 66);
            this.chkTmdb.Name = "chkTmdb";
            this.chkTmdb.Size = new System.Drawing.Size(57, 17);
            this.chkTmdb.TabIndex = 3;
            this.chkTmdb.Text = "TMDB";
            this.chkTmdb.UseVisualStyleBackColor = true;
            // 
            // chkCRC
            // 
            this.chkCRC.AutoSize = true;
            this.chkCRC.Location = new System.Drawing.Point(14, 43);
            this.chkCRC.Name = "chkCRC";
            this.chkCRC.Size = new System.Drawing.Size(72, 17);
            this.chkCRC.TabIndex = 2;
            this.chkCRC.Text = "CRC Calc";
            this.chkCRC.UseVisualStyleBackColor = true;
            // 
            // btnCatlog
            // 
            this.btnCatlog.Location = new System.Drawing.Point(14, 14);
            this.btnCatlog.Name = "btnCatlog";
            this.btnCatlog.Size = new System.Drawing.Size(75, 23);
            this.btnCatlog.TabIndex = 0;
            this.btnCatlog.Text = "Catlog";
            this.btnCatlog.UseVisualStyleBackColor = true;
            this.btnCatlog.Click += new System.EventHandler(this.btnCatalog_Click);
            // 
            // catMonitor
            // 
            this.catMonitor.AutoSize = true;
            this.catMonitor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.catMonitor.Location = new System.Drawing.Point(6, 4);
            this.catMonitor.Name = "catMonitor";
            this.catMonitor.Size = new System.Drawing.Size(0, 13);
            this.catMonitor.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(942, 300);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Search Patterns";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.IsSplitterFixed = true;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer5.Size = new System.Drawing.Size(942, 300);
            this.splitContainer5.SplitterDistance = 25;
            this.splitContainer5.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Patterns";
            // 
            // splitContainer6
            // 
            this.splitContainer6.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer6.IsSplitterFixed = true;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer6.Panel2.Controls.Add(this.chbRecursiveFoldersSearch);
            this.splitContainer6.Panel2.Controls.Add(this.btnSearchPatterns);
            this.splitContainer6.Panel2.Controls.Add(this.btnAddToFile);
            this.splitContainer6.Panel2.Controls.Add(this.btnAddToFolder);
            this.splitContainer6.Panel2.Controls.Add(this.btnAddToAll);
            this.splitContainer6.Size = new System.Drawing.Size(942, 271);
            this.splitContainer6.SplitterDistance = 807;
            this.splitContainer6.TabIndex = 0;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.Pattern});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.ShowCellErrors = false;
            this.dataGridView.ShowCellToolTips = false;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.ShowRowErrors = false;
            this.dataGridView.Size = new System.Drawing.Size(807, 271);
            this.dataGridView.TabIndex = 0;
            // 
            // Selected
            // 
            this.Selected.HeaderText = "";
            this.Selected.Name = "Selected";
            this.Selected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Selected.Width = 30;
            // 
            // Pattern
            // 
            this.Pattern.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Pattern.HeaderText = "Pattern";
            this.Pattern.Name = "Pattern";
            this.Pattern.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // chbRecursiveFoldersSearch
            // 
            this.chbRecursiveFoldersSearch.AutoSize = true;
            this.chbRecursiveFoldersSearch.Location = new System.Drawing.Point(14, 127);
            this.chbRecursiveFoldersSearch.Name = "chbRecursiveFoldersSearch";
            this.chbRecursiveFoldersSearch.Size = new System.Drawing.Size(111, 17);
            this.chbRecursiveFoldersSearch.TabIndex = 16;
            this.chbRecursiveFoldersSearch.Text = "Recursive Folders";
            this.chbRecursiveFoldersSearch.UseVisualStyleBackColor = true;
            // 
            // btnSearchPatterns
            // 
            this.btnSearchPatterns.Location = new System.Drawing.Point(13, 95);
            this.btnSearchPatterns.Name = "btnSearchPatterns";
            this.btnSearchPatterns.Size = new System.Drawing.Size(104, 23);
            this.btnSearchPatterns.TabIndex = 15;
            this.btnSearchPatterns.Text = "Search Patterns";
            this.btnSearchPatterns.UseVisualStyleBackColor = true;
            this.btnSearchPatterns.Click += new System.EventHandler(this.btnSearchPatterns_Click_1);
            // 
            // btnAddToFile
            // 
            this.btnAddToFile.Location = new System.Drawing.Point(13, 66);
            this.btnAddToFile.Name = "btnAddToFile";
            this.btnAddToFile.Size = new System.Drawing.Size(104, 23);
            this.btnAddToFile.TabIndex = 11;
            this.btnAddToFile.Text = "Add to File";
            this.btnAddToFile.UseVisualStyleBackColor = true;
            this.btnAddToFile.Click += new System.EventHandler(this.btnAddToFile_Click);
            // 
            // btnAddToFolder
            // 
            this.btnAddToFolder.Location = new System.Drawing.Point(13, 37);
            this.btnAddToFolder.Name = "btnAddToFolder";
            this.btnAddToFolder.Size = new System.Drawing.Size(104, 23);
            this.btnAddToFolder.TabIndex = 10;
            this.btnAddToFolder.Text = "Add to Folder";
            this.btnAddToFolder.UseVisualStyleBackColor = true;
            this.btnAddToFolder.Click += new System.EventHandler(this.btnAddToFolder_Click);
            // 
            // btnAddToAll
            // 
            this.btnAddToAll.Location = new System.Drawing.Point(13, 8);
            this.btnAddToAll.Name = "btnAddToAll";
            this.btnAddToAll.Size = new System.Drawing.Size(104, 23);
            this.btnAddToAll.TabIndex = 9;
            this.btnAddToAll.Text = "Add to All";
            this.btnAddToAll.UseVisualStyleBackColor = true;
            this.btnAddToAll.Click += new System.EventHandler(this.btnAddToAll_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(942, 300);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Patterns";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lblFile);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.lblFolder);
            this.splitContainer3.Panel1.Controls.Add(this.lblAll);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(936, 294);
            this.splitContainer3.SplitterDistance = 25;
            this.splitContainer3.TabIndex = 10;
            this.splitContainer3.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer3_SplitterMoved);
            this.splitContainer3.ClientSizeChanged += new System.EventHandler(this.splitContainer3_ClientSizeChanged);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.BackColor = System.Drawing.Color.DarkRed;
            this.lblFile.ForeColor = System.Drawing.Color.White;
            this.lblFile.Location = new System.Drawing.Point(202, 8);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(23, 13);
            this.lblFile.TabIndex = 17;
            this.lblFile.Text = "File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Words to Remove";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.BackColor = System.Drawing.Color.DarkRed;
            this.lblFolder.ForeColor = System.Drawing.Color.White;
            this.lblFolder.Location = new System.Drawing.Point(160, 8);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(36, 13);
            this.lblFolder.TabIndex = 16;
            this.lblFolder.Text = "Folder";
            // 
            // lblAll
            // 
            this.lblAll.AutoSize = true;
            this.lblAll.BackColor = System.Drawing.Color.DarkRed;
            this.lblAll.ForeColor = System.Drawing.Color.White;
            this.lblAll.Location = new System.Drawing.Point(136, 8);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(18, 13);
            this.lblAll.TabIndex = 15;
            this.lblAll.Text = "All";
            // 
            // splitContainer4
            // 
            this.splitContainer4.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer4.Panel2.Controls.Add(this.lblMonitor);
            this.splitContainer4.Panel2.Controls.Add(this.btnSort);
            this.splitContainer4.Panel2.Controls.Add(this.btnSave);
            this.splitContainer4.Size = new System.Drawing.Size(936, 265);
            this.splitContainer4.SplitterDistance = 847;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer4_SplitterMoved);
            this.splitContainer4.ClientSizeChanged += new System.EventHandler(this.splitContainer4_ClientSizeChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.wordsToRemove);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(847, 265);
            this.splitContainer1.SplitterDistance = 247;
            this.splitContainer1.TabIndex = 9;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer1.ClientSizeChanged += new System.EventHandler(this.splitContainer1_ClientSizeChanged);
            // 
            // wordsToRemove
            // 
            this.wordsToRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wordsToRemove.Location = new System.Drawing.Point(0, 0);
            this.wordsToRemove.Multiline = true;
            this.wordsToRemove.Name = "wordsToRemove";
            this.wordsToRemove.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.wordsToRemove.Size = new System.Drawing.Size(247, 265);
            this.wordsToRemove.TabIndex = 0;
            this.wordsToRemove.WordWrap = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.wordsToRemoveFolder);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.wordsToRemoveFile);
            this.splitContainer2.Size = new System.Drawing.Size(596, 265);
            this.splitContainer2.SplitterDistance = 289;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            this.splitContainer2.ClientSizeChanged += new System.EventHandler(this.splitContainer2_ClientSizeChanged);
            // 
            // wordsToRemoveFolder
            // 
            this.wordsToRemoveFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wordsToRemoveFolder.Location = new System.Drawing.Point(0, 0);
            this.wordsToRemoveFolder.Multiline = true;
            this.wordsToRemoveFolder.Name = "wordsToRemoveFolder";
            this.wordsToRemoveFolder.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.wordsToRemoveFolder.Size = new System.Drawing.Size(289, 265);
            this.wordsToRemoveFolder.TabIndex = 1;
            this.wordsToRemoveFolder.WordWrap = false;
            // 
            // wordsToRemoveFile
            // 
            this.wordsToRemoveFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wordsToRemoveFile.Location = new System.Drawing.Point(0, 0);
            this.wordsToRemoveFile.Multiline = true;
            this.wordsToRemoveFile.Name = "wordsToRemoveFile";
            this.wordsToRemoveFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.wordsToRemoveFile.Size = new System.Drawing.Size(303, 265);
            this.wordsToRemoveFile.TabIndex = 1;
            this.wordsToRemoveFile.WordWrap = false;
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(196, 14);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(0, 13);
            this.lblMonitor.TabIndex = 10;
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(9, 39);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(64, 23);
            this.btnSort.TabIndex = 9;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(9, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(259, 29);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(23, 69);
            this.btnClearList.TabIndex = 10;
            this.btnClearList.Text = "-";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(198, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(21, 22);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listFolders
            // 
            this.listFolders.FormattingEnabled = true;
            this.listFolders.Location = new System.Drawing.Point(130, 29);
            this.listFolders.Name = "listFolders";
            this.listFolders.Size = new System.Drawing.Size(123, 69);
            this.listFolders.TabIndex = 8;
            this.listFolders.DoubleClick += new System.EventHandler(this.listFolders_DoubleClick);
            // 
            // cleanNamesToolStripMenuItem
            // 
            this.cleanNamesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanNamesToolStripMenuItem1,
            this.createFoldersToolStripMenuItem,
            this.pairSubtitleToolStripMenuItem,
            this.toolStripSeparator2,
            this.searchPatternToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.cleanNamesToolStripMenuItem.Name = "cleanNamesToolStripMenuItem";
            this.cleanNamesToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.cleanNamesToolStripMenuItem.Text = "File";
            // 
            // cleanNamesToolStripMenuItem1
            // 
            this.cleanNamesToolStripMenuItem1.Name = "cleanNamesToolStripMenuItem1";
            this.cleanNamesToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.cleanNamesToolStripMenuItem1.Text = "Clean Names";
            this.cleanNamesToolStripMenuItem1.Click += new System.EventHandler(this.cleanNamesToolStripMenuItem1_Click);
            // 
            // createFoldersToolStripMenuItem
            // 
            this.createFoldersToolStripMenuItem.Name = "createFoldersToolStripMenuItem";
            this.createFoldersToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.createFoldersToolStripMenuItem.Text = "Create Folders";
            this.createFoldersToolStripMenuItem.Click += new System.EventHandler(this.createFoldersToolStripMenuItem_Click);
            // 
            // pairSubtitleToolStripMenuItem
            // 
            this.pairSubtitleToolStripMenuItem.Name = "pairSubtitleToolStripMenuItem";
            this.pairSubtitleToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.pairSubtitleToolStripMenuItem.Text = "Pair Subtitle";
            this.pairSubtitleToolStripMenuItem.Visible = false;
            this.pairSubtitleToolStripMenuItem.Click += new System.EventHandler(this.pairSubtitleToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // searchPatternToolStripMenuItem
            // 
            this.searchPatternToolStripMenuItem.Name = "searchPatternToolStripMenuItem";
            this.searchPatternToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.searchPatternToolStripMenuItem.Text = "Search Pattern";
            this.searchPatternToolStripMenuItem.Click += new System.EventHandler(this.searchPatternToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.aboutToolStripMenuItem.Text = "Exit";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanNamesToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(950, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // panelBase
            // 
            this.panelBase.Controls.Add(this.tabControl1);
            this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBase.Location = new System.Drawing.Point(0, 0);
            this.panelBase.Name = "panelBase";
            this.panelBase.Size = new System.Drawing.Size(950, 326);
            this.panelBase.TabIndex = 15;
            // 
            // splitContainer11
            // 
            this.splitContainer11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer11.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer11.IsSplitterFixed = true;
            this.splitContainer11.Location = new System.Drawing.Point(0, 24);
            this.splitContainer11.Name = "splitContainer11";
            this.splitContainer11.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer11.Panel1
            // 
            this.splitContainer11.Panel1.Controls.Add(this.btnSelectFolder);
            this.splitContainer11.Panel1.Controls.Add(this.btnClearList);
            this.splitContainer11.Panel1.Controls.Add(this.label1);
            this.splitContainer11.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer11.Panel1.Controls.Add(this.txtFolderName);
            this.splitContainer11.Panel1.Controls.Add(this.listFolders);
            // 
            // splitContainer11.Panel2
            // 
            this.splitContainer11.Panel2.Controls.Add(this.panelBase);
            this.splitContainer11.Size = new System.Drawing.Size(950, 437);
            this.splitContainer11.SplitterDistance = 107;
            this.splitContainer11.TabIndex = 16;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 461);
            this.Controls.Add(this.splitContainer11);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "mainForm";
            this.Text = "Folder Renamer";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer12.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).EndInit();
            this.splitContainer12.ResumeLayout(false);
            this.splitContainer13.Panel1.ResumeLayout(false);
            this.splitContainer13.Panel2.ResumeLayout(false);
            this.splitContainer13.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).EndInit();
            this.splitContainer13.ResumeLayout(false);
            this.tbCatalogs.ResumeLayout(false);
            this.splitContainer19.Panel1.ResumeLayout(false);
            this.splitContainer19.Panel2.ResumeLayout(false);
            this.splitContainer19.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer19)).EndInit();
            this.splitContainer19.ResumeLayout(false);
            this.splitContainer15.Panel1.ResumeLayout(false);
            this.splitContainer15.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer15)).EndInit();
            this.splitContainer15.ResumeLayout(false);
            this.splitContainer16.Panel1.ResumeLayout(false);
            this.splitContainer16.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer16)).EndInit();
            this.splitContainer16.ResumeLayout(false);
            this.splitContainer17.Panel1.ResumeLayout(false);
            this.splitContainer17.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer17)).EndInit();
            this.splitContainer17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridCatalogs)).EndInit();
            this.splitContainer18.Panel1.ResumeLayout(false);
            this.splitContainer18.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer18)).EndInit();
            this.splitContainer18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictPosterCatalogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBackdropCatalogs)).EndInit();
            this.tabCatlog.ResumeLayout(false);
            this.splitCatlog.Panel1.ResumeLayout(false);
            this.splitCatlog.Panel2.ResumeLayout(false);
            this.splitCatlog.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCatlog)).EndInit();
            this.splitCatlog.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.splitContainer9.Panel1.ResumeLayout(false);
            this.splitContainer9.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).EndInit();
            this.splitContainer9.ResumeLayout(false);
            this.tabDetail.ResumeLayout(false);
            this.tabTreeDetail.ResumeLayout(false);
            this.tabImages.ResumeLayout(false);
            this.splitContainer14.Panel1.ResumeLayout(false);
            this.splitContainer14.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer14)).EndInit();
            this.splitContainer14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewImage)).EndInit();
            this.splitContainer10.Panel1.ResumeLayout(false);
            this.splitContainer10.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).EndInit();
            this.splitContainer10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictPoster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBackdrop)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelBase.ResumeLayout(false);
            this.splitContainer11.Panel1.ResumeLayout(false);
            this.splitContainer11.Panel1.PerformLayout();
            this.splitContainer11.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).EndInit();
            this.splitContainer11.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbRecursiveFolders;
        private System.Windows.Forms.CheckBox chbFolderRename;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.CheckBox chbFileRename;
        private System.Windows.Forms.TreeView tvFolders;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox wordsToRemove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox listFolders;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox wordsToRemoveFolder;
        private System.Windows.Forms.TextBox wordsToRemoveFile;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Label lblAll;
        private System.Windows.Forms.Button btnCreateFolders;
        private System.Windows.Forms.Button btnPairSub;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cleanNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanNamesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem createFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pairSubtitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel panelBase;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem searchPatternToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pattern;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnAddToFile;
        private System.Windows.Forms.Button btnAddToFolder;
        private System.Windows.Forms.Button btnAddToAll;
        private System.Windows.Forms.CheckBox chkAgressive;
        private System.Windows.Forms.Button btnSearchPatterns;
        private System.Windows.Forms.TabPage tabCatlog;
        private System.Windows.Forms.SplitContainer splitCatlog;
        private System.Windows.Forms.Button btnCatlog;
        private System.Windows.Forms.TreeView tvFoldersCat;
        private System.Windows.Forms.Label catMonitor;
        private System.Windows.Forms.ImageList imageListIcon;
        private System.Windows.Forms.CheckBox chkCRC;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private PropertyGridEx.PropertyGridEx pptGrdDetailData;
        private System.Windows.Forms.SplitContainer splitContainer9;
        private System.Windows.Forms.TreeView tvJsonDetail;
        private System.Windows.Forms.CheckBox chkTmdb;
        private System.Windows.Forms.TabControl tabDetail;
        private System.Windows.Forms.TabPage tabTreeDetail;
        private System.Windows.Forms.TabPage tabImages;
        private System.Windows.Forms.SplitContainer splitContainer10;
        private System.Windows.Forms.PictureBox pictPoster;
        private System.Windows.Forms.PictureBox pictBackdrop;
        private System.Windows.Forms.SplitContainer splitContainer12;
        private System.Windows.Forms.SplitContainer splitContainer11;
        private System.Windows.Forms.SplitContainer splitContainer13;
        private System.Windows.Forms.CheckBox chbRecursiveFoldersSearch;
        private System.Windows.Forms.SplitContainer splitContainer14;
        private System.Windows.Forms.DataGridView dataGridViewImage;
        private System.Windows.Forms.DataGridViewImageColumn Image;
        private System.Windows.Forms.Button btnSaveCatalog;
        private System.Windows.Forms.TabPage tbCatalogs;
        private System.Windows.Forms.SplitContainer splitContainer15;
        private System.Windows.Forms.TreeView tvCatalogs;
        private System.Windows.Forms.SplitContainer splitContainer16;
        private PropertyGridEx.PropertyGridEx pptGridCatalogs;
        private System.Windows.Forms.SplitContainer splitContainer17;
        private System.Windows.Forms.DataGridView dtGridCatalogs;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.SplitContainer splitContainer18;
        private System.Windows.Forms.PictureBox pictPosterCatalogs;
        private System.Windows.Forms.PictureBox pictBackdropCatalogs;
        private System.Windows.Forms.SplitContainer splitContainer19;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chkTmdbDo;
        private System.Windows.Forms.Label lblRec;
    }
}

