namespace DatabaseDetective
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
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.btFindPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPathStart = new System.Windows.Forms.TextBox();
            this.tbPathFinish = new System.Windows.Forms.TextBox();
            this.btPrintTree = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTreeHead = new System.Windows.Forms.TextBox();
            this.tbDepth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btSwitchStorage = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lbStorageFiles = new System.Windows.Forms.ListBox();
            this.btRefresh = new System.Windows.Forms.Button();
            this.tbNewStorageName = new System.Windows.Forms.TextBox();
            this.btnCreateNewStorage = new System.Windows.Forms.Button();
            this.btScanObjects = new System.Windows.Forms.Button();
            this.btDownloadPck = new System.Windows.Forms.Button();
            this.btScanPackages = new System.Windows.Forms.Button();
            this.btGetFK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbOutput
            // 
            this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutput.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbOutput.Location = new System.Drawing.Point(3, 16);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(1090, 254);
            this.tbOutput.TabIndex = 0;
            this.tbOutput.Text = "";
            // 
            // btFindPath
            // 
            this.btFindPath.Location = new System.Drawing.Point(30, 25);
            this.btFindPath.Name = "btFindPath";
            this.btFindPath.Size = new System.Drawing.Size(75, 23);
            this.btFindPath.TabIndex = 1;
            this.btFindPath.Text = "Find a Path";
            this.btFindPath.UseVisualStyleBackColor = true;
            this.btFindPath.Click += new System.EventHandler(this.btFindPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "finish";
            // 
            // tbPathStart
            // 
            this.tbPathStart.Location = new System.Drawing.Point(129, 27);
            this.tbPathStart.Name = "tbPathStart";
            this.tbPathStart.Size = new System.Drawing.Size(107, 20);
            this.tbPathStart.TabIndex = 4;
            this.tbPathStart.Text = "ILCACCOUNTS";
            // 
            // tbPathFinish
            // 
            this.tbPathFinish.Location = new System.Drawing.Point(242, 28);
            this.tbPathFinish.Name = "tbPathFinish";
            this.tbPathFinish.Size = new System.Drawing.Size(187, 20);
            this.tbPathFinish.TabIndex = 5;
            this.tbPathFinish.Text = "ILACSPAYERAUTHREQLOG";
            // 
            // btPrintTree
            // 
            this.btPrintTree.Location = new System.Drawing.Point(30, 87);
            this.btPrintTree.Name = "btPrintTree";
            this.btPrintTree.Size = new System.Drawing.Size(75, 23);
            this.btPrintTree.TabIndex = 6;
            this.btPrintTree.Text = "Print a tree";
            this.btPrintTree.UseVisualStyleBackColor = true;
            this.btPrintTree.Click += new System.EventHandler(this.btPrintTree_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "head";
            // 
            // tbTreeHead
            // 
            this.tbTreeHead.Location = new System.Drawing.Point(129, 89);
            this.tbTreeHead.Name = "tbTreeHead";
            this.tbTreeHead.Size = new System.Drawing.Size(107, 20);
            this.tbTreeHead.TabIndex = 9;
            this.tbTreeHead.Text = "ILCACCOUNTS";
            // 
            // tbDepth
            // 
            this.tbDepth.Location = new System.Drawing.Point(242, 90);
            this.tbDepth.Name = "tbDepth";
            this.tbDepth.Size = new System.Drawing.Size(38, 20);
            this.tbDepth.TabIndex = 10;
            this.tbDepth.Text = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Depth";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbOutput);
            this.groupBox1.Location = new System.Drawing.Point(12, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1096, 273);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // btSwitchStorage
            // 
            this.btSwitchStorage.Location = new System.Drawing.Point(653, 25);
            this.btSwitchStorage.Name = "btSwitchStorage";
            this.btSwitchStorage.Size = new System.Drawing.Size(75, 38);
            this.btSwitchStorage.TabIndex = 14;
            this.btSwitchStorage.Text = "Switch storage";
            this.btSwitchStorage.UseVisualStyleBackColor = true;
            this.btSwitchStorage.Click += new System.EventHandler(this.btSwitchStorage_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(518, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Storage files";
            // 
            // lbStorageFiles
            // 
            this.lbStorageFiles.FormattingEnabled = true;
            this.lbStorageFiles.Location = new System.Drawing.Point(521, 28);
            this.lbStorageFiles.Name = "lbStorageFiles";
            this.lbStorageFiles.Size = new System.Drawing.Size(120, 95);
            this.lbStorageFiles.TabIndex = 16;
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(540, 129);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(75, 23);
            this.btRefresh.TabIndex = 17;
            this.btRefresh.Text = "Refresh list";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // tbNewStorageName
            // 
            this.tbNewStorageName.Location = new System.Drawing.Point(653, 72);
            this.tbNewStorageName.Name = "tbNewStorageName";
            this.tbNewStorageName.Size = new System.Drawing.Size(100, 20);
            this.tbNewStorageName.TabIndex = 18;
            // 
            // btnCreateNewStorage
            // 
            this.btnCreateNewStorage.Location = new System.Drawing.Point(653, 99);
            this.btnCreateNewStorage.Name = "btnCreateNewStorage";
            this.btnCreateNewStorage.Size = new System.Drawing.Size(75, 35);
            this.btnCreateNewStorage.TabIndex = 19;
            this.btnCreateNewStorage.Text = "Create new storage";
            this.btnCreateNewStorage.UseVisualStyleBackColor = true;
            this.btnCreateNewStorage.Click += new System.EventHandler(this.btnCreateNewStorage_Click);
            // 
            // btScanObjects
            // 
            this.btScanObjects.Location = new System.Drawing.Point(870, 27);
            this.btScanObjects.Name = "btScanObjects";
            this.btScanObjects.Size = new System.Drawing.Size(89, 36);
            this.btScanObjects.TabIndex = 20;
            this.btScanObjects.Text = "Scan ALL_OBJECTS";
            this.btScanObjects.UseVisualStyleBackColor = true;
            this.btScanObjects.Click += new System.EventHandler(this.btScanObjects_Click);
            // 
            // btDownloadPck
            // 
            this.btDownloadPck.Location = new System.Drawing.Point(870, 72);
            this.btDownloadPck.Name = "btDownloadPck";
            this.btDownloadPck.Size = new System.Drawing.Size(89, 34);
            this.btDownloadPck.TabIndex = 21;
            this.btDownloadPck.Text = "Download Packages";
            this.btDownloadPck.UseVisualStyleBackColor = true;
            this.btDownloadPck.Click += new System.EventHandler(this.btDownloadPck_Click);
            // 
            // btScanPackages
            // 
            this.btScanPackages.Location = new System.Drawing.Point(870, 119);
            this.btScanPackages.Name = "btScanPackages";
            this.btScanPackages.Size = new System.Drawing.Size(89, 42);
            this.btScanPackages.TabIndex = 22;
            this.btScanPackages.Text = "Scan Packages";
            this.btScanPackages.UseVisualStyleBackColor = true;
            this.btScanPackages.Click += new System.EventHandler(this.btScanPackages_Click);
            // 
            // btGetFK
            // 
            this.btGetFK.Location = new System.Drawing.Point(976, 119);
            this.btGetFK.Name = "btGetFK";
            this.btGetFK.Size = new System.Drawing.Size(75, 36);
            this.btGetFK.TabIndex = 27;
            this.btGetFK.Text = "Get Foreign Keys";
            this.btGetFK.UseVisualStyleBackColor = true;
            this.btGetFK.Click += new System.EventHandler(this.btGetFK_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 454);
            this.Controls.Add(this.btGetFK);
            this.Controls.Add(this.btScanPackages);
            this.Controls.Add(this.btDownloadPck);
            this.Controls.Add(this.btScanObjects);
            this.Controls.Add(this.btnCreateNewStorage);
            this.Controls.Add(this.tbNewStorageName);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.lbStorageFiles);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btSwitchStorage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDepth);
            this.Controls.Add(this.tbTreeHead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btPrintTree);
            this.Controls.Add(this.tbPathFinish);
            this.Controls.Add(this.tbPathStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btFindPath);
            this.Name = "MainWindow";
            this.Text = "DataModelActualizer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox tbOutput;
        private System.Windows.Forms.Button btFindPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPathStart;
        private System.Windows.Forms.TextBox tbPathFinish;
        private System.Windows.Forms.Button btPrintTree;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTreeHead;
        private System.Windows.Forms.TextBox tbDepth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btSwitchStorage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbStorageFiles;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.TextBox tbNewStorageName;
        private System.Windows.Forms.Button btnCreateNewStorage;
        private System.Windows.Forms.Button btScanObjects;
        private System.Windows.Forms.Button btDownloadPck;
        private System.Windows.Forms.Button btScanPackages;
        private System.Windows.Forms.Button btGetFK;
    }
}