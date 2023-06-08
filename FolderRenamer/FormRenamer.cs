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
using System.Linq.Expressions;
using static SQLite.SQLite3;

namespace FolderRenamer
{

    public partial class mainForm : Form
    {

        #region Properties

        private List<String> ToRemoveAll = new List<string>();
        private List<String> ToRemoveFolder = new List<string>();
        private List<String> ToRemoveFile = new List<string>();
        private List<String> Patterns = new List<string>();
        private int maxLengthForPattern = 50;
        private Boolean SilentMode = false;
        private Boolean GetTmdbPeople = false;
        private string SqLiteFile = "";
        private string[] allowedVideos;
        private string[] allowedSub;
        private string[] ignoreFolder;
        private Catalog Catalog = new Catalog();
        private CatalogItem ReadingData = new CatalogItem();
        private List<MovieImage> imageListPosters = new List<MovieImage>();
        private List<MovieImage> imageListBackdrop = new List<MovieImage>();
        private List<MovieImage> imageListActors = new List<MovieImage>();
        private uint? curVolumeId;
        private string curVolumeName;
        private string CatalogFolder = "";
        private int PptGridColunWidth = 150;

        //private MovieCatalog CurCatItem = new MovieCatalog();
        private CatalogItem CurCatItem = new CatalogItem();

        private string TMDB_ApiKey = "b54df3a8957fa96c9d23412c528bb667";
        private string TMDB_Token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiNTRkZjNhODk1N2ZhOTZjOWQyMzQxMmM1MjhiYjY2NyIsInN1YiI6IjYxOGU5NGU5NjZhN2MzMDA2NGJmZDFmMiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.7OeQCMpl6s7zpGvsRD4LaKS-ETGDe7T8_hH5Tqi5mAA";

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
            GetTmdbPeople = Convert.ToBoolean(ConfigurationManager.AppSettings["GetTmdbPeople"]);
            SilentMode = Convert.ToBoolean(ConfigurationManager.AppSettings["silentMode"]);

            if (SilentMode)
            {
                tvFolders.Visible = false;
            }
            ResizeForm();

            allowedVideos = ConfigurationManager.AppSettings["video"].Split(',').ToArray();
            allowedSub = ConfigurationManager.AppSettings["subtitle"].Split(',').ToArray();
            ignoreFolder = ConfigurationManager.AppSettings["ignoreFolder"].ToUpper().Split(',').ToArray();

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
      
            // load data from db (root)
            LoadRoot();

            pptGridCatalogs.MoveSplitterTo(PptGridColunWidth);
            pptGrdDetailData.MoveSplitterTo(PptGridColunWidth);
        }

        public static void SetLabelColumnWidth(PropertyGrid grid, int width)
        {
            if (grid == null)
                return;
            FieldInfo fi = grid.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi == null)
                return;
            Control view = fi.GetValue(grid) as Control;
            if (view == null)
                return;
            MethodInfo mi = view.GetType().GetMethod("MoveSplitterTo", BindingFlags.Instance | BindingFlags.NonPublic);
            if (mi == null)
                return;
            mi.Invoke(view, new object[] { width });
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

            pptGridCatalogs.MoveSplitterTo(PptGridColunWidth);
            pptGrdDetailData.MoveSplitterTo(PptGridColunWidth);
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

        private async void  BtnProcess_Click(object sender, EventArgs e)
        {
             Process(chkAgressive.Checked, chkVideoRes.Checked);
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
        
        private async void btnCatalog_Click(object sender, EventArgs e)
        {
            await MountCatalog();
            //await Task.Run(MountCatalog);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDicts(); 
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About();
        }    

        private void btnAddToAll_Click(object sender, EventArgs e)
        {
            //AddToAll();
            AddToList(wordsToRemove, "ToRemove.txt");
        }

        private void btnAddToFolder_Click(object sender, EventArgs e)
        {
            //AddToFolder();
            AddToList(wordsToRemoveFolder, "ToRemoveFolder.txt");
        }

        private void btnAddToFile_Click(object sender, EventArgs e)
        {
            //AddToFile();
            AddToList(wordsToRemoveFile, "ToRemoveFile.txt");
        }        

        private void btnSearchPatterns_Click_1(object sender, EventArgs e)
        {
            SearchPatterns(chbRecursiveFoldersSearch.Checked);
        }

        private void btnSaveCatalog_Click(object sender, EventArgs e)
        {
            SaveCatalog(ReadingData, 0);
        }

        private async void btnReprocessData_Click(object sender, EventArgs e)
        {
            await ReprocessNodeDataTitle((CatalogItem)tvCatalogs.SelectedNode.Tag, chkTmdbDo.Checked, null, 0, chkRefreshTmdb.Checked);
        }

        private void btnLoadCatalogs_Click(object sender, EventArgs e)
        {
            LoadRoot();
        }

        private void btnTitleEdit_Click(object sender, EventArgs e)
        {
            EditTitle(tvCatalogs, pptGridCatalogs);
        }

        private void btnTitleEditCatalog_Click(object sender, EventArgs e)
        {
            EditTitle(tvFoldersCat, pptGrdDetailData);
        }

        private void btnReprocess_Click(object sender, EventArgs e)
        {
            UpdateNodeDataTitle(chkTmdbDo.Checked);
        }

        #endregion

        #region Menu

        private async void cleanNamesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await Process(chkAgressive.Checked, chkVideoRes.Checked);
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
            DialogResult result = MessageBox.Show("Maintain only this image, Proceed?", "Image Selection", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result.Equals(DialogResult.OK))
            {
                DeleteFolderExceptThis(tvCatalogs, e.RowIndex);
            }
        }

        private void tvCatalogs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            LoadNodes(e.Node);
        }

        private void tvCatalogs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetCurMovieCatalog();
        }

        private void chkOnlyWTmdb_CheckedChanged(object sender, EventArgs e)
        {
            LoadRoot();
        }

        private void chkNeedRemove_CheckedChanged(object sender, EventArgs e)
        {
            LoadRoot();
        }

        private void tvCatalogs_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            EditTitle(tvCatalogs, pptGridCatalogs);
        }

        private void tvFoldersCat_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            EditTitle(tvFoldersCat, pptGrdDetailData);
        }

        #endregion

        #endregion

        #region Parms To Rename File & Folders

        private void SaveDicts()
        {
            SaveFile(wordsToRemove, "ToRemove.txt");
            SaveFile(wordsToRemoveFolder, "ToRemoveFolder.txt");
            SaveFile(wordsToRemoveFile, "ToRemoveFile.txt");
            ToRemoveAll.Clear();
            ToRemoveFolder.Clear();
            ToRemoveFile.Clear();
            // reread
            ReadWords();
        }

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

        private void SaveFile(TextBox tb, string Fiename)
        {

            if ((tb.Text != null) && (tb.Text != ""))
            {
                string filePath = Path.Combine(CatalogFolder, Fiename);

                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    for (int i = 0; i < tb.Lines.Length; i++)
                    {

                        String text = tb.Lines[i].Trim();
                        if (text != "")
                        {
                            if (i == tb.Lines.Length - 2)
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

        private void About()
        {
            AboutBox form = new AboutBox();
            form.ShowDialog();
        }

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

                //foreach (var DictItem in ToRemoveAll)
                //{
                    CleanedString = CleanedString.RemoveFromList(ToRemoveAll, true);
                //}

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

        private void ProcessFolder(String path, bool AgressiveMode = false, bool VideoResolution = false)
        {
            TreeNode node = new TreeNode();
            if (File.Exists(path))
            {
                ProcessFile(path, AgressiveMode, VideoResolution);
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
                ProcessDirectory(path, node, AgressiveMode, VideoResolution);
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

        private Task Process(bool AgressiveMode = false, bool VideoResolution = false)
        {
            if (!SilentMode)
            {
                tvFolders.Nodes.Clear();
            }

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
                foreach (var itm in listFolders.Items)
                {
                    ProcessFolder(itm.ToString(), AgressiveMode, VideoResolution);
                }
            }
            SaveVideoLog();
            return null;
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public void ProcessDirectory(string targetDirectory, TreeNode node, bool AgressiveMode = false, bool VideoResulution = false)
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
                    if (Array.IndexOf(ignoreFolder, LastString(subdirectory.ToUpper())) == -1)
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

                        ProcessDirectory(subdirectory, snode, AgressiveMode, VideoResulution);
                        Application.DoEvents();
                    }
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
                ProcessFile(fileName, AgressiveMode, VideoResulution);
                Application.DoEvents();
            }
        }

        // Insert logic for processing found files here.
        public void ProcessFile(string originalFileName, bool AgressiveMode = false, bool VideoResolution = false )
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

                if (VideoResolution)
                {
                    try
                    {
                        VideoModel resolution = new VideoModel().VideoInfo(newFileName);
                        // buscar o width e o heigh no nome, se houver remover()
                        if (resolution != null)
                        {
                            cleanFileName = cleanFileName.Replace("480p", " ");
                            cleanFileName = cleanFileName.Replace("480", " ");
                            cleanFileName = cleanFileName.Replace("720p", " ");
                            cleanFileName = cleanFileName.Replace("720", " ");
                            cleanFileName = cleanFileName.Replace("1080p", " ");
                            cleanFileName = cleanFileName.Replace("1080", " ");
                            cleanFileName = Regex.Replace(cleanFileName, @"\[.*?\]", "");
                            cleanFileName = cleanFileName.Replace("[]", " ");
                            // incluir no final do nome o height entre "[" "]"
                            cleanFileName = cleanFileName.Replace("  ", " ");
                            var vRes = resolution.Height.ToString();
                            if (vRes != "480" && vRes != "720" && vRes != "1080" && vRes != "562")
                            {
                                switch (resolution.Width.ToString())
                                {
                                    case "1920":
                                        vRes = "1080";
                                        break;
                                    case "1280":
                                        vRes = "720";
                                        break;
                                    case "1000":
                                        vRes = "562";
                                        break;
                                    case "640":
                                        vRes = "480";
                                        break;
                                }
                            }



                            String newResFileName = Path.Combine(nPath, cleanFileName + " [" + vRes + "]" + nExtension);
                            try
                            {
                                File.Move(newFileName, newResFileName);
                                if (!SilentMode)
                                {
                                    fNode = tvFolders.SelectedNode.Nodes.Add(LastString(originalFileName) + " -> " + LastString(newResFileName));

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
                    catch { }
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
                if (Array.IndexOf(ignoreFolder, LastString(folder.ToUpper())) == -1)
                {
                    GetTokens(folder);
                    if (Recursive)
                    {
                        SearchPattern(folder, Recursive);
                    }
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

        public void AddToList(TextBox Tb, string Filename)
        {
            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow rw in dataGridView.Rows)
                {
                    if (Convert.ToBoolean(rw.Cells["Selected"].Value))
                    {
                        Tb.Text = Tb.Text + Environment.NewLine + Convert.ToString(rw.Cells["Pattern"].Value) + Environment.NewLine;
                    }
                }
            }
            SaveFile(Tb, Filename);
            ReadWords();
        }

        //public void AddToAll()
        //{
        //    if (dataGridView.Rows.Count > 0)
        //    {
        //        foreach(DataGridViewRow rw in dataGridView.Rows)
        //        {
        //            if (Convert.ToBoolean(rw.Cells["Selected"].Value))
        //            {
        //                wordsToRemove.Text = wordsToRemove.Text + Environment.NewLine + Convert.ToString(rw.Cells["Pattern"].Value) + Environment.NewLine;
        //                //Application.DoEvents();
        //            }
        //        }
        //    }
        //    SaveFile(wordsToRemove, "ToRemove.txt");
        //    ReadWords();
        //}

        //public void AddToFolder()
        //{
        //    if (dataGridView.Rows.Count > 0)
        //    {
        //        foreach (DataGridViewRow rw in dataGridView.Rows)
        //        {
        //            if (Convert.ToBoolean(rw.Cells["Selected"].Value))
        //            {
        //                wordsToRemoveFolder.Text = wordsToRemoveFolder.Text + Environment.NewLine + Convert.ToString(rw.Cells["Pattern"].Value) + Environment.NewLine;
        //                //Application.DoEvents();
        //            }
        //        }
        //    }
        //    SaveFile(wordsToRemoveFolder, "ToRemoveFolder.txt");
        //    ReadWords();
        //}

        //public void AddToFile()
        //{
        //    if (dataGridView.Rows.Count > 0)
        //    {
        //        foreach (DataGridViewRow rw in dataGridView.Rows)
        //        {
        //            if (Convert.ToBoolean(rw.Cells["Selected"].Value))
        //            {
        //                wordsToRemoveFile.Text = wordsToRemoveFile.Text + Environment.NewLine + Convert.ToString(rw.Cells["Pattern"].Value) + Environment.NewLine;
        //                //Application.DoEvents();
        //            }
        //        }
        //    }
        //    SaveFile(wordsToRemoveFile, "ToRemoveFile.txt");
        //    ReadWords();
        //}

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
                        string filename = Path.GetFileName(FullName);
                        var iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(filename);
                        imageListIcon.Images.Add(Extension, iconForFile);
                        imageListIcon.TransparentColor = Color.Black;
                    }
                    catch (Exception ex){
                        string ext = Path.GetExtension(FullName);
                        Bitmap icn  = AppIcon.GetFileIcon(ext);
                        imageListIcon.Images.Add(Extension, icn);
                        imageListIcon.TransparentColor = Color.Black;
                    }
                }
            }
            else
            {
                String Filename = Path.GetFileName(FullName);
                if (!imageListIcon.Images.ContainsKey(Filename))
                {
                    try
                    {
                        string filename = Path.GetFileName(FullName);
                        var iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(filename);
                        imageListIcon.Images.Add(Filename, iconForFile);
                        imageListIcon.TransparentColor = Color.Black;
                    }
                    catch (Exception ex)
                    {
                        string ext = Path.GetExtension(FullName);
                        Bitmap icn = AppIcon.GetFileIcon(ext);
                        imageListIcon.Images.Add(Extension, icn);
                        imageListIcon.TransparentColor = Color.Black;
                    }
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
            //int MaxAttempts = maxLengthForPattern = Convert.ToInt32(ConfigurationManager.AppSettings["MaxAttempts"]);
            int MaxAttempts = Convert.ToInt32(ConfigurationManager.AppSettings["MaxAttempts"]);
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

        private void UpdateNestedNode(TreeNode node, bool chkTmdb, bool ForceTmdb)
        {
            CatalogItem data = (CatalogItem)node.Tag;
            if (data != null)
            {
                lblRec.Text = data.Id.ToString();
                Application.DoEvents();

                data.Title = GetTitle(data.FullFilename, data);

                data.SoundexTitle = Soundex.Generate(data.Title, 10);

                if (chkTmdb || ForceTmdb)
                {
                    GetMovieTmdbData(data, data.FullFilename, chkTmdb, ForceTmdb);
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
                        UpdateNestedNode(cNode, chkTmdb, ForceTmdb);
                    }
                }
                else
                {
                    UpdateDbNestedNodeTitle(data, chkTmdb);
                }
            }
        }

        private void UpdateDbNestedNodeTitle(CatalogItem Data, bool chkTmdb, bool ForceTmdb = false)
        {
            if (Data != null)
            {
                List<CatalogItem> Items = GetItems(Data.Id);
                foreach (CatalogItem data in Items)
                {
                    lblRec.Text = data.Id.ToString();
                    Application.DoEvents();

                    data.Title = GetTitle(data.FullFilename, data);
                    data.SoundexTitle = Soundex.Generate(data.Title, 10);

                    if (Data.ManualIntervention == 1)
                    {
                        data.Title = Data.Title;
                        data.SoundexTitle = Data.SoundexTitle;
                        data.ManualIntervention = 1;
                    }

                    if (chkTmdb || ForceTmdb)
                    {
                        GetMovieTmdbData(data, data.FullFilename, chkTmdb, ForceTmdb);
                    }

                    var recs = UpdateItem(data);

                    lblRec.Text = data.Id.ToString() + "ok";
                    Application.DoEvents();

                    UpdateDbNestedNodeTitle(data, chkTmdb);
                }
            }
        }

        private void UpdateNodeDataTitle(bool chkTmdb, bool ForceTmdb = false)
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
                    GetMovieTmdbData(data, data.FullFilename, chkTmdb, ForceTmdb);
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
                        UpdateNestedNode(node, chkTmdb, ForceTmdb);
                    }
                }
                else // tenta pela base de dados
                {
                    UpdateDbNestedNodeTitle(data, chkTmdb);
                }
            }
        }

        private int IdentifyUnnecessaryFolders(CatalogItem data, SQLite.SQLiteConnection con)
        {
            int unnecessaryFolder = 0;
            if (data.TmdbPosterFoldersQtde>1)
            {
                unnecessaryFolder = 1;
            }
            int tempUnnecessaryFolder = 0;
            foreach (var dta in data.Items)
            {
                tempUnnecessaryFolder = IdentifyUnnecessaryFolders(dta, con);
                if (tempUnnecessaryFolder == 1)
                {
                    unnecessaryFolder = 1;
                }
            }
            //
            // salva o resultado do processamento recursivo
            //
            data.ToRemoveFromCatalog = unnecessaryFolder;
           UpdateItem(data, con);
            return unnecessaryFolder;
        }

        private async Task ReprocessNodeDataTitle(CatalogItem data, bool chkTmdb = false, SQLite.SQLiteConnection con = null, int level = 0, bool ForceTmdb = false)
        {
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
            }
            if (data != null)
            {
                lblRec.Text = level.ToString() + " " + data.Title;
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

                // Count subdirectories at tmdb folder
                data.TmdbPosterFoldersQtde = 0;
                string filepathMovie = Path.Combine(CatalogFolder, "TMDB", "Movies", data.Title);
                try
                {
                    string[] subdirectoryEntries = Directory.GetDirectories(filepathMovie);
                    data.TmdbPosterFoldersQtde = subdirectoryEntries.Length;
                }
                catch { }

                if (chkTmdb || ForceTmdb)
                {
                    GetMovieTmdbData(data, data.FullFilename, chkTmdb, ForceTmdb);
                }

                var recs = UpdateItem(data, con);

                data.Items = GetItems(data.Id, con);
                if (data.Items != null && data.Items.Count > 0)
                {
                    foreach (var dta in data.Items)
                    {
                        await ReprocessNodeDataTitle(dta, chkTmdb, con, level + 1);
                    }
                }

                lblRec.Text = level.ToString() + " " + data.Title + " OK!";
                Application.DoEvents();
                if (level==0)
                {
                    
                }
            }
            if (level == 0)
            {
                if (data != null)
                {
                    if (level == 0)
                    {
                        data.ToRemoveFromCatalog = IdentifyUnnecessaryFolders(data, con);
                    }
                }
            }
            if (level == 0)
            {
                con.Close();
            }
            // process data strucuture

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
                    var rs = MessageBox.Show("Needs to create Database, Proceed?", "Movie Catalog Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (rs.Equals(DialogResult.OK))
                    {
                        System.Data.SQLite.SQLiteConnection.CreateFile(SqLiteFile);

                        var con = new SQLite.SQLiteConnection(SqLiteFile);
                        con.CreateTable<CatalogItem>();
                        con.CreateIndex("CatalogItem", new string[] { "VolumeId", "FatherId" });
                        con.CreateIndex("CatalogItem", new string[] { "FatherId", "VolumeId" });

                        con.CreateTable<ManualIntervention>();
                        con.CreateIndex("CatalogItem", new string[] { "Field", "OriginalValue" });

                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }

        }

        public ManualIntervention GetManualIntervention(ManualIntervention dta, SQLite.SQLiteConnection con = null)
        {

            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }

            ManualIntervention data = new ManualIntervention();
            try
            {
                data = con.Query<ManualIntervention>($"select * from ManualIntervention Where OriginalValue = '{dta.OriginalValue.Replace("'", "`") }' and Field = {((int)dta.Field).ToString()}").FirstOrDefault();
            }
            catch(Exception ex)
            {
                int x = 1;
            }

            if (close) {
                con.Close();
            }

            return data;

        }

        public int UpdateManualInterventions (ManualIntervention dta, SQLite.SQLiteConnection con = null)
        {
            int result = 0;

            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }

            // find a data for field
            var record = GetManualIntervention(dta, con);
            if (record == null)
            {
                InsertManualInterventionItem(dta, con);
            }
            else
            {
                dta.Id = record.Id;
                result = con.Update(dta);
            }

            if (close)
            {
                con.Close();
            }

            return result;
        }

        private int UpdateItem(CatalogItem Data, SQLite.SQLiteConnection con = null)
        {
            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }

            bool mustInsertIntervention = true;
            ManualIntervention dta = new ManualIntervention();

            // testar se já existe algum tipo de intervenção manual para o item
            if (Data.ManualIntervention != 1) {
                dta.Field = ManualFieldsIntervention.Title;
                dta.OriginalValue = Data.FullFilename.Split('\\').Last().Split('.').First().Replace("'","`") ;
                var Intervention = GetManualIntervention(dta, con);
                if (Intervention != null)
                {
                    Data.Title = Intervention.Value;
                    Data.ManualIntervention = 1;
                    mustInsertIntervention = false;
                }
            }

            int id=con.Update(Data);

            // atualiza os filhos para manterem o mesmo dado
            if (Data.ManualIntervention == 1)
            {
                UpdateAllChildItemsTitle(Data);
            }

            if (Data.ManualIntervention == 1 && mustInsertIntervention)
            {
                // this must be updated in future for all other logged fields
                dta.Field = ManualFieldsIntervention.Title;
                dta.OriginalValue = Data.FullFilename.Split('\\').Last().Split('.').First().Replace("'", "`");
                dta.Value = Data.Title;
                UpdateManualInterventions(dta, con);
            }

            if (close)
            {
                con.Close();
            }
            return id;
        }

        private void UpdateAllChildItemsTitle(CatalogItem Data)
        {
            if (Data.ManualIntervention ==  1)
            {
                UpdateDbNestedNodeTitle(Data, false);
            }
        }

        private int InsertItem(CatalogItem Data, SQLite.SQLiteConnection con = null)
        {
            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }

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
                if (close)
                {
                    con.Close();
                }
                return id;
            }
            else
            {
                if (close)
                {
                    con.Close();
                }
                return idx;
            }
        }

        private int InsertManualInterventionItem(ManualIntervention Data, SQLite.SQLiteConnection con = null)
        {
            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }

            int id = 0;
            try
            {
                id = con.Insert(Data);
                id = (int)con.ExecuteScalar<int>(@"select last_insert_rowid()");
                if (close)
                {
                    con.Close();
                }
            }
            catch
            {
                id = 0;
            }

            if (close)
            {
                con.Close();
            }
            return id;

        }

        private CatalogItem GetItem(int id, SQLite.SQLiteConnection con = null)
        {
            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }
         
            var data = con.Query<CatalogItem>("select * from CatalogItem Where id = ?", new object[] { id } ).FirstOrDefault();

            if (close)
            {
                con.Close();
            }

            return data;

        }

        private int DeleteId(int Id, SQLite.SQLiteConnection con = null)
        {
            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }
         
            int id = con.Delete<CatalogItem>(Id);
            if (close)
            {
                con.Close();
            }

            return id;

        }

        public void SaveSons(CatalogItem Sons, int Father, SQLite.SQLiteConnection con = null)
        {
            foreach(var Movie in Sons.Items)
            {
                Movie.FatherId = Father;
                int FatherId = InsertItem(Movie, con);
                if (Movie.Items != null && Movie.Items.Count > 0)
                {
                    if (Movie.FullFilename==null)
                    {
                        var x = Movie;
                    }
                    SaveSons(Movie, FatherId, con);
                }
            }
            return;
        }

        private void SaveCatalog(CatalogItem Movies, int FatherId, SQLite.SQLiteConnection con = null)
        {
            // Apagar todos os registros do Volume/MArcar como Em Ajuste...
            // Ler a estrutura do catalogo.. item a item.. sub item a sub item...
            // SAlvar o item e guardar o id como pai do prx nivel

            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }

            con.DeleteAllForVolumeId("CatalogItem", "VolumeId", curVolumeId);


            Movies.FatherId = FatherId;
            int Father = InsertItem(Movies, con);
            if (Movies.Items != null && Movies.Items.Count > 0)
            {
                SaveSons(Movies, Father, con);
            }

            if (close)
            {
                con.Close();
            }

        }

        private List<CatalogItem> GetVolumes(SQLite.SQLiteConnection con = null)
        {
            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }

            var data = con.Query<CatalogItem>("select * from CatalogItem Where Type = ?", new object[] { (int)ItemType.Volume });

            if (close)
            {
                con.Close();
            }

            return data;
        }

        private List<CatalogItem> GetItems(int? FatherId, SQLite.SQLiteConnection con = null)
        {
            bool close = false;
            if (con == null)
            {
                con = new SQLite.SQLiteConnection(SqLiteFile);
                close = true;
            }
          
            var data = con.Query<CatalogItem>("select * from CatalogItem Where FatherId = ?", FatherId);
            if (close)
            {
                con.Close();
            }
            return data;
        }

        private void LoadCatalogs(TreeNode node=null, int? Father = null, bool OnlyNoTmdb = false, bool OnlyNeedRemove = false)
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
                data = GetItems(obj.Id); // otimizar oportunamente
            }

            if (Father == null || Father == 0)
            {
                tvCatalogs.Nodes.Clear();
                //data = GetVolumes();
                data = GetItems(0);
            }
            foreach (CatalogItem itm in data)
            {
                bool show = true;
                if (OnlyNeedRemove && itm.ToRemoveFromCatalog!=1)
                {
                    show = false;
                }

                if (OnlyNoTmdb && (itm.JsonMoviesData != null && itm.JsonFileInfo!="") && itm.Type==ItemType.File)
                {
                    show = false;
                }

                if (show)
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
        }

        #endregion

        private void LoadRoot()
        {
            tvCatalogs.Nodes.Clear();
            LoadCatalogs(null, 0, chkOnlyWTmdb.Checked, chkNeedRemove.Checked);
        }

        private void LoadNodes(TreeNode node)
        {
            CatalogItem itm = (CatalogItem)node.Tag;
            if (itm != null)
            {
                LoadCatalogs(node, itm.Id, chkOnlyWTmdb.Checked, chkNeedRemove.Checked);
                node.Expand();
            }
        }

        private void SetCurMovie()
        {
            CurCatItem = (CatalogItem)tvFoldersCat.SelectedNode.Tag;
            pptGrdDetailData.SelectedObject = CurCatItem;
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
            newItem.parent = BaseItem;

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
                        filepath = Path.Combine(CatalogFolder, "TMDB", "Catalog", catItem.Title + " (" + year.ToString() + ").movie");
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

                filepath = Path.Combine(CatalogFolder, "TMDB", "Catalog", catItem.Title + " (TMDB).movie");
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
                filepath = Path.Combine(CatalogFolder, "TMDB", "Catalog", XcatItem.Title + " (" + XcatItem.Year.ToString() + ").movie");
            }
            else
            {
                filepath = Path.Combine(CatalogFolder, "TMDB", "Catalog", XcatItem.Title + " (TMDB).movie");
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
                        Task<Movies> task = Task.Run<Movies>(async () => await GetMovieData(CatalogFolder, catItem.Title, catItem.Year, catItem.Season, catItem.Episode, cToken, TMDB_ApiKey, GetTmdbPeople));
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
                    Task<Movies> task = Task.Run<Movies>(async () => await GetMovieData(CatalogFolder, catItem.Title, 0, 0, 0, cToken, TMDB_ApiKey, GetTmdbPeople));
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

        private void GetMovieTmdbData(CatalogItem itm, string path, bool GetMovieTmdbInfo, bool ForceTmdb = false)
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
            if (ForceTmdb)
            {
                lblRec.Text = itm.Id.ToString() + " tmdb by service";
                Application.DoEvents();
                GetMovieCatalog(years, itm, ForceTmdb);
            }
            else
            {
                if ((itm.JsonMoviesData == null || itm.JsonMoviesData == "") && (GetMovieTmdbInfo))
                {
                    lblRec.Text = itm.Id.ToString() + " tmdb by service";
                    Application.DoEvents();
                    GetMovieCatalog(years, itm, GetMovieTmdbInfo);
                }
            }
            lblRec.Text = itm.Id.ToString() + " tmdb done";
            Application.DoEvents();

        }

        private bool isSonOfSerie(CatalogItem itm)
        {
            if (itm.parent == null) return false;
            if (itm.parent.Serie) return true;
            return isSonOfSerie(itm.parent);
        }

        private bool isFolderOfSerie(string path)
        {
            bool isSerie = false;
            string[] subdirectoryEntries = Directory.GetDirectories(path);
            foreach (string folder in subdirectoryEntries)
            {
                if (Array.IndexOf(ignoreFolder, LastString(folder.ToUpper())) == -1)
                {
                    isSerie = int.TryParse(folder.Split('\\').LastOrDefault(), out int val);
                    if (isSerie) return true;
                }
            }
            return isSerie;
        }

        private CatalogItem SetCatalogItem(ItemType Type, string path, string Name, CatalogItem bItem, DriveInfo drive = null, bool CheckCrc = false, bool GetMovieInfo = false, bool ForceTmdb = false)
        {
            CatalogItem data = new CatalogItem();

            if (Type == ItemType.Volume)
            {
                FillCatalogItem(bItem, ItemType.Volume, "NONAME");

                if (drive.IsReady == true)
                {
                    curVolumeId = HD.getSerial(drive.Name);
                    curVolumeName = drive.VolumeLabel;

                    bItem.DiveInfo = drive;
                    bItem.Size = drive.TotalSize;
                    bItem.UnsudedSpace = drive.AvailableFreeSpace;
                    bItem.VolumeId = curVolumeId;
                    bItem.VolumeName = curVolumeName;
                    bItem.FullFilename = curVolumeName;
                    bItem.Name = curVolumeName;
                    bItem.Title = curVolumeName;

                    bItem.SoundexTitle = Soundex.Generate(data.Title, 10);

                    bItem.Serie = false;

                }
            }

            if (Type == ItemType.Folder)
            {
                data = AddCatalogItem(bItem, ItemType.Folder, Name);

                string cpath = Path.Combine(path, Name);
                FileInfo fi = new FileInfo(cpath);

                var settings = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };

                data.JsonFileInfo = Newtonsoft.Json.JsonConvert.SerializeObject(fi, settings);
                data.FileInfo = fi;
                data.CreationDate = fi.CreationTime;
                data.ModifiedDate = fi.LastWriteTime;
                data.VolumeId = curVolumeId;
                data.VolumeName = curVolumeName;
                data.FullFilename = cpath;              
                data.Year = Convert.ToInt16(cpath.GetYearsFromString().FirstOrDefault());

                data.Title = GetTitle(Name, data);
                data.SoundexTitle = Soundex.Generate(data.Title,10);

                data.Serie = isFolderOfSerie(cpath);
                if (!data.Serie)
                {
                    data.Serie = isSonOfSerie(bItem);
                }

                try
                {
                    data.Size = fi.Length;
                }
                catch { }

            }

            if (Type == ItemType.File)
            {
                data = AddCatalogItem(bItem, ItemType.File, Name);

                FileInfo fi = new FileInfo(path);
                var settings = new JsonSerializerSettings() { ContractResolver = new MyContractResolver() };

                data.JsonFileInfo = Newtonsoft.Json.JsonConvert.SerializeObject(fi, settings);
                data.FileInfo = fi;
                data.Size = fi.Length;
                data.FullFilename = path;
                data.CreationDate = fi.CreationTime;
                data.ModifiedDate = fi.LastWriteTime;
                data.VolumeId = curVolumeId;
                data.VolumeName = curVolumeName;

                data.Season = getSeasonEpisode("s", path);
                data.Episode = getSeasonEpisode("e", path);
                data.Year = Convert.ToInt16(path.GetYearsFromString().FirstOrDefault());

                data.Serie = isSonOfSerie(data);

                data.Title = GetTitle(path, data);
                data.SoundexTitle = Soundex.Generate(data.Title, 10);

                if (CheckCrc)
                {
                    if (Array.IndexOf(allowedVideos, Path.GetExtension(path)) >= 0)
                    {
                        catMonitor.Text = "CRC CALC for: " + path;
                        Application.DoEvents();
                        try
                        {
                            Crc32 result = CRC(path);
                            data.JsonCRC = Newtonsoft.Json.JsonConvert.SerializeObject(CRC(path), settings);
                            WriteCrcJsonToFile(data);
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
                    data.JsonCRC = ReadCrcJsonFile(data);
                }

                // apenas para videos
                if (Array.IndexOf(allowedVideos, Path.GetExtension(path)) >= 0)
                {
                    GetMovieTmdbData(data, path, GetMovieInfo, ForceTmdb);
                }
            }

            return data;
        }

        private async Task<TreeNode> doCatalog(TreeView Tree,  String path, TreeNode node, CatalogItem baseItem, bool CheckCrc, bool GetMovieTmdbInfo)
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
                    if (Array.IndexOf(ignoreFolder, LastString(subdirectory.ToUpper())) == -1) {
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

                        await doCatalog(Tree, subdirectory, nodec, catItem, CheckCrc, GetMovieTmdbInfo);
                    }
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
            string sToSearch = "";

            ManualIntervention dta = new ManualIntervention();
            dta.OriginalValue = Item.FullFilename.Split(':').Last().Split('\\').Last().Split('.').First().Replace("'", "`");
            dta.Field = ManualFieldsIntervention.Title;
            dta = GetManualIntervention(dta);
            if (dta != null)
            {
                sToSearch = dta.Value;
                Item.ManualIntervention = 1;
            }
            else
            {
                if (Item != null)
                {
                    if (Item.ManualIntervention != 1)
                    {
                        sToSearch = ClearStringData(RemoveExtension(Path.GetFileName(Data)), true, true);

                        if (Item.Season > 0)
                        {
                            sToSearch = RemoveSeasonEpisodeFromName("s", sToSearch, Item.Season);
                        }
                        if (Item.Episode > 0)
                        {
                            sToSearch = RemoveSeasonEpisodeFromName("e", sToSearch, Item.Episode);
                        }
                    }

                    if (Item.parent != null
                        && Item.FatherId != 0
                        && Item.FatherId != null
                        && Item.parent.ManualIntervention == 1)
                    {
                        Item.Title = Item.parent.Title;
                        Item.ManualIntervention = 1;
                        sToSearch = Item.Title;
                    }
                }
            }
            if (sToSearch == null || sToSearch == "")
            {
                if (Item.parent != null)
                {
                    try
                    {
                        sToSearch = Item.parent.Title;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            if (sToSearch == null || sToSearch == "")
            {
                sToSearch = Item.Name;
            }
            if (sToSearch == null || sToSearch == "")
            {
                sToSearch = "NONAME";
            }
            return sToSearch;
        }

        private async Task MountCatalog()
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
                    await doCatalog(tvFoldersCat, path, null, ReadingData, chkCRC.Checked, chkTmdb.Checked);
                }
            }
            else
            {
                foreach (var itm in listFolders.Items)
                {
                    await doCatalog(tvFoldersCat, itm.ToString(), null, ReadingData, chkCRC.Checked, chkTmdb.Checked);
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

        private void EditTitle(TreeView tv, PropertyGridEx.PropertyGridEx pptGrid )
        {
            try
            {
                
                CatalogItem data = (CatalogItem)tv.SelectedNode.Tag;
                string oldTitle = data.Title;
                var newTitle = InputBox.ShowDialog("", "Movie Title", data.Title);
                if (newTitle != null && newTitle != "")
                {
                    data.Title = newTitle;
                    data.ManualIntervention = 1;
                    data.SoundexTitle = Soundex.Generate(data.Title, 10);
                    UpdateItem(data);
                    // rename tmdb folder
                    string OldFilepathMovie = Path.Combine(CatalogFolder, "TMDB", "Movies", oldTitle);
                    string NewFilepathMovie = Path.Combine(CatalogFolder, "TMDB", "Movies", newTitle);

                    try
                    {
                        if (File.Exists(OldFilepathMovie) && !File.Exists(NewFilepathMovie))
                        {
                            Directory.Move(OldFilepathMovie, NewFilepathMovie);
                        }
                    }
                    catch {
                        MessageBox.Show("Error trybg Rename TMDB Catalog Folder!", "Catalog Folder REname", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // delete all subnodes
                    try { 
                        tv.SelectedNode.Nodes.Clear();
                    }
                    catch { }

                    // update current item
                    tv.SelectedNode.Tag = data;
                    tv.SelectedNode.Text = newTitle;
                    pptGrid.SelectedObject = tv.SelectedNode.Tag;
                }
            }
            catch (Exception ex)
            {

            }
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

        static async Task<Movies> GetMovieData(String CatalogFolder, String Name, int year, int Season, int Episode,  CancellationToken cancellationToken, String TMDB_ApiKey, bool GetTmdbPeople)
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
                    if (GetTmdbPeople)
                    {
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
                if (MovieName != null && MovieName != "")
                {
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
            }
            catch { }
        }

        private void LoadImages()
        {
            LoadImages((CatalogItem)pptGrdDetailData.SelectedObject, "Poster.Jpg", pictPoster, imageListPosters, dataGridViewImage);
            LoadImages((CatalogItem)pptGrdDetailData.SelectedObject, "Backdrop.Jpg", pictBackdrop, imageListBackdrop);
        }

        private void LoadImagesCatalogs()
        {
            LoadImages((CatalogItem)pptGridCatalogs.SelectedObject, "Poster.Jpg", pictPosterCatalogs, imageListPosters, dtGridCatalogs);
            LoadImages((CatalogItem)pptGridCatalogs.SelectedObject, "Backdrop.Jpg", pictBackdropCatalogs, imageListBackdrop);
        }

        private void DeleteFolderExceptThis(TreeView tList, int Index)
        {

            int imgIndex = imageListPosters.Count - Index - 1;
            CatalogItem catItm = (CatalogItem)tList.SelectedNode.Tag;
            if (catItm == null)
            {
                return;
            }
            String Name = "";
            try
            {
                Name = imageListPosters[imgIndex].Key;
            }
            catch
            {
                return;
            }

            if (Name == null || Name == "")
            {
                return;
            }

            try
            {
                String MovieName = catItm.Title;
                String filepathMovie = Path.Combine(CatalogFolder, "TMDB", "Movies", MovieName);
                String[] subdirectoryEntries = Directory.GetDirectories(filepathMovie);
                if (subdirectoryEntries.Length > 0)
                {

                    foreach (string folder in subdirectoryEntries)
                    {
                        String FolderName = folder.Split('\\').LastOrDefault();
                        try
                        {
                            if (FolderName != Name)
                            {
                                String extraFilepathMovie = Path.Combine(CatalogFolder, "TMDB", "Extras", FolderName);
                                // move to A Special Folder
                                try
                                {
                                    Directory.Move(folder, extraFilepathMovie);
                                    catItm.ToRemoveFromCatalog = 0;
                                    catItm.TmdbPosterFoldersQtde = 1;
                                    UpdateItem(catItm);
                                    try
                                    {
                                        tList.SelectedNode.Tag = catItm;
                                    }
                                    catch { }
                                }
                                catch (IOException exp)
                                { }

                            }
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            catch { }

        }

        #endregion

        private void MediaPlayerCataloging_DoubleClickEvent(object sender, AxWMPLib._WMPOCXEvents_DoubleClickEvent e)
        {
            PlayVideo(tvFoldersCat.SelectedNode, MediaPlayerCataloging);
        }

        private void PlayVideo(TreeNode node, AxWMPLib.AxWindowsMediaPlayer Mp)
        {
            CatalogItem video = (CatalogItem)node.Tag;
            if (video != null)
            {
                Mp.URL = "";
                string filename = video.FullFilename;
                Mp.URL = filename;
                //MediaPlayerCataloging.

            }
        }

        private void dataGridViewImage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SetMainPoster(e.RowIndex);
            DialogResult result = MessageBox.Show("Maintain only this image, Proceed?", "Image Selection", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result.Equals(DialogResult.OK))
            {
                DeleteFolderExceptThis(tvFoldersCat, e.RowIndex);
            }
        }

        private void MediaPlayerCatalog_DoubleClickEvent(object sender, AxWMPLib._WMPOCXEvents_DoubleClickEvent e)
        {
            PlayVideo(tvCatalogs.SelectedNode, MediaPlayerCatalog);
        }
 
    }

}
