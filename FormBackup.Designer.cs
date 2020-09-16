namespace KCBackup
{
    partial class FormBackup
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
            this.grpUserFiles = new System.Windows.Forms.GroupBox();
            this.chkUserFiles = new System.Windows.Forms.CheckBox();
            this.chkVideos = new System.Windows.Forms.CheckBox();
            this.chkPictures = new System.Windows.Forms.CheckBox();
            this.chkMusic = new System.Windows.Forms.CheckBox();
            this.chkFavorites = new System.Windows.Forms.CheckBox();
            this.chkDownloads = new System.Windows.Forms.CheckBox();
            this.chkDocuments = new System.Windows.Forms.CheckBox();
            this.chkDesktop = new System.Windows.Forms.CheckBox();
            this.grpDataFromPrograms = new System.Windows.Forms.GroupBox();
            this.chkThunderbird = new System.Windows.Forms.CheckBox();
            this.chkChrome = new System.Windows.Forms.CheckBox();
            this.chkFirefox = new System.Windows.Forms.CheckBox();
            this.grpCustomFolders = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lstCustom = new System.Windows.Forms.ListBox();
            this.grpCommonFiles = new System.Windows.Forms.GroupBox();
            this.chkCommonUserFiles = new System.Windows.Forms.CheckBox();
            this.chkCommonVideos = new System.Windows.Forms.CheckBox();
            this.chkCommonPictures = new System.Windows.Forms.CheckBox();
            this.chkCommonMusic = new System.Windows.Forms.CheckBox();
            this.chkCommonDownloads = new System.Windows.Forms.CheckBox();
            this.chkCommonDocuments = new System.Windows.Forms.CheckBox();
            this.chkCommonDesktop = new System.Windows.Forms.CheckBox();
            this.btnLoadRestore = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnStartProcess = new System.Windows.Forms.Button();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblCurrentMode = new System.Windows.Forms.Label();
            this.btnModeCancel = new System.Windows.Forms.Button();
            this.blbLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnRescan = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblRescan = new System.Windows.Forms.Label();
            this.lblBackupNotice = new System.Windows.Forms.Label();
            this.chkRestoreOnly = new System.Windows.Forms.CheckBox();
            this.grpUserFiles.SuspendLayout();
            this.grpDataFromPrograms.SuspendLayout();
            this.grpCustomFolders.SuspendLayout();
            this.grpCommonFiles.SuspendLayout();
            this.grpMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUserFiles
            // 
            this.grpUserFiles.Controls.Add(this.chkUserFiles);
            this.grpUserFiles.Controls.Add(this.chkVideos);
            this.grpUserFiles.Controls.Add(this.chkPictures);
            this.grpUserFiles.Controls.Add(this.chkMusic);
            this.grpUserFiles.Controls.Add(this.chkFavorites);
            this.grpUserFiles.Controls.Add(this.chkDownloads);
            this.grpUserFiles.Controls.Add(this.chkDocuments);
            this.grpUserFiles.Controls.Add(this.chkDesktop);
            this.grpUserFiles.Location = new System.Drawing.Point(18, 18);
            this.grpUserFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpUserFiles.Name = "grpUserFiles";
            this.grpUserFiles.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpUserFiles.Size = new System.Drawing.Size(300, 323);
            this.grpUserFiles.TabIndex = 0;
            this.grpUserFiles.TabStop = false;
            this.grpUserFiles.Text = "User files";
            // 
            // chkUserFiles
            // 
            this.chkUserFiles.AutoSize = true;
            this.chkUserFiles.Checked = true;
            this.chkUserFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserFiles.Location = new System.Drawing.Point(9, 29);
            this.chkUserFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkUserFiles.Name = "chkUserFiles";
            this.chkUserFiles.Size = new System.Drawing.Size(101, 24);
            this.chkUserFiles.TabIndex = 0;
            this.chkUserFiles.Text = "User files";
            this.toolTip1.SetToolTip(this.chkUserFiles, "These are for user-specific files");
            this.chkUserFiles.UseVisualStyleBackColor = true;
            this.chkUserFiles.Click += new System.EventHandler(this.chkAllNone_Click);
            // 
            // chkVideos
            // 
            this.chkVideos.AutoSize = true;
            this.chkVideos.Checked = true;
            this.chkVideos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVideos.Location = new System.Drawing.Point(39, 277);
            this.chkVideos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkVideos.Name = "chkVideos";
            this.chkVideos.Size = new System.Drawing.Size(84, 24);
            this.chkVideos.TabIndex = 7;
            this.chkVideos.Text = "Videos";
            this.toolTip1.SetToolTip(this.chkVideos, "These are for user-specific files");
            this.chkVideos.UseVisualStyleBackColor = true;
            // 
            // chkPictures
            // 
            this.chkPictures.AutoSize = true;
            this.chkPictures.Checked = true;
            this.chkPictures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPictures.Location = new System.Drawing.Point(39, 242);
            this.chkPictures.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkPictures.Name = "chkPictures";
            this.chkPictures.Size = new System.Drawing.Size(92, 24);
            this.chkPictures.TabIndex = 6;
            this.chkPictures.Text = "Pictures";
            this.toolTip1.SetToolTip(this.chkPictures, "These are for user-specific files");
            this.chkPictures.UseVisualStyleBackColor = true;
            // 
            // chkMusic
            // 
            this.chkMusic.AutoSize = true;
            this.chkMusic.Checked = true;
            this.chkMusic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMusic.Location = new System.Drawing.Point(39, 206);
            this.chkMusic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkMusic.Name = "chkMusic";
            this.chkMusic.Size = new System.Drawing.Size(76, 24);
            this.chkMusic.TabIndex = 5;
            this.chkMusic.Text = "Music";
            this.toolTip1.SetToolTip(this.chkMusic, "These are for user-specific files");
            this.chkMusic.UseVisualStyleBackColor = true;
            // 
            // chkFavorites
            // 
            this.chkFavorites.AutoSize = true;
            this.chkFavorites.Location = new System.Drawing.Point(39, 171);
            this.chkFavorites.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkFavorites.Name = "chkFavorites";
            this.chkFavorites.Size = new System.Drawing.Size(232, 24);
            this.chkFavorites.TabIndex = 4;
            this.chkFavorites.Text = "Favorites (Internet Explorer)";
            this.toolTip1.SetToolTip(this.chkFavorites, "These are for user-specific files");
            this.chkFavorites.UseVisualStyleBackColor = true;
            // 
            // chkDownloads
            // 
            this.chkDownloads.AutoSize = true;
            this.chkDownloads.Checked = true;
            this.chkDownloads.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDownloads.Location = new System.Drawing.Point(39, 135);
            this.chkDownloads.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDownloads.Name = "chkDownloads";
            this.chkDownloads.Size = new System.Drawing.Size(114, 24);
            this.chkDownloads.TabIndex = 3;
            this.chkDownloads.Text = "Downloads";
            this.toolTip1.SetToolTip(this.chkDownloads, "These are for user-specific files");
            this.chkDownloads.UseVisualStyleBackColor = true;
            // 
            // chkDocuments
            // 
            this.chkDocuments.AutoSize = true;
            this.chkDocuments.Checked = true;
            this.chkDocuments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDocuments.Location = new System.Drawing.Point(39, 100);
            this.chkDocuments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDocuments.Name = "chkDocuments";
            this.chkDocuments.Size = new System.Drawing.Size(117, 24);
            this.chkDocuments.TabIndex = 2;
            this.chkDocuments.Text = "Documents";
            this.toolTip1.SetToolTip(this.chkDocuments, "These are for user-specific files");
            this.chkDocuments.UseVisualStyleBackColor = true;
            // 
            // chkDesktop
            // 
            this.chkDesktop.AutoSize = true;
            this.chkDesktop.Checked = true;
            this.chkDesktop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDesktop.Location = new System.Drawing.Point(39, 65);
            this.chkDesktop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDesktop.Name = "chkDesktop";
            this.chkDesktop.Size = new System.Drawing.Size(95, 24);
            this.chkDesktop.TabIndex = 1;
            this.chkDesktop.Text = "Desktop";
            this.toolTip1.SetToolTip(this.chkDesktop, "These are for user-specific files");
            this.chkDesktop.UseVisualStyleBackColor = true;
            // 
            // grpDataFromPrograms
            // 
            this.grpDataFromPrograms.Controls.Add(this.chkThunderbird);
            this.grpDataFromPrograms.Controls.Add(this.chkChrome);
            this.grpDataFromPrograms.Controls.Add(this.chkFirefox);
            this.grpDataFromPrograms.Location = new System.Drawing.Point(327, 351);
            this.grpDataFromPrograms.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDataFromPrograms.Name = "grpDataFromPrograms";
            this.grpDataFromPrograms.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDataFromPrograms.Size = new System.Drawing.Size(300, 145);
            this.grpDataFromPrograms.TabIndex = 2;
            this.grpDataFromPrograms.TabStop = false;
            this.grpDataFromPrograms.Text = "Data from programs";
            // 
            // chkThunderbird
            // 
            this.chkThunderbird.AutoSize = true;
            this.chkThunderbird.Checked = true;
            this.chkThunderbird.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThunderbird.Location = new System.Drawing.Point(9, 100);
            this.chkThunderbird.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkThunderbird.Name = "chkThunderbird";
            this.chkThunderbird.Size = new System.Drawing.Size(172, 24);
            this.chkThunderbird.TabIndex = 17;
            this.chkThunderbird.Text = "Mozilla Thunderbird";
            this.toolTip1.SetToolTip(this.chkThunderbird, "Copy Thunderbird profiles");
            this.chkThunderbird.UseVisualStyleBackColor = true;
            // 
            // chkChrome
            // 
            this.chkChrome.AutoSize = true;
            this.chkChrome.Checked = true;
            this.chkChrome.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChrome.Location = new System.Drawing.Point(9, 65);
            this.chkChrome.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkChrome.Name = "chkChrome";
            this.chkChrome.Size = new System.Drawing.Size(147, 24);
            this.chkChrome.TabIndex = 16;
            this.chkChrome.Text = "Google Chrome";
            this.toolTip1.SetToolTip(this.chkChrome, "Copy Chrome profiles");
            this.chkChrome.UseVisualStyleBackColor = true;
            // 
            // chkFirefox
            // 
            this.chkFirefox.AutoSize = true;
            this.chkFirefox.Checked = true;
            this.chkFirefox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFirefox.Location = new System.Drawing.Point(9, 29);
            this.chkFirefox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkFirefox.Name = "chkFirefox";
            this.chkFirefox.Size = new System.Drawing.Size(135, 24);
            this.chkFirefox.TabIndex = 15;
            this.chkFirefox.Text = "Mozilla Firefox";
            this.toolTip1.SetToolTip(this.chkFirefox, "Copy Firefox profiles");
            this.chkFirefox.UseVisualStyleBackColor = true;
            // 
            // grpCustomFolders
            // 
            this.grpCustomFolders.Controls.Add(this.btnRefresh);
            this.grpCustomFolders.Controls.Add(this.btnRemoveAll);
            this.grpCustomFolders.Controls.Add(this.btnRemove);
            this.grpCustomFolders.Controls.Add(this.btnBrowse);
            this.grpCustomFolders.Controls.Add(this.lstCustom);
            this.grpCustomFolders.Location = new System.Drawing.Point(327, 18);
            this.grpCustomFolders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCustomFolders.Name = "grpCustomFolders";
            this.grpCustomFolders.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCustomFolders.Size = new System.Drawing.Size(681, 323);
            this.grpCustomFolders.TabIndex = 3;
            this.grpCustomFolders.TabStop = false;
            this.grpCustomFolders.Text = "Custom folders";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(443, 278);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 35);
            this.btnRefresh.TabIndex = 21;
            this.btnRefresh.Text = "Refresh List";
            this.toolTip1.SetToolTip(this.btnRefresh, "Resets list to all extra user folders on backup, or all extra backed up folders o" +
        "n restore");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Enabled = false;
            this.btnRemoveAll.Location = new System.Drawing.Point(128, 278);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(112, 35);
            this.btnRemoveAll.TabIndex = 20;
            this.btnRemoveAll.Text = "Remove All";
            this.toolTip1.SetToolTip(this.btnRemoveAll, "Removes all custom folders");
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(9, 278);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(112, 35);
            this.btnRemove.TabIndex = 19;
            this.btnRemove.Text = "Remove";
            this.toolTip1.SetToolTip(this.btnRemove, "Remove location from backup or restore process");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(560, 278);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(112, 35);
            this.btnBrowse.TabIndex = 18;
            this.btnBrowse.Text = "Browse";
            this.toolTip1.SetToolTip(this.btnBrowse, "Add a new folder to the backup process only");
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lstCustom
            // 
            this.lstCustom.FormattingEnabled = true;
            this.lstCustom.ItemHeight = 20;
            this.lstCustom.Location = new System.Drawing.Point(9, 29);
            this.lstCustom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstCustom.Name = "lstCustom";
            this.lstCustom.Size = new System.Drawing.Size(661, 244);
            this.lstCustom.TabIndex = 0;
            this.lstCustom.TabStop = false;
            this.toolTip1.SetToolTip(this.lstCustom, "List of custom folders to be backed up or restored");
            this.lstCustom.SelectedIndexChanged += new System.EventHandler(this.lstCustom_SelectedIndexChanged);
            // 
            // grpCommonFiles
            // 
            this.grpCommonFiles.Controls.Add(this.chkCommonUserFiles);
            this.grpCommonFiles.Controls.Add(this.chkCommonVideos);
            this.grpCommonFiles.Controls.Add(this.chkCommonPictures);
            this.grpCommonFiles.Controls.Add(this.chkCommonMusic);
            this.grpCommonFiles.Controls.Add(this.chkCommonDownloads);
            this.grpCommonFiles.Controls.Add(this.chkCommonDocuments);
            this.grpCommonFiles.Controls.Add(this.chkCommonDesktop);
            this.grpCommonFiles.Location = new System.Drawing.Point(18, 351);
            this.grpCommonFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCommonFiles.Name = "grpCommonFiles";
            this.grpCommonFiles.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCommonFiles.Size = new System.Drawing.Size(300, 291);
            this.grpCommonFiles.TabIndex = 1;
            this.grpCommonFiles.TabStop = false;
            this.grpCommonFiles.Text = "Common files";
            // 
            // chkCommonUserFiles
            // 
            this.chkCommonUserFiles.AutoSize = true;
            this.chkCommonUserFiles.Checked = true;
            this.chkCommonUserFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommonUserFiles.Location = new System.Drawing.Point(9, 29);
            this.chkCommonUserFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCommonUserFiles.Name = "chkCommonUserFiles";
            this.chkCommonUserFiles.Size = new System.Drawing.Size(131, 24);
            this.chkCommonUserFiles.TabIndex = 8;
            this.chkCommonUserFiles.Text = "Common files";
            this.toolTip1.SetToolTip(this.chkCommonUserFiles, "These are for the \"Public\" user for shared files");
            this.chkCommonUserFiles.UseVisualStyleBackColor = true;
            this.chkCommonUserFiles.Click += new System.EventHandler(this.chkAllNoneCommon_Click);
            // 
            // chkCommonVideos
            // 
            this.chkCommonVideos.AutoSize = true;
            this.chkCommonVideos.Checked = true;
            this.chkCommonVideos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommonVideos.Location = new System.Drawing.Point(39, 242);
            this.chkCommonVideos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCommonVideos.Name = "chkCommonVideos";
            this.chkCommonVideos.Size = new System.Drawing.Size(84, 24);
            this.chkCommonVideos.TabIndex = 14;
            this.chkCommonVideos.Text = "Videos";
            this.toolTip1.SetToolTip(this.chkCommonVideos, "These are for the \"Public\" user for shared files");
            this.chkCommonVideos.UseVisualStyleBackColor = true;
            // 
            // chkCommonPictures
            // 
            this.chkCommonPictures.AutoSize = true;
            this.chkCommonPictures.Checked = true;
            this.chkCommonPictures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommonPictures.Location = new System.Drawing.Point(39, 206);
            this.chkCommonPictures.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCommonPictures.Name = "chkCommonPictures";
            this.chkCommonPictures.Size = new System.Drawing.Size(92, 24);
            this.chkCommonPictures.TabIndex = 13;
            this.chkCommonPictures.Text = "Pictures";
            this.toolTip1.SetToolTip(this.chkCommonPictures, "These are for the \"Public\" user for shared files");
            this.chkCommonPictures.UseVisualStyleBackColor = true;
            // 
            // chkCommonMusic
            // 
            this.chkCommonMusic.AutoSize = true;
            this.chkCommonMusic.Checked = true;
            this.chkCommonMusic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommonMusic.Location = new System.Drawing.Point(39, 171);
            this.chkCommonMusic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCommonMusic.Name = "chkCommonMusic";
            this.chkCommonMusic.Size = new System.Drawing.Size(76, 24);
            this.chkCommonMusic.TabIndex = 12;
            this.chkCommonMusic.Text = "Music";
            this.toolTip1.SetToolTip(this.chkCommonMusic, "These are for the \"Public\" user for shared files");
            this.chkCommonMusic.UseVisualStyleBackColor = true;
            // 
            // chkCommonDownloads
            // 
            this.chkCommonDownloads.AutoSize = true;
            this.chkCommonDownloads.Checked = true;
            this.chkCommonDownloads.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommonDownloads.Location = new System.Drawing.Point(39, 135);
            this.chkCommonDownloads.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCommonDownloads.Name = "chkCommonDownloads";
            this.chkCommonDownloads.Size = new System.Drawing.Size(114, 24);
            this.chkCommonDownloads.TabIndex = 11;
            this.chkCommonDownloads.Text = "Downloads";
            this.toolTip1.SetToolTip(this.chkCommonDownloads, "These are for the \"Public\" user for shared files");
            this.chkCommonDownloads.UseVisualStyleBackColor = true;
            // 
            // chkCommonDocuments
            // 
            this.chkCommonDocuments.AutoSize = true;
            this.chkCommonDocuments.Checked = true;
            this.chkCommonDocuments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommonDocuments.Location = new System.Drawing.Point(39, 100);
            this.chkCommonDocuments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCommonDocuments.Name = "chkCommonDocuments";
            this.chkCommonDocuments.Size = new System.Drawing.Size(117, 24);
            this.chkCommonDocuments.TabIndex = 10;
            this.chkCommonDocuments.Text = "Documents";
            this.toolTip1.SetToolTip(this.chkCommonDocuments, "These are for the \"Public\" user for shared files");
            this.chkCommonDocuments.UseVisualStyleBackColor = true;
            // 
            // chkCommonDesktop
            // 
            this.chkCommonDesktop.AutoSize = true;
            this.chkCommonDesktop.Checked = true;
            this.chkCommonDesktop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommonDesktop.Location = new System.Drawing.Point(39, 65);
            this.chkCommonDesktop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCommonDesktop.Name = "chkCommonDesktop";
            this.chkCommonDesktop.Size = new System.Drawing.Size(95, 24);
            this.chkCommonDesktop.TabIndex = 9;
            this.chkCommonDesktop.Text = "Desktop";
            this.toolTip1.SetToolTip(this.chkCommonDesktop, "These are for the \"Public\" user for shared files");
            this.chkCommonDesktop.UseVisualStyleBackColor = true;
            // 
            // btnLoadRestore
            // 
            this.btnLoadRestore.Location = new System.Drawing.Point(130, 29);
            this.btnLoadRestore.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoadRestore.Name = "btnLoadRestore";
            this.btnLoadRestore.Size = new System.Drawing.Size(112, 35);
            this.btnLoadRestore.TabIndex = 21;
            this.btnLoadRestore.Text = "Restore";
            this.toolTip1.SetToolTip(this.btnLoadRestore, "Click to browse for a RSTR file to start restroe");
            this.btnLoadRestore.UseVisualStyleBackColor = true;
            this.btnLoadRestore.Click += new System.EventHandler(this.btnLoadRestore_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(9, 29);
            this.btnBackup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(112, 35);
            this.btnBackup.TabIndex = 20;
            this.btnBackup.Text = "Backup";
            this.toolTip1.SetToolTip(this.btnBackup, "Click to browse for a place to save the backup");
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnStartProcess
            // 
            this.btnStartProcess.Enabled = false;
            this.btnStartProcess.Location = new System.Drawing.Point(896, 566);
            this.btnStartProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartProcess.Name = "btnStartProcess";
            this.btnStartProcess.Size = new System.Drawing.Size(112, 35);
            this.btnStartProcess.TabIndex = 24;
            this.btnStartProcess.Text = "Start";
            this.toolTip1.SetToolTip(this.btnStartProcess, "Start the Backup or Restore process");
            this.btnStartProcess.UseVisualStyleBackColor = true;
            this.btnStartProcess.Click += new System.EventHandler(this.btnStartProcess_Click);
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.lblMode);
            this.grpMode.Controls.Add(this.lblCurrentMode);
            this.grpMode.Controls.Add(this.btnModeCancel);
            this.grpMode.Controls.Add(this.btnBackup);
            this.grpMode.Controls.Add(this.btnLoadRestore);
            this.grpMode.Location = new System.Drawing.Point(636, 351);
            this.grpMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpMode.Name = "grpMode";
            this.grpMode.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpMode.Size = new System.Drawing.Size(372, 115);
            this.grpMode.TabIndex = 4;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "Choose mode";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(130, 75);
            this.lblMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(54, 20);
            this.lblMode.TabIndex = 12;
            this.lblMode.Text = "NONE";
            // 
            // lblCurrentMode
            // 
            this.lblCurrentMode.AutoSize = true;
            this.lblCurrentMode.Location = new System.Drawing.Point(10, 75);
            this.lblCurrentMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentMode.Name = "lblCurrentMode";
            this.lblCurrentMode.Size = new System.Drawing.Size(110, 20);
            this.lblCurrentMode.TabIndex = 11;
            this.lblCurrentMode.Text = "Current mode:";
            // 
            // btnModeCancel
            // 
            this.btnModeCancel.Enabled = false;
            this.btnModeCancel.Location = new System.Drawing.Point(252, 29);
            this.btnModeCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnModeCancel.Name = "btnModeCancel";
            this.btnModeCancel.Size = new System.Drawing.Size(112, 35);
            this.btnModeCancel.TabIndex = 22;
            this.btnModeCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnModeCancel, "Click to cancel the current mode");
            this.btnModeCancel.UseVisualStyleBackColor = true;
            this.btnModeCancel.Click += new System.EventHandler(this.btnModeCancel_Click);
            // 
            // blbLocation
            // 
            this.blbLocation.AutoSize = true;
            this.blbLocation.Location = new System.Drawing.Point(327, 586);
            this.blbLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.blbLocation.Name = "blbLocation";
            this.blbLocation.Size = new System.Drawing.Size(183, 20);
            this.blbLocation.TabIndex = 11;
            this.blbLocation.Text = "Backup/Restore location";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(327, 611);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(679, 26);
            this.txtLocation.TabIndex = 12;
            this.txtLocation.TabStop = false;
            this.toolTip1.SetToolTip(this.txtLocation, "This is the currently set backup or restore location");
            // 
            // btnRescan
            // 
            this.btnRescan.Location = new System.Drawing.Point(896, 475);
            this.btnRescan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRescan.Name = "btnRescan";
            this.btnRescan.Size = new System.Drawing.Size(112, 35);
            this.btnRescan.TabIndex = 23;
            this.btnRescan.Text = "Rescan installed programs";
            this.toolTip1.SetToolTip(this.btnRescan, "Click to rescan installed programs");
            this.btnRescan.UseVisualStyleBackColor = true;
            this.btnRescan.Click += new System.EventHandler(this.btnRescan_Click);
            // 
            // lblRescan
            // 
            this.lblRescan.AutoSize = true;
            this.lblRescan.Location = new System.Drawing.Point(676, 483);
            this.lblRescan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRescan.Name = "lblRescan";
            this.lblRescan.Size = new System.Drawing.Size(210, 20);
            this.lblRescan.TabIndex = 14;
            this.lblRescan.Text = "Rescan installed programs >";
            // 
            // lblBackupNotice
            // 
            this.lblBackupNotice.AutoSize = true;
            this.lblBackupNotice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackupNotice.ForeColor = System.Drawing.Color.Red;
            this.lblBackupNotice.Location = new System.Drawing.Point(321, 551);
            this.lblBackupNotice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBackupNotice.Name = "lblBackupNotice";
            this.lblBackupNotice.Size = new System.Drawing.Size(0, 29);
            this.lblBackupNotice.TabIndex = 25;
            // 
            // chkRestoreOnly
            // 
            this.chkRestoreOnly.AutoSize = true;
            this.chkRestoreOnly.Enabled = false;
            this.chkRestoreOnly.Location = new System.Drawing.Point(336, 551);
            this.chkRestoreOnly.Name = "chkRestoreOnly";
            this.chkRestoreOnly.Size = new System.Drawing.Size(127, 24);
            this.chkRestoreOnly.TabIndex = 26;
            this.chkRestoreOnly.Text = "Restore Only";
            this.chkRestoreOnly.UseVisualStyleBackColor = true;
            // 
            // FormBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 662);
            this.Controls.Add(this.chkRestoreOnly);
            this.Controls.Add(this.lblBackupNotice);
            this.Controls.Add(this.lblRescan);
            this.Controls.Add(this.btnRescan);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.blbLocation);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.btnStartProcess);
            this.Controls.Add(this.grpCommonFiles);
            this.Controls.Add(this.grpCustomFolders);
            this.Controls.Add(this.grpDataFromPrograms);
            this.Controls.Add(this.grpUserFiles);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormBackup";
            this.Text = "Kiry\'s Computers Backup/Restore";
            this.Load += new System.EventHandler(this.FormBackup_Load);
            this.grpUserFiles.ResumeLayout(false);
            this.grpUserFiles.PerformLayout();
            this.grpDataFromPrograms.ResumeLayout(false);
            this.grpDataFromPrograms.PerformLayout();
            this.grpCustomFolders.ResumeLayout(false);
            this.grpCommonFiles.ResumeLayout(false);
            this.grpCommonFiles.PerformLayout();
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpUserFiles;
        private System.Windows.Forms.CheckBox chkUserFiles;
        private System.Windows.Forms.CheckBox chkVideos;
        private System.Windows.Forms.CheckBox chkPictures;
        private System.Windows.Forms.CheckBox chkMusic;
        private System.Windows.Forms.CheckBox chkFavorites;
        private System.Windows.Forms.CheckBox chkDownloads;
        private System.Windows.Forms.CheckBox chkDocuments;
        private System.Windows.Forms.CheckBox chkDesktop;
        private System.Windows.Forms.GroupBox grpDataFromPrograms;
        private System.Windows.Forms.CheckBox chkThunderbird;
        private System.Windows.Forms.CheckBox chkChrome;
        private System.Windows.Forms.CheckBox chkFirefox;
        private System.Windows.Forms.GroupBox grpCustomFolders;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ListBox lstCustom;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox grpCommonFiles;
        private System.Windows.Forms.CheckBox chkCommonUserFiles;
        private System.Windows.Forms.CheckBox chkCommonVideos;
        private System.Windows.Forms.CheckBox chkCommonPictures;
        private System.Windows.Forms.CheckBox chkCommonMusic;
        private System.Windows.Forms.CheckBox chkCommonDownloads;
        private System.Windows.Forms.CheckBox chkCommonDocuments;
        private System.Windows.Forms.CheckBox chkCommonDesktop;
        private System.Windows.Forms.Button btnLoadRestore;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnStartProcess;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.Button btnModeCancel;
        private System.Windows.Forms.Label blbLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblCurrentMode;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnRescan;
        private System.Windows.Forms.Label lblRescan;
        private System.Windows.Forms.Label lblBackupNotice;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox chkRestoreOnly;
    }
}

