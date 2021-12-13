using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.TMDb;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using TestDLApp.Utilities.Extensions;
using Microsoft.VisualBasic;
using SQLite;

namespace FolderRenamer
{

    public partial class mainForm : Form
    {


        #region Properties

        public List<String> ToRemoveAll = new List<string>();
        public List<String> ToRemoveFolder = new List<string>();
        public List<String> ToRemoveFile = new List<string>();
        //public List<VideoFile> VideoFiles = new List<VideoFile>();
        //public List<SubtitleFile> SubtitleFiles = new List<SubtitleFile>();
        //public List<VideoFolder> VideoFolders = new List<VideoFolder>();
        public List<String> Patterns = new List<string>();
        public int maxLengthForPattern = 50;
        public Boolean SilentMode = false;
        public string SqLiteFile = "";
        string[] allowedVideos;
        string[] allowedSub;
        public Catalog Catalog = new Catalog();
        public CatalogItem ReadingData = new CatalogItem();
        List<MovieImage> imageListPosters = new List<MovieImage>();
        List<MovieImage> imageListBackdrop = new List<MovieImage>();
        List<MovieImage> imageListActors = new List<MovieImage>();
        public uint? curVolumeId;
        public string curVolumeName;
        public string CatalogFolder = "";

        //private MovieCatalog CurCatItem = new MovieCatalog();
        private CatalogItem CurCatItem = new CatalogItem();

        public string TMDB_ApiKey = "b54df3a8957fa96c9d23412c528bb667";
        public string TMDB_Token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiNTRkZjNhODk1N2ZhOTZjOWQyMzQxMmM1MjhiYjY2NyIsInN1YiI6IjYxOGU5NGU5NjZhN2MzMDA2NGJmZDFmMiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.7OeQCMpl6s7zpGvsRD4LaKS-ETGDe7T8_hH5Tqi5mAA";

        #endregion

        #region Constructors

        public mainForm()
        {
            InitializeComponent();
            ToRemoveAll.Clear();
            ToRemoveFolder.Clear();
            ToRemoveFile.Clear();
            ReadWords();

        }

        private void mainForm_Load(object sender, EventArgs e)
        {           
            maxLengthForPattern = Convert.ToInt32(ConfigurationManager.AppSettings["maxLengthForPattern"]);
            SilentMode = Convert.ToBoolean(ConfigurationManager.AppSettings["silentMode"]);

            if (SilentMode)
            {
                tvFolders.Visible = false;
            }
            ResizeForm();

            allowedVideos = ConfigurationManager.AppSettings["video"].Split(',').ToArray();
            allowedSub = ConfigurationManager.AppSettings["subtitle"].Split(',').ToArray();
            
            imageListIcon.Images.Add("_FOLDER_", IconHelper.GetDirectoryIcon());
            lblAll.Parent = tabControl1.TabPages["tabPage2"];
            lblFolder.Parent = tabControl1.TabPages["tabPage2"];
            lblFile.Parent = tabControl1.TabPages["tabPage2"];
            lblAll.Top = 25;
            lblFolder.Top = 25;
            lblFile.Top = 25;
            lblAll.BringToFront();
            lblFolder.BringToFront();
            lblFile.BringToFront();
            ResizeForm();
            ReadLastSelections();

            //string path = Path.Combine(Application.StartupPath, "CatalogFolder.Dat"); // 
            string path = Path.Combine(CatalogFolder, "CatalogFolder.Dat"); // 
            if (!File.Exists(path))
            {
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                folderDlg.ShowNewFolderButton = true;
                folderDlg.SelectedPath = txtFolderName.Text;
                folderDlg.Description = "Select a Catalog Folder";
                DialogResult result = folderDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CatalogFolder = folderDlg.SelectedPath;
                    using (StreamWriter outputFile = new StreamWriter(path))
                    {
                        outputFile.Write(CatalogFolder);
                    }
                }
                if (CatalogFolder == "")
                {
                   MessageBox.Show("Need a Catalog Folder to Proceed!", "Catalog Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Windows.Forms.Application.Exit();
                }
            }

            try
            {
                CatalogFolder = System.IO.File.ReadAllLines(path).OrderBy(p => p).Distinct().ToList().FirstOrDefault()+"\\";
            }
            catch
            {
                System.Windows.Forms.Application.Exit();
            }

            if (CatalogFolder == "")
            {
                MessageBox.Show("Need a Catalog Folder to Proceed!", "Catalog Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }

            CreateDB();
        }

        #endregion

        #region Resize Form & Controls

        private void Form_Resize(object sender, EventArgs e)
        {
            ResizeForm();
        }

        private void ResizeForm()
        {
            wordsToRemove.Visible = false;
            wordsToRemoveFile.Visible = false;
            wordsToRemoveFolder.Visible = false;

            if (this.Width < 600)
            {
                this.Width = 600;
            }

            if (this.Height < 500)
            {
                this.Height = 500;
            }
            Application.DoEvents();
            panelBase.Width = this.Width;
            panelBase.Height = this.Height - menuStrip1.Height-30;
            btnSelectFolder.Left = this.Width  - 20- btnSelectFolder.Width - 10;
            Application.DoEvents();

            listFolders.Width = this.Width -20  - listFolders.Left - 10;
            btnClearList.Left = listFolders.Left + listFolders.Width - btnClearList.Width;

            txtFolderName.Width = this.Width -20 - txtFolderName.Left - btnAdd.Width - btnSelectFolder.Width +5;

            btnAdd.Left = txtFolderName.Left + txtFolderName.Width - btnAdd.Width ;

            if (SilentMode)
            {
                listFolders.Height = tabPage1.Height - 75;
                btnClearList.Height = listFolders.Height;
            }

            tvFolders.Width = tabPage1.Width - 25;
            tvFolders.Height = tabPage1.Height - 165;

            pptGrdDetailData.SetLabelColumnWidth(100);

            wordsToRemove.Visible = true;
            wordsToRemoveFile.Visible = true;
            wordsToRemoveFolder.Visible = true;
        }

        private void splitContainer1_ClientSizeChanged(object sender, EventArgs e)
        {
            PosLabels();
        }

        private void splitContainer2_ClientSizeChanged(object sender, EventArgs e)
        {
            PosLabels();
        }

        private void splitContainer3_ClientSizeChanged(object sender, EventArgs e)
        {
            PosLabels();
        }

        private void splitContainer4_ClientSizeChanged(object sender, EventArgs e)
        {
            PosLabels();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            PosLabels();
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            PosLabels();
        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {
            PosLabels();
        }

        private void splitContainer4_SplitterMoved(object sender, SplitterEventArgs e)
        {
            PosLabels();
        }

        private void PosLabels()
        {
            Point relativeLocA = GetPositionInForm(wordsToRemove);
            lblAll.Left = relativeLocA.X + wordsToRemove.Width - lblAll.Width - 30;
            Point relativeLocB = GetPositionInForm(wordsToRemoveFolder);
            lblFolder.Left = relativeLocB.X + wordsToRemoveFolder.Width - lblFolder.Width - 30;
            Point relativeLocC = GetPositionInForm(wordsToRemoveFile);
            lblFile.Left = relativeLocC.X + wordsToRemoveFile.Width - lblFile.Width - 30;
        }

        #endregion

        #region UI - Buttons & Other Human Interactions

        #region Buttons

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            SelectFolder();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Process(chkAgressive.Checked);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddFolder(txtFolderName.Text);
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            listFolders.Items.Clear();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            SortLists();
        }

        private void btnCreateFolders_Click(object sender, EventArgs e)
        {
            CreateFolders();
        }

        private void btnPairSub_Click(object sender, EventArgs e)
        {
            ProcessSubNames();
        }        
        
        private void btnCatalog_Click(object sender, EventArgs e)
        {
            MountCatalog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveFile();
            SaveFileFolder();
            SaveFileFile();
            ToRemoveAll.Clear();
            ToRemoveFolder.Clear();
            ToRemoveFile.Clear();

            //wordsToRemove.Visible = false;
            //wordsToRemoveFile.Visible = false;
            //wordsToRemoveFolder.Visible = false;

            ReadWords();

            //wordsToRemove.Visible = true;
            //wordsToRemoveFile.Visible = true;
            //wordsToRemoveFolder.Visible = true;

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About();
        }    

        private void btnAddToAll_Click(object sender, EventArgs e)
        {
            AddToAll();
        }

        private void btnAddToFolder_Click(object sender, EventArgs e)
        {
            AddToFolder();
        }

        private void btnAddToFile_Click(object sender, EventArgs e)
        {
            AddToFile();
        }        

        private void btnSearchPatterns_Click_1(object sender, EventArgs e)
        {
            SearchPatterns(chbRecursiveFoldersSearch.Checked);
        }

        private void btnSaveCatalog_Click(object sender, EventArgs e)
        {
            SaveCatalog(ReadingData, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateNodeDataTitle(chkTmdbDo.Checked);
        }

        private void btnLoadCatalogs_Click(object sender, EventArgs e)
        {
            tvCatalogs.Nodes.Clear();
            LoadCatalogs(null, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // get current video and get movie information about
            // ave on db to
            CatalogItem data = (CatalogItem)tvCatalogs.SelectedNode.Tag;
            if (data != null)
            {
                data.Title = GetTitle(data.Name, data);
            }
        }

        #endregion

        #region Menu

        private void cleanNamesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process(chkAgressive.Checked);
        }

        private void createFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFolders();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pairSubtitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessSubNames();
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            About();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About();
        }

        private void searchPatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchPatterns(chbRecursiveFoldersSearch.Checked);
        }

        #endregion

        #region Other interface

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            ResizeForm();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            ResizeForm();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResizeForm();
        }

        private void About()
        {
            AboutBox form = new AboutBox();
            form.ShowDialog();
        }

        private void listFolders_DoubleClick(object sender, EventArgs e)
        {
            listFolders.Items.Clear();
        }

        private void tvFoldersCat_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetCurMovie();
        }

        private void dataGridViewImage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetMainPoster(e.RowIndex);
        }

        private void dataGridViewImage_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            SetImages(e.RowIndex);
        }

        private void dataGridViewImage_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SetImages(e.RowIndex);
        }

        private void pptGrdDetailData_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            SetTreeMovieObjectDetail(e.NewSelection.PropertyDescriptor.Name.ToUpper(), e.NewSelection.Value);
        }

        private void dtGridCatalogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SetImagesCatalog(e.RowIndex);
        }

        private void dtGridCatalogs_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SetImagesCatalog(e.RowIndex);
        }

        private void dtGridCatalogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SetImagesCatalog(e.RowIndex);
        }

        private void tvCatalogs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CatalogItem itm = (CatalogItem)e.Node.Tag;
            if (itm != null)
            {
                LoadCatalogs(e.Node, itm.Id);
                e.Node.Expand();
            }
        }

        private void tvCatalogs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetCurMovieCatalog();
        }

        #endregion

        #endregion

        #region Parms To Rename File & Folders

        public void ReadWords()
        {
            wordsToRemove.Text = "";
            wordsToRemoveFolder.Text = "";
            wordsToRemoveFile.Text = "";
            //Application.DoEvents();

            string filePath = Path.Combine(CatalogFolder, "ToRemove.txt");
            ToRemoveAll = System.IO.File.ReadAllLines(filePath).OrderBy(p=>p).Distinct().ToList();
            // load textbox
            var tempList = ToRemoveAll.OrderByDescending(p => p.Length).ToList();
            ToRemoveAll = tempList;

            foreach (string s in ToRemoveAll)
            {
                wordsToRemove.Text = wordsToRemove.Text + s + Environment.NewLine;
            }

            filePath = Path.Combine(CatalogFolder, "ToRemoveFolder.txt");
            ToRemoveFolder = System.IO.File.ReadAllLines(filePath).OrderBy(p => p).Distinct().ToList();
            tempList = ToRemoveFolder.OrderByDescending(p => p.Length).ToList();
            ToRemoveFolder = tempList;
            // load textbox
            foreach (string s in ToRemoveFolder)
            {
                wordsToRemoveFolder.Text = wordsToRemoveFolder.Text + s + Environment.NewLine;
            }

            filePath = Path.Combine(CatalogFolder, "ToRemoveFile.txt");
            ToRemoveFile = System.IO.File.ReadAllLines(filePath).OrderBy(p => p).Distinct().ToList();
            tempList = ToRemoveFile.OrderByDescending(p => p.Length).ToList();
            ToRemoveFile = tempList;
            // load textbox
            foreach (string s in ToRemoveFile)
            {
                wordsToRemoveFile.Text = wordsToRemoveFile.Text + s + Environment.NewLine;
            }

            //Application.DoEvents();
        }

        private void SaveFile()
        {


            if ((wordsToRemove.Text != null) && (wordsToRemove.Text != ""))
            {
                string filePath = Path.Combine(CatalogFolder, "ToRemove.txt");

                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    for (int i = 0; i < wordsToRemove.Lines.Length; i++)
                    {

                        String text = wordsToRemove.Lines[i].Trim();
                        if (text != "")
                        {
                            if (i == wordsToRemove.Lines.Length-2)
                            {
                                outputFile.Write(text);
                            }
                            else
                            {
                                outputFile.WriteLine(text);
                            }
                        }

                    }
                }

            }
        }

        private void SaveFileFolder()
        {
            if ((wordsToRemoveFolder.Text != null) && (wordsToRemoveFolder.Text != ""))
            {
                string filePath = Path.Combine(CatalogFolder, "ToRemoveFolder.txt");

                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    for (int i = 0; i < wordsToRemoveFolder.Lines.Length; i++)
                    {

                        String text = wordsToRemoveFolder.Lines[i].Trim();
                        if (text != "")
                        {
                            if (i == wordsToRemoveFolder.Lines.Length - 2)
                            {
                                outputFile.Write(text);
                            }
                            else
                            {
                                outputFile.WriteLine(text);
                            }
                        }

                    }
                }

            }
        }

        private void SaveFileFile()
        {
            if ((wordsToRemoveFile.Text != null) && (wordsToRemoveFile.Text != ""))
            {
                string filePath = Path.Combine(CatalogFolder, "ToRemoveFile.txt");

                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    for (int i = 0; i < wordsToRemoveFile.Lines.Length; i++)
                    {

                        String text = wordsToRemoveFile.Lines[i].Trim();
                        if (text != "")
                        {
                            if (i == wordsToRemoveFile.Lines.Length - 2)
                            {
                                outputFile.Write(text);
                            }
                            else
                            {
                                outputFile.WriteLine(text);
                            }
                        }

                    }
                }

            }
        }

        private void SortLists()
        {
            //return; // rotiona esa com bug
            var tempList = ToRemoveAll.OrderBy(p=>p).ToList();
            ToRemoveAll = tempList;

            tempList = ToRemoveFolder.OrderBy(p => p).ToList();
            ToRemoveFolder = tempList;

            tempList = ToRemoveFile.OrderBy(p => p).ToList();
            ToRemoveFile = tempList;

            var listString = new StringBuilder();
            foreach (string s in ToRemoveAll)
            {
                listString.Append(s);
                listString.Append(Environment.NewLine);
            }
            wordsToRemove.Text = listString.ToString();

            listString = new StringBuilder();
            foreach (string s in ToRemoveFolder)
            {
                listString.Append(s);
                listString.Append(Environment.NewLine);
            }
            wordsToRemoveFolder.Text = listString.ToString();

            listString = new StringBuilder();
            foreach (string s in ToRemoveFile)
            {
                listString.Append(s);
                listString.Append(Environment.NewLine);
            }
            wordsToRemoveFile.Text = listString.ToString();
        }

        #endregion

        #region CORE: Process Folder & Files 

        #region Functions

        public String ClearStringData(String ToClean, Boolean isFile, bool AgressiveMode = false)
        {

            String CleanedString = ToClean;
            String tempData = "";

            List<string> Years = ToClean.GetYearsFromString(); // GetYearsFromString(CleanedString);

            while (tempData != CleanedString)
            {
                tempData = CleanedString;
                if (isFile)
                {
                    CleanedString = CleanedString.RemoveFromList(ToRemoveFile);
                }

                if (!isFile || AgressiveMode)
                {
                    CleanedString = CleanedString.RemoveFromList(ToRemoveFolder, true);                  
                }

                if (isFile && AgressiveMode)
                {
                    CleanedString = Regex.Replace(CleanedString, @"\(\d{1,2}\/\d{1,2}\/\d{1,2}\)", "");
                    //List<string> Years = CleanedString.GetYearsFromString(); // GetYearsFromString(CleanedString);
                    foreach (string year in Years)
                    {
                        CleanedString = CleanedString.Replace(year, " ");
                    }
                }

                foreach (var DictItem in ToRemoveAll)
                {
                    CleanedString = CleanedString.RemoveFromList(ToRemoveAll, true);
                }

                CleanedString = " " + CleanedString.TrimStart().TrimEnd() + " ";
                CleanedString = CleanedString.RemoveSpecialChars();

            }

            return CleanedString;

        }
        
        #endregion

        #region Select Folders

        private void SelectFolder()
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            folderDlg.SelectedPath = txtFolderName.Text;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFolderName.Text = folderDlg.SelectedPath;
                SaveSelectedFolder(txtFolderName.Text);
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void SaveSelectedFolder(string filename)
        {
            string filePath = Path.Combine(Application.StartupPath, "LastSelection.dat");
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                outputFile.Write(filename);
            }
        }

        private void SaveAllSelectedFolders()
        {
            string filePath = Path.Combine(Application.StartupPath, "LastSelection.dat");
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                foreach (var itm in listFolders.Items)
                {
                    outputFile.WriteLine(itm);
                }
            }

        }

        public void ReadLastSelections()
        {
            List<String> SelectedFolders = new List<string>();
            string filePath = Path.Combine(Application.StartupPath, "LastSelection.dat");
            try
            {
                SelectedFolders = System.IO.File.ReadAllLines(filePath).OrderBy(p => p).Distinct().ToList();
                // load textbox
                var tempList = SelectedFolders.OrderByDescending(p => p.Length).ToList();
                SelectedFolders = tempList;

                foreach (string s in SelectedFolders)
                {
                    ReadFolder(s);
                }
            }
            catch { }
            
        }

        private void ReadFolder(String FolderName)
        {
            if (FolderName != null && FolderName != "")
            {
                listFolders.Items.Add(FolderName);
            }
        }

        private void AddFolder(String FolderName)
        {
            if (FolderName != null && FolderName != "")
            {
                listFolders.Items.Add(FolderName);
            }
            SaveAllSelectedFolders();
        }

        #endregion

        #region Process File & Folder Names

        private string  LastString(String Item)
        {
            string result = Item;
            string[] results = result.Split('\\');
            if (results.Length>0)
            {
                result = results[results.Length-1];
            }

            return result;
        }

        private void ProcessFolder(String path, bool AgressiveMode = false)
        {
            TreeNode node = new TreeNode();
            if (File.Exists(path))
            {
                ProcessFile(path, AgressiveMode);
            }
            else if (Directory.Exists(path))
            {
                if (!SilentMode)
                {
                    node = tvFolders.Nodes.Add(LastString(path));

                    node.ImageKey = "_FOLDER_";
                    node.SelectedImageKey = "_FOLDER_";
                    node.StateImageKey = "_FOLDER_";

                    tvFolders.SelectedNode = node;
                }
                ProcessDirectory(path, node, AgressiveMode);
                Application.DoEvents();
            }
            else
            {
                if (!SilentMode)
                {
                    node = tvFolders.Nodes.Add($"{path} is not a valid file or directory.");
                }
            }
        }

        private void Process(bool AgressiveMode = false)
        {
            if (!SilentMode)
            {
                tvFolders.Nodes.Clear();
            }
            //VideoFiles.Clear();
            //SubtitleFiles.Clear();
            if (listFolders.Items.Count == 0)
            {
                if (txtFolderName.Text != "" && txtFolderName.Text != null)
                {
                    string path = txtFolderName.Text;
                    ProcessFolder(path);
                }
            }
            else
            {
                foreach(var itm in listFolders.Items)
                {
                    ProcessFolder(itm.ToString(), AgressiveMode);
                }
            }
            SaveVideoLog();
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public void ProcessDirectory(string targetDirectory, TreeNode node, bool AgressiveMode = false)
        {

            if (chbFolderRename.Checked)
            {

                String nFileName = Path.GetFileName(targetDirectory);
                String nPath = Path.GetDirectoryName(targetDirectory);

                String cleanPath = ClearStringData(nFileName, false, AgressiveMode);
                if (AgressiveMode)
                {
                    cleanPath = ClearStringData(nFileName, false, AgressiveMode);
                }

                String NewPath = Path.Combine(nPath, cleanPath);


                if (cleanPath != nFileName)
                {
                   
                    try
                    {
                        Directory.Move(targetDirectory, NewPath);
                        targetDirectory = NewPath;
                    }
                    catch { }
                    finally
                    {
                        if (!SilentMode)
                        {
                            var tnode = tvFolders.SelectedNode.Nodes.Add(LastString(NewPath));

                            tnode.ImageKey = "__FOLDER_SELECTED__";
                            tnode.SelectedImageKey = "__FOLDER_SELECTED__";
                            tnode.StateImageKey = "__FOLDER_SELECTED__";

                        }
                    }
                    
                }
            }

            if (chbRecursiveFolders.Checked)
            {
                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    
                    TreeNode snode = null;
                    if (!SilentMode)
                    {
                        tvFolders.SelectedNode = node;
                        snode = tvFolders.SelectedNode.Nodes.Add(LastString(subdirectory));

                        snode.ImageKey = "_FOLDER_";
                        snode.SelectedImageKey = "_FOLDER_";
                        snode.StateImageKey = "_FOLDER_";

                        tvFolders.SelectedNode = snode;
                    }

                    ProcessDirectory(subdirectory, snode, AgressiveMode);
                    Application.DoEvents();
                }
            }
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                if (!SilentMode)
                {
                    tvFolders.SelectedNode = node;
                }
                ProcessFile(fileName, AgressiveMode);
                Application.DoEvents();
            }
        }

        // Insert logic for processing found files here.
        public void ProcessFile(string originalFileName, bool AgressiveMode = false)
        {
            TreeNode fNode = null;


            if (chbFileRename.Checked)
            {

                String nExtension = Path.GetExtension(originalFileName);
                String nFileName = Path.GetFileNameWithoutExtension(originalFileName);
                String nPath = Path.GetDirectoryName(originalFileName);

                String cleanFileName = ClearStringData(nFileName, true, AgressiveMode);

                if (AgressiveMode)
                {
                    cleanFileName = ClearStringData(nFileName, true, AgressiveMode);
                }

                String newFileName = Path.Combine(nPath, cleanFileName + nExtension);

                if (cleanFileName != nFileName) {

                    try
                    {
                        File.Move(originalFileName, newFileName);
                        if (!SilentMode)
                        {
                            fNode = tvFolders.SelectedNode.Nodes.Add(LastString(originalFileName) +  " -> " + LastString(newFileName));

                            fNode.ImageKey = "__MEDIA__";
                            fNode.SelectedImageKey = "__MEDIA__";
                            fNode.StateImageKey = "__MEDIA__";

                        }
                    }
                    catch { }
                    finally
                    {

                    }
                    
                }
            }

        }

        #endregion

        #region Create Folder & move Midia to Folder

        public void CreateFolder(string path)
        {
            TreeNode fNode = null;
            if (!SilentMode)
            {
                fNode = tvFolders.Nodes.Add(LastString(path));

                fNode.ImageKey = "_FOLDER_";
                fNode.SelectedImageKey = "_FOLDER_";
                fNode.StateImageKey = "_FOLDER_";

            }

            //string[] fileEntries = Directory.GetFiles(path);
            //var allowedVideos = ConfigurationManager.AppSettings["video"].Split(',').ToArray();

            List<String> MidiafileEntries = Directory
                .GetFiles(path)
                .Where(file => allowedVideos.Any(file.ToLower().EndsWith))
                .ToList();

            foreach (string fileName in MidiafileEntries)
            {
                if (!SilentMode)
                {
                    tvFolders.SelectedNode = fNode;
                }

                String nExtension = Path.GetExtension(fileName);
                String nFileName = Path.GetFileNameWithoutExtension(fileName);
                String nPath = Path.GetDirectoryName(fileName);

                String newFileName = Path.Combine(nPath, nFileName);

                if (!SilentMode)
                {
                    var tnode = tvFolders.SelectedNode.Nodes.Add(LastString(newFileName));
                    tnode.ImageKey = "__MEDIA__";
                    tnode.SelectedImageKey = "__MEDIA_SELECTED__";
                }

                try
                {
                    String DestFolder = Path.Combine(nPath, nFileName);
                    newFileName = Path.Combine(nPath, DestFolder, nFileName+nExtension);
                    if (!System.IO.Directory.Exists(DestFolder))
                    {
                        System.IO.Directory.CreateDirectory(DestFolder);
                    }
                    File.Move(fileName, newFileName);
                }
                catch { }
                //Application.DoEvents();
            }

        }

        public void DelEmptyFolder(string basePath, string path)
        {
            TreeNode fNode = null;
            if (!SilentMode)
            {
                fNode = tvFolders.Nodes.Add(LastString(path));

                fNode.ImageKey = "_FOLDER_";
                fNode.SelectedImageKey = "_FOLDER_";
                fNode.StateImageKey = "_FOLDER_";

            }

            List<String> FileEntries = Directory
                .GetFiles(path)              
                .ToList();
            List<String> FolderEntries = Directory
                .GetDirectories(path)
                .ToList();


            if (FileEntries.Count == 0 && FolderEntries.Count == 0 && basePath!="")
            {
                Directory.Delete(path);
            }
            else
            {
                if (basePath != "")
                {
                    foreach (string fileName in FileEntries)
                    {

                        string DestFile = Path.Combine(basePath, Path.GetFileName(fileName));

                        if (!(File.Exists(DestFile)))
                        {
                            File.Move(fileName, DestFile);
                        }
                    }
                }
                foreach (string folderName in FolderEntries)
                {
                    string OldBasePath = basePath;
                    if (basePath=="")
                    {
                        basePath = folderName;
                    }
                    DelEmptyFolder(basePath, folderName);
                    basePath = OldBasePath;
                }
            }

            try
            {
                FileEntries = Directory
                    .GetFiles(path)
                    .ToList();
                FolderEntries = Directory
                    .GetDirectories(path)
                    .ToList();
                if (FileEntries.Count == 0 && FolderEntries.Count == 0 && basePath != "")
                {
                    Directory.Delete(path);
                }
            }
            catch { }

        }

        public void CreateFolders()
        {
            // create folders and move files to it
            MessageBox.Show("Caution!!!, This operation should only be used on content without subfolders, or when the subfolders are content. Using in subfolders whose purpose is just to organize the content will move the content in an unwanted way.", "Folder Creation and Move Process", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            DialogResult result = MessageBox.Show("Caution!!!, Proceed?", "Folder Process", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result.Equals(DialogResult.OK))
            {
                //Do something

                tvFolders.Nodes.Clear();
                if (listFolders.Items.Count == 0)
                {
                    if (txtFolderName.Text != "" && txtFolderName.Text != null)
                    {
                        string path = txtFolderName.Text;
                        CreateFolder(path);
                        DelEmptyFolder("", path);
                    }
                }
                else
                {
                    foreach (var itm in listFolders.Items)
                    {
                        CreateFolder(itm.ToString());
                        DelEmptyFolder("", itm.ToString());
                    }
                }
                SaveFolderLog();
            }
        }

        #endregion

        #region Search Patterns

        #region Search for Patterns To Process

        public void GetTokens(String Data) 
        {

            String strTemp = TextPrepare(Data);

            String[] Tokens = strTemp.Split(' ');
            if (Tokens.Length>0)
            {
                for (int i= 0; i< Tokens.Length;i++)
                {
                    if (!Patterns.Exists(p => p == Tokens[i].Trim())) {
                        if (Tokens[i].Trim().Length < maxLengthForPattern 
                            && !Tokens[i].Trim().Contains("\\")
                            && !ToRemoveAll.Exists(p => p == Tokens[i].Trim())
                            && !ToRemoveFile.Exists(p => p == Tokens[i].Trim())
                            && !ToRemoveFolder.Exists(p => p == Tokens[i].Trim())
                            )
                        {
                            Patterns.Add(Tokens[i].Trim());
                        }
                    }
                }
            }

            String[] aTokens = strTemp.Split('.');
            if (aTokens.Length > 0)
            {
                for (int i = 0; i < aTokens.Length; i++)
                {
                    String Text = aTokens[i].Trim();
                    if (!Patterns.Exists(p => p == Text))
                    {
                        if (aTokens[i].Trim().Length < maxLengthForPattern 
                            && !Text.Trim().Contains("\\")
                            && !ToRemoveAll.Exists(p => p == Text)
                            && !ToRemoveFile.Exists(p => p == Text)
                            && !ToRemoveFolder.Exists(p => p == Text))
                        {
                            Patterns.Add(Text);
                        }
                    }
                    // multiple
                   
                    if (i < aTokens.Length-1)
                    { 
                        Text = aTokens[i].Trim() + "." + aTokens[i + 1].Trim();
                        if (!Patterns.Exists(p => p == Text))
                        {
                            if (Text.Length < maxLengthForPattern
                            && !Text.Contains("\\")
                            && !ToRemoveAll.Exists(p => p == Text)
                            && !ToRemoveFile.Exists(p => p == Text)
                            && !ToRemoveFolder.Exists(p => p == Text))
                            {
                                Patterns.Add(Text);
                            }
                        }
                    }

                    // multiple

                    if (i < aTokens.Length - 2)
                    {
                        Text = aTokens[i].Trim() + "." + aTokens[i + 1].Trim() + "." + aTokens[i + 2].Trim();
                        if (!Patterns.Exists(p => p == Text))
                        {
                            if (Text.Length < maxLengthForPattern
                            && !Text.Contains("\\")
                            && !ToRemoveAll.Exists(p => p == Text)
                            && !ToRemoveFile.Exists(p => p == Text)
                            && !ToRemoveFolder.Exists(p => p == Text))
                            {
                                Patterns.Add(Text);
                            }
                        }
                    }

                }
            }

        }

        public String TextPrepare(String Data)
        {
            String result = Data;

            result = result.Replace("[", " [")  
                           .Replace("[ ", "[ ")
                           .Replace(" ]", "]")
                           .Replace("]", "] ")

                           .Replace("{", " {")
                           .Replace("{ ", "{ ")
                           .Replace(" }", "}")
                           .Replace("}", "} ")

                           .Replace("(", " (")
                           .Replace("( ", "( ")
                           .Replace(" )", ")")
                           .Replace(")", ") ")

                           .Replace("<", " <")
                           .Replace("< ", "< ")
                           .Replace(" >", ">")
                           .Replace(">", "> ")

                           .Replace(",", ", ")
                           .Replace(";", "; ")
                           .Replace(":", ": ")

                           .Replace(" :", ":")
                           .Replace(" ;", ";")
                           .Replace(" ,", ",")
                           .Replace(" .", ".")

                           .Replace("  ", " ")
                           .TrimEnd()
                           .TrimStart();

            return result;
        }

        public void SearchPattern(string path, bool Recursive)
        {

            dataGridView.Rows.Clear();

            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                GetTokens(Path.GetFileNameWithoutExtension(fileName));
            }

            string[] folderEntries = Directory.GetDirectories(path);
            foreach (string folder in folderEntries)
            {
                GetTokens(folder);
                if (Recursive)
                {
                    SearchPattern(folder, Recursive);
                }
            }
        }

        public void SearchPatterns(bool Recursive)
        {
            List<String> result = new List<string>();
            // create folders and move files to it
            dataGridView.Rows.Clear();
            Patterns.Clear();
            dataGridView.Refresh();
            dataGridView.DataSource = null;
            if (listFolders.Items.Count == 0)
            {
                if (txtFolderName.Text != "" && txtFolderName.Text != null)
                {
                    string path = txtFolderName.Text;
                    SearchPattern(path, Recursive);
                }
            }
            else
            {
                foreach (var itm in listFolders.Items)
                {
                   SearchPattern(itm.ToString(), Recursive);
                }
            }
            //SaveFolderLog();
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
            dataGridView.DataSource = null;
    
            if (Patterns.Count > 0)
            {
                foreach (String Token in Patterns.OrderBy(p=>p))
                {
                    if (Token.Length > 0)
                    {
                        int rowIndex = this.dataGridView.Rows.Add();

                        var row = this.dataGridView.Rows[rowIndex];

                        row.Cells["Selected"].Value = false;
                        row.Cells["Pattern"].Value = Token;
                    }
                }
            }

        }

        #endregion

        public void AddToAll()
        {
            if (dataGridView.Rows.Count > 0)
            {
                foreach(DataGridViewRow rw in dataGridView.Rows)
                {
                    if (Convert.ToBoolean(rw.Cells["Selected"].Value))
                    {
                        wordsToRemove.Text = wordsToRemove.Text + Environment.NewLine + Convert.ToString(rw.Cells["Pattern"].Value) + Environment.NewLine;
                        //Application.DoEvents();
                    }
                }
            }
            SaveFile();
            ReadWords();
        }

        public void AddToFolder()
        {
            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow rw in dataGridView.Rows)
                {
                    if (Convert.ToBoolean(rw.Cells["Selected"].Value))
                    {
                        wordsToRemoveFolder.Text = wordsToRemoveFolder.Text + Environment.NewLine + Convert.ToString(rw.Cells["Pattern"].Value) + Environment.NewLine;
                        //Application.DoEvents();
                    }
                }
            }
            SaveFileFolder();
            ReadWords();
        }

        public void AddToFile()
        {
            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow rw in dataGridView.Rows)
                {
                    if (Convert.ToBoolean(rw.Cells["Selected"].Value))
                    {
                        wordsToRemoveFile.Text = wordsToRemoveFile.Text + Environment.NewLine + Convert.ToString(rw.Cells["Pattern"].Value) + Environment.NewLine;
                        //Application.DoEvents();
                    }
                }
            }
            SaveFileFile();
            ReadWords();
        }

        #endregion

        // implementing
        #region Subtitle

        public void ProcessSubName(string path)
        {
            TreeNode fNode = null;
            if (!SilentMode)
            {
                fNode = tvFolders.Nodes.Add(LastString(path));

                fNode.ImageKey = "_FOLDER_";
                fNode.SelectedImageKey = "_FOLDER_";
                fNode.StateImageKey = "_FOLDER_";

            }

            //var allowedVideos = ConfigurationManager.AppSettings["video"].Split(',').ToArray();
            //var allowedSub = ConfigurationManager.AppSettings["subtitle"].Split(',').ToArray();

            List<String> MidiafileEntries = Directory
                .GetFiles(path)
                .Where(file => allowedVideos.Any(file.ToLower().EndsWith))
                .ToList();

            List<String> SubtitlefileEntries = Directory
                .GetFiles(path)
                .Where(file => allowedSub.Any(file.ToLower().EndsWith))
                .ToList();

            foreach (string fileName in MidiafileEntries)
            {
                if (!SilentMode)
                {
                    tvFolders.SelectedNode = fNode;
                }
                else
                {
                    fNode = null;
                }

                String nExtension = Path.GetExtension(fileName);
                String nFileName = Path.GetFileNameWithoutExtension(fileName);
                String nPath = Path.GetDirectoryName(fileName);

                String newFileName = Path.Combine(nPath, nFileName);
                if (!SilentMode)
                {
                    var tnode = tvFolders.SelectedNode.Nodes.Add(LastString(newFileName));
                    tnode.ImageKey = "__MEDIA__";
                    tnode.SelectedImageKey = "__MEDIA_SELECTED__";
                    tnode.StateImageKey = "__MEDIA__";
                }

                //try
                //{
                //    String DestFolder = Path.Combine(nPath, nFileName);
                //    newFileName = Path.Combine(nPath, DestFolder, nFileName + nExtension);
                //    if (!System.IO.Directory.Exists(DestFolder))
                //    {
                //        System.IO.Directory.CreateDirectory(DestFolder);
                //    }
                //    File.Move(fileName, newFileName);
                //}
                //catch { }
                //Application.DoEvents();
            }

        }

        private void ProcessSubNames()
        {
           // VideoFiles.Clear();
            //SubtitleFiles.Clear();

            // create folders and move files to it
            tvFolders.Nodes.Clear();
            if (listFolders.Items.Count == 0)
            {
                if (txtFolderName.Text != "" && txtFolderName.Text != null)
                {
                    string path = txtFolderName.Text;
                    ProcessSubName(path);
                }
            }
            else
            {
                foreach (var itm in listFolders.Items)
                {
                    ProcessSubName(itm.ToString());
                }
            }
            SaveSubtilteLog();
        }

        #endregion

        #endregion

        // To Implement
        #region LOG

        public void SaveSubtilteLog()
        {

        }

        public void SaveVideoLog()
        {

        }

        public void SaveFolderLog()
        {

        }

        #endregion

        #region Special Functions

        public  static Point GetPositionInForm( Control ctrl)
        {
            Point p = ctrl.Location;
            Control parent = ctrl.Parent;
            Form frm = ctrl.FindForm();
            Rectangle screenRectangle = frm.RectangleToScreen(frm.ClientRectangle);
            int titleHeight = screenRectangle.Top - frm.Top;
            int leftMargin = screenRectangle.Left - frm.Left;
            p.Offset(leftMargin, titleHeight);

            while (!(parent is Form))
            {
                p.Offset(parent.Location.X, parent.Location.Y);
                parent = parent.Parent;
            }
            return p;
        }

        public void SetSystemIcon(String Name)
        {
            if (!imageListIcon.Images.ContainsKey(Name))
            {
                imageListIcon.Images.Add(Name, IconHelper.GetFileIcon(Name));
            }
        }

        private void updtIcons(String FullName)
        {
            String Extension = Path.GetExtension(FullName);
            if (Extension.ToUpper() != ".EXE")
            {
                if (!imageListIcon.Images.ContainsKey(Extension))
                {
                    try
                    {
                        var iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(FullName);
                        imageListIcon.Images.Add(Extension, iconForFile);
                        imageListIcon.TransparentColor = Color.Black;
                    }
                    catch { }
                }
            }
            else
            {
                String Filename = Path.GetFileName(FullName);
                if (!imageListIcon.Images.ContainsKey(Filename))
                {
                    try
                    {
                        var iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(FullName);
                        imageListIcon.Images.Add(Filename, iconForFile);
                        imageListIcon.TransparentColor = Color.Black;
                    }
                    catch { }
                }
            }
        }

        public string RemoveExtension(string Name)
        {
            string result = Name;
            string[] parts = result.Split('.');
            if (parts.Length>1)
            {
                result = parts[0];
                for(int i = 1; i < parts.Length-1; i++)
                {
                    result = result + "." + parts[i];
                }
            } 

            return result;
        }

        private Crc32 CRC(string file, long bytes = 0)
        {
            var crc32 = new Crc32();
            var hash = String.Empty;
            long bytesCount = 0;

            int attempts = 0;
            int MaxAttempts = maxLengthForPattern = Convert.ToInt32(ConfigurationManager.AppSettings["MaxAttempts"]);
            bool done = false;

            while (attempts < MaxAttempts && !done)
            {
                try
                {
                    using (var fs = File.Open(file, FileMode.Open, FileAccess.Read))
                    {
                        try
                        {
                            var c = crc32.ComputeHash(fs);
                            foreach (byte b in c)
                            {
                                hash += b.ToString("x2").ToLower();
                                bytesCount = bytesCount + 1;
                                // to count a minimum bytes offset needed
                                if (bytesCount > bytes && bytes != 0)
                                {
                                    break;
                                }
                            }
                            done = true;
                        }
                        catch
                        {
                            crc32 = null;
                            attempts = attempts + 1;
                            //MessageBox.Show("2", "ERR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                catch
                {
                    crc32 = null;
                    attempts = attempts + 1;
                    //MessageBox.Show("1", "ERR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            return crc32;
        }
    
        #endregion

        #region Catalog Data

        #region DB Catalog

        private void UpdateNestedNode(TreeNode node, bool chkTmdb)
        {
            CatalogItem data = (CatalogItem)node.Tag;
            if (data != null)
            {
                lblRec.Text = data.Id.ToString();
                Application.DoEvents();

                data.Title = GetTitle(data.FullFilename, data);
                data.SoundexTitle = Soundex.Generate(data.Title, 10);

                if (chkTmdb)
                {
                    GetMovieTmdbData(data, data.FullFilename, chkTmdb);
                }

                var recs = UpdateItem(data);

                lblRec.Text = data.Id.ToString() + " ok";
                Application.DoEvents();

                node.Tag = data;
                node.Text = data.Title;
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode cNode in node.Nodes)
                    {
                        UpdateNestedNode(cNode, chkTmdb);
                    }
                }
                else
                {
                    UpdateDbNestedNodeTitle(data, chkTmdb);
                }
            }
        }

        private void UpdateDbNestedNodeTitle(CatalogItem data, bool chkTmdb)
        {
            if (data != null)
            {
                List<CatalogItem> Items = GetItems(data.Id);
                foreach (CatalogItem Item in Items)
                {
                    lblRec.Text = Item.Id.ToString();
                    Application.DoEvents();

                    Item.Title = GetTitle(Item.FullFilename, Item);
                    Item.SoundexTitle = Soundex.Generate(Item.Title, 10);

                    if (chkTmdb)
                    {
                        GetMovieTmdbData(Item, Item.FullFilename, chkTmdb);
                    }

                    var recs = UpdateItem(Item);

                    lblRec.Text = Item.Id.ToString() + "ok";
                    Application.DoEvents();

                    UpdateDbNestedNodeTitle(Item, chkTmdb);
                }
            }
        }

        private void UpdateNodeDataTitle(bool chkTmdb)
        {
            CatalogItem data = (CatalogItem)tvCatalogs.SelectedNode.Tag;
            tvCatalogs.SelectedNode.ExpandAll();
            if (data != null)
            {
                lblRec.Text = data.Id.ToString();
                Application.DoEvents();
                if (data.Type == ItemType.Volume)
                {
                    data.Title = data.Name;
                }
                else
                {
                    data.Title = GetTitle(data.FullFilename, data);
                }
                data.SoundexTitle = Soundex.Generate(data.Title, 10);
                if (chkTmdb)
                {
                    GetMovieTmdbData(data, data.FullFilename, chkTmdb);
                }
                var recs = UpdateItem(data);

                lblRec.Text = data.Id.ToString() + "ok";
                Application.DoEvents();

                tvCatalogs.SelectedNode.Tag = data;
                tvCatalogs.SelectedNode.Text = data.Title;
                pptGridCatalogs.SelectedObject = tvCatalogs.SelectedNode.Tag;
                if (tvCatalogs.SelectedNode.Nodes.Count>0)
                {
                    foreach(TreeNode node in tvCatalogs.SelectedNode.Nodes)
                    {
                        UpdateNestedNode(node, chkTmdb);
                    }
                }
                else // tenta pela base de dados
                {
                    UpdateDbNestedNodeTitle(data, chkTmdb);
                }
            }
        }

        public void CreateDB()
        {
            //return;
            try
            {

                //SqLiteFile = Application.StartupPath + "\\Movies.Catlog"; // CatalogFolder
                SqLiteFile = CatalogFolder + "Movies.Catlog"; // CatalogFolder
                if (!File.Exists(SqLiteFile))
                {

                    System.Data.SQLite.SQLiteConnection.CreateFile(SqLiteFile);

                    var con = new SQLite.SQLiteConnection(SqLiteFile);
                    con.CreateTable<CatalogItem>();
                    con.CreateIndex("CatalogItem", new string[] { "VolumeId", "FatherId" });
                    con.CreateIndex("CatalogItem", new string[] { "FatherId" , "VolumeId" });
                    con.Close();

                }
            }
            catch (Exception ex) { }

        }

        private int UpdateItem(CatalogItem Data)
        {
            var con = new SQLite.SQLiteConnection(SqLiteFile);
            int id=con.Update(Data);
            con.Close();

            return id;

        }

        private int InsertItem(CatalogItem Data)
        {
            var con = new SQLite.SQLiteConnection(SqLiteFile);

            // testar se´já está no bd
            int id = 0;
            int idx = 0;
            try
            {
                //String sql = $"select * from CatalogItem where Type = { ((int)Data.Type).ToString() } and VolumeId = {Data.VolumeId.ToString()} and FullFilename = '{ Data.FullFilename }' ";

                String sql = $"select * from CatalogItem where Type = { ((int)Data.Type).ToString() } and VolumeId = {Data.VolumeId.ToString()} and FullFilename = '{ Data.FullFilename }' ";
                CatalogItem dplItm = con.FindWithQuery<CatalogItem>(sql);
                if (dplItm != null)
                {
                    idx = dplItm.Id;
                }
            }
            catch (Exception ex) {
                idx = 0;
            }
            // inserir...
            if ( idx == 0)
            {
                id = con.Insert(Data);
                id = (int)con.ExecuteScalar<int>(@"select last_insert_rowid()");
                con.Close();
                return id;
            }
            else
            {
                con.Close();
                return idx;
            }
        }

        private CatalogItem GetItem(int id)
        {

            var con = new SQLite.SQLiteConnection(SqLiteFile);
            var data = con.Query<CatalogItem>("select * from CatalogItem Where id = ?", new object[] { id } ).FirstOrDefault();
            con.Close();

            return data;

        }

        private int DeleteId(int Id)
        {

            var con = new SQLite.SQLiteConnection(SqLiteFile);
            int id = con.Delete<CatalogItem>(Id);
            con.Close();

            return id;

        }

        public void SaveSons(CatalogItem Sons, int Father)
        {
            foreach(var Movie in Sons.Items)
            {
                Movie.FatherId = Father;
                int FatherId = InsertItem(Movie);
                if (Movie.Items != null && Movie.Items.Count > 0)
                {
                    if (Movie.FullFilename==null)
                    {
                        var x = Movie;
                    }
                    SaveSons(Movie, FatherId);
                }
            }
            return;
        }

        private void SaveCatalog(CatalogItem Movies, int FatherId)
        {
            // Apagar todos os registros do Volume/MArcar como Em Ajuste...
            // Ler a estrutura do catalogo.. item a item.. sub item a sub item...
            // SAlvar o item e guardar o id como pai do prx nivel

            var con = new SQLite.SQLiteConnection(SqLiteFile);
            con.DeleteAllForVolumeId("CatalogItem", "VolumeId", curVolumeId);
            con.Close();

            Movies.FatherId = FatherId;
            int Father = InsertItem(Movies);
            if (Movies.Items != null && Movies.Items.Count > 0)
            {
                SaveSons(Movies, Father);
            }
        }

        private List<CatalogItem> GetVolumes()
        {
            var con = new SQLite.SQLiteConnection(SqLiteFile);
            var data = con.Query<CatalogItem>("select * from CatalogItem Where Type = ?", new object[] { (int)ItemType.Volume });
            con.Close();
            return data;
        }

        private List<CatalogItem> GetItems(int? FatherId)
        {
            var con = new SQLite.SQLiteConnection(SqLiteFile);
            var data = con.Query<CatalogItem>("select * from CatalogItem Where FatherId = ?", FatherId);
            con.Close();
            return data;
        }

        private void LoadCatalogs(TreeNode node=null, int? Father = null)
        {

            List<CatalogItem> data = new List<CatalogItem>();

            if (node != null)
            {
                tvCatalogs.SelectedNode = node;
                if (node.GetNodeCount(true) > 0)
                {
                    return;
                }
                CatalogItem obj = (CatalogItem)node.Tag; 
                data = GetItems(obj.Id);
            }



            if (Father == null || Father == 0)
            {
                tvCatalogs.Nodes.Clear();
                //data = GetVolumes();
                data = GetItems(0);
            }
            foreach (CatalogItem itm in data)
            {
                TreeNode cNode = new TreeNode();
                if (node != null)
                {
                    tvCatalogs.SelectedNode = node;
                    cNode = tvCatalogs.SelectedNode.Nodes.Add(itm.Title);
                    SetTreeNode(cNode, itm);
                }
                else
                {
                    cNode = tvCatalogs.Nodes.Add(itm.VolumeName);
                    SetTreeNode(cNode, itm);
                }
                cNode.Tag = itm;
                if (itm.Type == ItemType.Volume)
                {

                }
            }
        }

        #endregion

        private void SetCurMovie()
        {
            pptGrdDetailData.SetLabelColumnWidth(100);
            pptGrdDetailData.SelectedObject = tvFoldersCat.SelectedNode.Tag;
            CurCatItem = (CatalogItem)tvFoldersCat.SelectedNode.Tag;
            LoadImages();
        }

        private void SetCurMovieCatalog()
        {
            pptGridCatalogs.SetLabelColumnWidth(100);
            pptGridCatalogs.SelectedObject = tvCatalogs.SelectedNode.Tag;
            CurCatItem = (CatalogItem)tvCatalogs.SelectedNode.Tag;
            LoadImagesCatalogs();
        }

        private CatalogItem AddCatalogItem (CatalogItem BaseItem, ItemType ItemType, string Name)
        {
            CatalogItem newItem = new CatalogItem();
            FillCatalogItem(newItem, ItemType, Name);

            if (BaseItem.Items == null)
            {
                BaseItem.Items = new List<CatalogItem>();
            }
            BaseItem.Items.Add(newItem);

            return newItem;
        }

        private void FillCatalogItem(CatalogItem RefItem, ItemType ItemType, string Name)
        {
            RefItem.Name = Name;
            RefItem.Type = ItemType;
            return ;
        }

        private String ReadTmdbJsonFile (List<string> years, CatalogItem catItem)
        {
            string filepath = "";
            string data = "";
            string appPath = Application.StartupPath;

            bool done = false;

            if (years.Count > 0)
            {
                foreach (string year in years)
                {
                    catItem.Year = Convert.ToInt32(year);
                    try
                    {
                        //filepath = Path.Combine(appPath, "TMDB", "Movies", catItem.Title + " ("+year.ToString()+").movie");
                        filepath = Path.Combine(CatalogFolder, "TMDB", "Movies", catItem.Title + " (" + year.ToString() + ").movie");
                        try
                        {
                            using (StreamReader streamReader = File.OpenText(filepath))
                            {
                                data = streamReader.ReadToEnd();
                            }
                        }
                        catch (Exception ex) {}
                        if (data != "") { }
                        done = true;
                        break;
                    }
                    catch (Exception ex) {}
                    if (done) break;
                }
            }
  
            if(data == "")
            {
                //filepath = Path.Combine(appPath, "TMDB", "Movies", catItem.Title + " (TMDB).movie");
                filepath = Path.Combine(CatalogFolder, "TMDB", "Movies", catItem.Title + " (TMDB).movie");
                try
                {
                    using (StreamReader streamReader = File.OpenText(filepath))
                    {
                        data = streamReader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return data;
        }

        private String ReadCrcJsonFile(CatalogItem catItem)
        {
            string filepath = "";
            string data = "";

            try
            {
                filepath = Path.Combine(catItem.FileInfo.FullName + ".crc");
                try
                {
                    using (StreamReader streamReader = File.OpenText(filepath))
                    {
                        data = streamReader.ReadToEnd();
                    }
                }
                catch (Exception ex) { }
                if (data != "") { }

            }
            catch (Exception ex) { }

            return data;
        }

        private void WriteTmdbJsonToFile(CatalogItem XcatItem)
        {
            string filepath = "";
            if (XcatItem.Year > 0)
            {
                //filepath = Path.Combine("TMDB", "Movies", XcatItem.Title + " (" + XcatItem.Year.ToString() + ").movie");
                filepath = Path.Combine(CatalogFolder, "TMDB", "Movies", XcatItem.Title + " (" + XcatItem.Year.ToString() + ").movie");
            }
            else
            {
                //filepath = Path.Combine("TMDB", "Movies", XcatItem.Title + " (TMDB).movie");
                filepath = Path.Combine(CatalogFolder, "TMDB", "Movies", XcatItem.Title + " (TMDB).movie");
            }
            if (XcatItem.JsonMoviesData != null)
            {
                File.WriteAllText(filepath, XcatItem.JsonMoviesData);
            }
        }

        private void WriteCrcJsonToFile(CatalogItem catItem)
        {
            string filepath = "";

            filepath = Path.Combine(catItem.FileInfo.FullName + ".crc");
            
            if (catItem.JsonCRC != null)
            {
                File.WriteAllText(filepath, catItem.JsonCRC);
            }
        }

        private void GetMovieCatalog(List<string> years , CatalogItem catItem, bool GetTMDB)
        {
            if (!GetTMDB) return;

            CancellationToken cToken = new CancellationToken();

            bool done = false;
 
            if (years.Count > 0)
            {
                foreach (string year in years)
                {
                    catItem.Year = Convert.ToInt32(year);
                    try
                    {
                        Task<Movies> task = Task.Run<Movies>(async () => await GetMovieData(CatalogFolder, catItem.Title, catItem.Year, catItem.Season, catItem.Episode, cToken, TMDB_ApiKey));
                        if (task.Result != null && task.Result.TotalCount > 0)
                        {
                            var settings = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                            catItem.JsonMoviesData = Newtonsoft.Json.JsonConvert.SerializeObject(task.Result,settings);                           
                            done = true;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            if (years.Count == 0 || !done)
            {
                try
                {
                    Task<Movies> task = Task.Run<Movies>(async () => await GetMovieData(CatalogFolder, catItem.Title, 0, 0, 0, cToken, TMDB_ApiKey));
                    if (task.Result != null && task.Result.TotalCount > 0)
                    {
                        var settings = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };
                        catItem.JsonMoviesData = Newtonsoft.Json.JsonConvert.SerializeObject(task.Result,settings);                      
                    }
                }
                catch (Exception ex)
                {

                }
            }
            WriteTmdbJsonToFile(catItem);
        }

        private void SetTreeNode(TreeNode node, CatalogItem itm, string Extra = "")
        {
            if (itm.Type == ItemType.Volume)
            {
                node.ImageKey = Extra;
                node.SelectedImageKey = Extra;
                node.StateImageKey = Extra;
                node.Tag = itm;
            }

            if (itm.Type == ItemType.Folder)
            {
                node.ImageKey = "_FOLDER_";
                node.SelectedImageKey = "_FOLDER_";
                node.StateImageKey = "_FOLDER_";
            }

            if (itm.Type == ItemType.File)
            {
                string filename = itm.FullFilename;
                updtIcons(filename);
                if (Path.GetExtension(filename).ToUpper() != ".EXE")
                {
                    node.ImageKey = Path.GetExtension(filename);
                    node.SelectedImageKey = Path.GetExtension(filename);
                    node.StateImageKey = Path.GetExtension(filename);
                }
                else
                {
                    node.ImageKey = Path.GetFileName(filename);
                    node.SelectedImageKey = Path.GetFileName(filename);
                    node.StateImageKey = Path.GetFileName(filename);
                }
            }

            node.Tag = itm;
        }

        private void GetMovieTmdbData(CatalogItem itm, string path, bool GetMovieTmdbInfo)
        {
            lblRec.Text = itm.Id.ToString() + " tmdb";
            Application.DoEvents();

            List<string> years = Path.GetFileName(path).GetYearsFromString(); //  GetYearsFromString(Path.GetFileName(path));
                                                                              // tentar achar o arquivo json na pasta    
            string dataMovie = ReadTmdbJsonFile(years, itm);
            if (dataMovie.Length > 0)
            {

                lblRec.Text = itm.Id.ToString() + " tmdb by json";
                Application.DoEvents();
                itm.JsonMoviesData = dataMovie;

            }
            if ((itm.JsonMoviesData == null || itm.JsonMoviesData == "") && (GetMovieTmdbInfo))
            {
                lblRec.Text = itm.Id.ToString() + " tmdb by service";
                Application.DoEvents();
                GetMovieCatalog(years, itm, GetMovieTmdbInfo);
            }
            lblRec.Text = itm.Id.ToString() + " tmdb done";
            Application.DoEvents();

        }

        private CatalogItem SetCatalogItem(ItemType Type, string path, string Name, CatalogItem bItem, DriveInfo drive = null, bool CheckCrc = false, bool GetMovieInfo = false)
        {
            CatalogItem itm = new CatalogItem();

            if (Type == ItemType.Volume)
            {
                FillCatalogItem(bItem, ItemType.Volume, Name);

                if (drive.IsReady == true)
                {
                    curVolumeId = HD.getSerial(drive.Name);
                    curVolumeName = drive.VolumeLabel;
                   
                    bItem.DiveInfo = drive;
                    bItem.Size = drive.TotalSize;
                    bItem.UnsudedSpace = drive.AvailableFreeSpace;
                    bItem.VolumeId = curVolumeId;
                    bItem.VolumeName = curVolumeName;
                    bItem.Name = curVolumeName;
                    bItem.Title = curVolumeName;
                    bItem.FullFilename = drive.Name;
                }
            }

            if (Type == ItemType.Folder)
            {
                itm = AddCatalogItem(bItem, ItemType.Folder, Name);

                string cpath = Path.Combine(path, Name);
                FileInfo fi = new FileInfo(cpath);

                var settings = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };

                itm.JsonFileInfo = Newtonsoft.Json.JsonConvert.SerializeObject(fi, settings);
                itm.FileInfo = fi;
                itm.CreationDate = fi.CreationTime;
                itm.ModifiedDate = fi.LastWriteTime;
                itm.VolumeId = curVolumeId;
                itm.VolumeName = curVolumeName;
                itm.Title = GetTitle(Name, bItem);
                itm.FullFilename = cpath;
                bItem.Year = Convert.ToInt16(cpath.GetYearsFromString().FirstOrDefault());
                try
                {
                    itm.Size = fi.Length;
                }
                catch { }

            }

            if (Type == ItemType.File)
            {
                itm = AddCatalogItem(bItem, ItemType.File, Name);

                FileInfo fi = new FileInfo(path);
                var settings = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };

                itm.JsonFileInfo = Newtonsoft.Json.JsonConvert.SerializeObject(fi, settings);
                itm.FileInfo = fi;
                itm.Size = fi.Length;
                itm.FullFilename = path;
                itm.CreationDate = fi.CreationTime;
                itm.ModifiedDate = fi.LastWriteTime;
                itm.VolumeId = curVolumeId;
                itm.VolumeName = curVolumeName;
                itm.Season = getSeasonEpisode("s", path);
                itm.Episode = getSeasonEpisode("e", path);
                itm.Title = GetTitle(path, itm);
                bItem.Year = Convert.ToInt16(path.GetYearsFromString().FirstOrDefault());

                if (CheckCrc)
                {
                    if (Array.IndexOf(allowedVideos, Path.GetExtension(path)) >= 0)
                    {
                        catMonitor.Text = "CRC CALC for: " + path;
                        Application.DoEvents();
                        try
                        {
                            Crc32 result = CRC(path);
                            itm.JsonCRC = Newtonsoft.Json.JsonConvert.SerializeObject(CRC(path), settings);
                            WriteCrcJsonToFile(itm);
                        }
                        catch
                        {
                            //chkCRC.Checked = false;
                            MessageBox.Show("CRC Process Error for: " + path, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    itm.JsonCRC = ReadCrcJsonFile(itm);
                }

                // apenas para videos
                if (Array.IndexOf(allowedVideos, Path.GetExtension(path)) >= 0)
                {
                    GetMovieTmdbData(itm, path, GetMovieInfo);
                }
            }

            return itm;
        }

        private TreeNode doCatalog(TreeView Tree,  String path, TreeNode node, CatalogItem baseItem, bool CheckCrc, bool GetMovieTmdbInfo)
        { 
            catMonitor.Text = path;

            // apenas para compor os nós iniciais
            if (node == null)
            {
                string[] pts = path.Split('\\');
                bool first = true;
                string cpath = pts[0]+"\\";
                string npath = cpath;

                foreach (string pt in pts)
                {
                   
                    if (!first)
                    {
                        tvFoldersCat.SelectedNode= node;
                        node = tvFoldersCat.SelectedNode.Nodes.Add(pt);

                        baseItem = SetCatalogItem(ItemType.Folder, npath, pt, baseItem);
                        SetTreeNode(node, baseItem);
                        npath = Path.Combine(npath, pt);
                    }
                    else {
                        DriveInfo[] myDrives = DriveInfo.GetDrives();
                        foreach(var drive in myDrives)
                        {
                            if (drive.Name==pt+"\\")
                            {
                                SetSystemIcon(pt);
                                node = tvFoldersCat.Nodes.Add(drive.VolumeLabel); // aqui colocar o nome do disco
                                SetCatalogItem(ItemType.Volume, "", "", baseItem, drive);
                                SetTreeNode(node, baseItem, pt);
                                first = false;
                            }
                        }
                        npath = pt + "\\";
                    }
                   
                }
               // return null;
            }

            // tratar a pasta definida na chamada
            if (Directory.Exists(path))
            {
                // cada arquivo na pasta
                Tree.SelectedNode = node;
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    catMonitor.Text = file;
                    string[] pts = file.Split('\\');
                    Tree.SelectedNode = node;
                    string filename = pts[pts.Length - 1];
                    var nodef = Tree.SelectedNode.Nodes.Add(filename);
                    var catItem = SetCatalogItem(ItemType.File, file, filename, baseItem, null, CheckCrc, GetMovieTmdbInfo);
                    SetTreeNode(nodef, catItem);
                }

                // as demais sub-pastas
                string[] subdirectoryEntries = Directory.GetDirectories(path);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    catMonitor.Text = subdirectory;
                    string[] pts = subdirectory.Split('\\');
                    Tree.SelectedNode = node;
                    var nodec = Tree.SelectedNode.Nodes.Add(pts[pts.Length - 1]);
                    string npath = pts[0] + "\\"; 
                    for(int i = 1; i<pts.Length-1; i++)
                    {
                        npath = Path.Combine(npath, pts[i]);
                    }
                    var catItem = SetCatalogItem(ItemType.Folder, npath, pts[pts.Length - 1], baseItem);
                    SetTreeNode(nodec, catItem);
                    doCatalog(Tree, subdirectory, nodec, catItem, CheckCrc, GetMovieTmdbInfo);
                }
                Application.DoEvents();
            }
            else
            {
                node = null;
            }

            return node;
        }

        private String GetTitle(String Data, CatalogItem Item)
        {
            string sToSearch = ClearStringData(RemoveExtension(Path.GetFileName(Data)), true, true);

            if (Item.Season > 0)
            {
                sToSearch = RemoveSeasonEpisodeFromName("s", sToSearch, Item.Season);
            }
            if (Item.Episode > 0)
            {
                sToSearch = RemoveSeasonEpisodeFromName("e", sToSearch, Item.Episode);
            }

            return sToSearch;
        }

        private void MountCatalog()
        {
            // começa zerando o catalog em processo de leitura
            ReadingData = new CatalogItem();
            pptGrdDetailData.SetLabelColumnWidth(100);
            pptGrdDetailData.SelectedObject = null;
            tvJsonDetail.Nodes.Clear();

            tvFoldersCat.Nodes.Clear();
            //Application.DoEvents();
            tvFoldersCat.BeginUpdate();
            if (listFolders.Items.Count == 0)
            {
                if (txtFolderName.Text != "" && txtFolderName.Text != null)
                {
                    string path = txtFolderName.Text;
                    doCatalog(tvFoldersCat, path, null, ReadingData, chkCRC.Checked, chkTmdb.Checked);
                }
            }
            else
            {
                foreach (var itm in listFolders.Items)
                {
                    doCatalog(tvFoldersCat, itm.ToString(), null, ReadingData, chkCRC.Checked, chkTmdb.Checked);
                }
            }
            catMonitor.Text = "";
            tvFoldersCat.EndUpdate();
        }

        private void getDataFromTmdb(string Mame)
        {
            //string call = "https://api.themoviedb.org/3/movie/550?api_key=b54df3a8957fa96c9d23412c528bb667";
            //var request = (HttpWebRequest)WebRequest.Create(string.Format("http://api.themoviedb.org/3/movie/{1}?api_key={0}", ApiKey, tmdb));
            var request = (HttpWebRequest)WebRequest.Create(string.Format("http://api.themoviedb.org/3/movie/{1}?api_key={0}", TMDB_ApiKey));
            request.Method = "GET";
            request.Accept = "application/json";
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.ContentLength = 0;
            string json;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (null != response && HttpStatusCode.OK == response.StatusCode)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }
        }

        private void SetTreeMovieObjectDetail(String Descriptor,Object Value)
        {

            switch (Descriptor)
            {
                case "JSONMOVIESDATA":
                    if (Value != null)
                    {
                        try
                        {
                            tvJsonDetail.SetObjectAsJson(JToken.Parse(Value.ToString()));
                            tvJsonDetail.ExpandAll();
                        }
                        catch { }
                    }
                    break;
                case "JSONFILEINFO":
                    if (Value != null)
                    {
                        try
                        {
                            tvJsonDetail.SetObjectAsJson(JToken.Parse(Value.ToString()));
                            tvJsonDetail.ExpandAll();
                        }
                        catch { }
                    }
                    break;
                case "JSONCRC":
                    if (Value != null)
                    {
                        try
                        {
                            tvJsonDetail.SetObjectAsJson(JToken.Parse(Value.ToString()));
                            tvJsonDetail.ExpandAll();
                        }
                        catch { }
                    }
                    break;
                default:
                    tvJsonDetail.Nodes.Clear();
                    break;
            }
        }

        private int getSeasonEpisode(string Mode, string Name) // mode must be E or S
        {
            int result = 0;
            string uName = Name.ToUpper();
            Mode = Mode.ToUpper();

            if (result == 0)
            {
                for (int i = 1; i < 100; i++)
                {
                    String s = Mode + ("000" + i.ToString()).Right(3);
                    s = Mode + s;
                    if (uName.IndexOf(s) > 0)
                    {
                        result = i;
                    }
                }
            }

            if (result == 0)
            {
                for (int i = 1; i < 100; i++)
                {
                    string s = Mode + ("000" + i.ToString()).Right(2);
                    if (uName.IndexOf(s) > 0)
                    {
                        result = i;
                    }
                }
            }

            if (result == 0)
            {
                for (int i = 1; i < 10; i++)
                {
                    string s = Mode + ("000" + i.ToString()).PadRight(1);
                    if (uName.IndexOf(s) > 0)
                    {
                        result = i;
                    }
                }
            }

            return result; ;
        }

        private string RemoveSeasonEpisodeFromName(string Mode, string Name, int i) // mode must be E or S
        {
            Mode = Mode.ToUpper();

            string ResultString = Name;
            string uName = Name.ToUpper();
            string s = Mode.ToUpper() + ("000" + i.ToString()).Right(3);

            ResultString = ResultString.Replace(s, "");
            s = Mode.ToLower() + ("000" + i.ToString()).Right(3);
            ResultString = ResultString.Replace(s, "");

            if (i < 100)
            {
                s = Mode.ToUpper() + ("000" + i.ToString()).Right(2);
                ResultString = ResultString.Replace(s, "");
                s = Mode.ToLower() + ("000" + i.ToString()).Right(2);
                ResultString = ResultString.Replace(s, "");
            }

            if (i < 10)
            {
                s = Mode.ToUpper() + ("000" + i.ToString()).Right(1);
                ResultString = ResultString.Replace(s, "");
                s = Mode.ToLower() + ("000" + i.ToString()).Right(1);
                ResultString = ResultString.Replace(s, "");
            }

            return ClearStringData(ResultString,false,true); ;
        }

        #endregion

        #region Movie info 

        // for catlog aditional data 
        // https://www.themoviedb.org/documentation/api // joynernetwork psylocke
        // http://www.omdbapi.com/

        //static async Task<Movies> GetMovieData(CatalogItem Item, CancellationToken cancellationToken, String TMDB_ApiKey)
        //{
        //    Task<Movies> task = Task.Run<Movies>(async () => await GetMovieData(Item.Title, Item.Year, Item.Season, Item.Episode, cancellationToken, TMDB_ApiKey));
        //    return await task;
        //}

        static async Task<Movies> GetMovieData(String CatalogFolder, String Name, int year, int Season, int Episode,  CancellationToken cancellationToken, String TMDB_ApiKey)
        {
            Movies result = new Movies();
            using (var client = new ServiceClient(TMDB_ApiKey))
            {
      
                var movies = await client.Movies.SearchAsync(Name, null, true,year,true, 1, cancellationToken);
                result = movies;

                foreach (Movie m in movies.Results)
                {
                    var movie = await client.Movies.GetAsync(m.Id, null, true, cancellationToken);

                    var personIds = movie.Credits.Cast.Select(s => s.Id)
                        .Union(movie.Credits.Crew.Select(s => s.Id));

                    foreach (var id in personIds)
                    {
                        var person = await client.People.GetAsync(id, true, cancellationToken);
                        // person.Name
                        string PersonName = person.Name;
                        int ImageNumber = 1;
                        foreach (var img in person.Images.Results)
                        {
                            try
                            {
                                String MovieName = m.OriginalTitle;
                                if (MovieName == "")
                                {
                                    MovieName = Name;
                                }
                                //string filepath = Path.Combine("TMDB", "Movies", Name, MovieName.FileNameClean(), "Peoples", PersonName.FileNameClean() + " (" + ImageNumber.ToString() + ")" + Path.GetExtension(img.FilePath));
                                string filepath = Path.Combine(CatalogFolder, "TMDB", "Movies", Name, MovieName.FileNameClean(), "Peoples", PersonName.FileNameClean() + " (" + ImageNumber.ToString() + ")" + Path.GetExtension(img.FilePath));
                                await DownloadImage(img.FilePath, filepath, cancellationToken);
                            }
                            catch { }
                        }
                    }
                    try
                    {
                        String MovieName = m.OriginalTitle;
                        if (m.Poster != null)
                        {
                            //string filepathMovie = Path.Combine("TMDB", "Movies", Name, MovieName.FileNameClean(), "Poster" + Path.GetExtension(m.Poster));
                            string filepathMovie = Path.Combine(CatalogFolder, "TMDB", "Movies", Name, MovieName.FileNameClean(), "Poster" + Path.GetExtension(m.Poster));
                            await DownloadImage(m.Poster, filepathMovie, cancellationToken);
                        }
                        if (m.Backdrop != null)
                        {
                            //string filepathMovie = Path.Combine( "TMDB", "Movies", Name, MovieName.FileNameClean(), "Backdrop" + Path.GetExtension(m.Poster));
                            string filepathMovie = Path.Combine(CatalogFolder, "TMDB", "Movies", Name, MovieName.FileNameClean(), "Backdrop" + Path.GetExtension(m.Poster));
                            await DownloadImage(m.Backdrop, filepathMovie, cancellationToken);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                
            }
            return result;
        }

        static async Task DownloadImage(string filename, string localpath, CancellationToken cancellationToken)
        {
            if (!File.Exists(localpath))
            {
                string folder = Path.GetDirectoryName(localpath);
                Directory.CreateDirectory(folder);

                var storage = new StorageClient();
                using (var fileStream = new FileStream(localpath, FileMode.Create,
                    FileAccess.Write, FileShare.None, short.MaxValue, true))
                {
                    try { await storage.DownloadAsync(filename, fileStream, cancellationToken); }
                    catch (Exception ex) { System.Diagnostics.Trace.TraceError(ex.ToString()); }
                }
            }
        }

        private void SetImages(int index)
        {
            int imgIndex = imageListPosters.Count - index - 1;
            pictBackdrop.Image = null;
            pictPoster.Image = null;
            String Name = "";
            try
            {
                pictPoster.Image = imageListPosters[imgIndex].Image;
                Name = imageListPosters[imgIndex].Key;
            }
            catch { }
            try
            {
                foreach (var nm in imageListBackdrop)
                {
                    if (nm.Key == Name)
                    {
                        pictBackdrop.Image = nm.Image;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void SetImagesCatalog(int index)
        {
            int imgIndex = imageListPosters.Count - index - 1;
            pictBackdropCatalogs.Image = null;
            pictPosterCatalogs.Image = null;
            String Name = "";
            try
            {
                pictPosterCatalogs.Image = imageListPosters[imgIndex].Image;
                Name = imageListPosters[imgIndex].Key;
            }
            catch { }
            try
            {
                foreach (var nm in imageListBackdrop)
                {
                    if (nm.Key == Name)
                    {
                        pictBackdropCatalogs.Image = nm.Image;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void SetMainPoster(int index)
        {
            int itemIndex = imageListPosters.Count - index - 1;
            try
            {
                String Name = imageListPosters[itemIndex].Key;
                CurCatItem.TmdbMainMovieFolder = Name;
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadImages(CatalogItem catItm, string ImageName,  PictureBox Pict,  List<MovieImage> PictList, DataGridView GridList = null)
        {
            Pict.Image = null;
            PictList.Clear();

            if (GridList!=null)
            {
                GridList.DataSource = null;
                while (GridList.Rows.Count > 0)
                {
                    GridList.Rows.Clear();
                }
            }

            try
            {
                CatalogItem catItem = catItm; // (CatalogItem)pptGrdDetailData.SelectedObject;
                String MovieName = catItem.Title;
                String Name = catItem.Name;
                String filepathMovie = Path.Combine(CatalogFolder, "TMDB", "Movies", MovieName);
                String[] subdirectoryEntries = Directory.GetDirectories(filepathMovie);
                if (subdirectoryEntries.Length > 0)
                {
                    bool first = true;
                    foreach (string folder in subdirectoryEntries)
                    {
                        try
                        {
                            string filepathMovieName = Path.Combine(folder, ImageName);
                            string FolderName = new DirectoryInfo(System.IO.Path.GetDirectoryName(folder + "\\")).Name;
                            var tempImage = System.Drawing.Image.FromFile(filepathMovieName); //Load the image from directory location
                            Bitmap pic = new Bitmap(tempImage.Width, tempImage.Height);

                            using (Graphics g = Graphics.FromImage(pic))
                            {
                                g.DrawImage(tempImage, new Rectangle(0, 0, pic.Width, pic.Height)); //redraw smaller image
                            }

                            tempImage.Dispose();

                            var img = new MovieImage();
                            img.Key = FolderName;
                            img.Image = pic;

                            PictList.Add(img);

                            if (GridList != null)
                            {
                                GridList.Rows.Insert(0, pic);
                                GridList.Rows[0].Height = 108;
                            }

                            if (first)
                            {
                                first = false;
                                Pict.Image = img.Image;
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            catch { }
        }

        private void LoadImages()
        {
            LoadImages((CatalogItem)pptGrdDetailData.SelectedObject, "Poster.Jpg", pictPoster, imageListPosters, dataGridViewImage);
            LoadImages((CatalogItem)pptGrdDetailData.SelectedObject, "BAckdrop.Jpg", pictBackdrop, imageListBackdrop);
        }

        private void LoadImagesCatalogs()
        {
            LoadImages((CatalogItem)pptGridCatalogs.SelectedObject, "Poster.Jpg", pictPosterCatalogs, imageListPosters, dtGridCatalogs);
            LoadImages((CatalogItem)pptGridCatalogs.SelectedObject, "BAckdrop.Jpg", pictBackdropCatalogs, imageListBackdrop);
        }

        #endregion

    }

}



