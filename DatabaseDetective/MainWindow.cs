using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace DatabaseDetective
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            




           
        }
        
        private void btFindPath_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            tbOutput.AppendText($"PATH FROM {tbPathStart.Text} TO {tbPathFinish.Text}\r\n");
            string start = tbPathStart.Text;
            string finish = tbPathFinish.Text;



            GraphControl gControl = new GraphControl();
            gControl.FindPathJson(start, finish, Indented: true);

            tbOutput.AppendText(gControl.FindPathJson(start, finish, Indented: true));
            

            ScrollToEnd();
            SetUIBlockState(block: false);

        }


        private int StringToInt(string input) {

            
            int i = 0;
            if (!Int32.TryParse(input, out i))
            {
                i = -1;
            }
            return i;
        }

        private void btPrintTree_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            try
            {
                GraphControl gControl = new GraphControl();

                var output = gControl.CreateTreeJson(gControl.converter.ConvertNametoID(tbTreeHead.Text), StringToInt(tbDepth.Text), Indented: true);
                tbOutput.AppendText($"PRINTING A TREE STARTING FROM {tbTreeHead.Text} WITH DUPLICATES\r\n");
                tbOutput.AppendText(output);
                tbOutput.AppendText("========================\r\n");



                ScrollToEnd();
            }
            catch (Exception ex)
            {

                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
            }
            SetUIBlockState(block: false);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            refreshStorageFiles();

            tbOutput.AppendText($"Current storage file used: {StorageContext.constr.ToString()} \r\n");
            //tbOutput.AppendText($"Current db connectionstring: {StorageContext.constr.ConnectionString} \r\n");
        }

        public void refreshStorageFiles() {
            try
            {
                lbStorageFiles.Items.Clear();
                var storageFilesDir = ConfigurationManager.AppSettings["storageFilesDir"];
                DirectoryInfo directoryInfo = new DirectoryInfo(storageFilesDir);

                foreach (FileInfo file in directoryInfo.GetFiles("*.db"))
                {

                    lbStorageFiles.Items.Add(file.Name);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
        }

        

        private void btRefresh_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            try
            {
                refreshStorageFiles();
                tbOutput.AppendText("\r\nSTORAGE LIST REFRESH\r\n");
                ScrollToEnd();
            }
            catch (Exception ex)
            {
                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
            }
            SetUIBlockState(block: false);
        }

        private void btSwitchStorage_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            try
            {
                StorageControl sControl = new StorageControl();
                sControl.SwitchStorage(lbStorageFiles.SelectedItem.ToString());
                tbOutput.AppendText($"\r\nSTORAGE SWITCHED TO {lbStorageFiles.SelectedItem.ToString()}\r\n");
                ScrollToEnd();
            }
            catch (Exception ex)
            {

                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
            }
            SetUIBlockState(block: false);
        }

        private void btnCreateNewStorage_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            try
            {
                StorageControl sControl = new StorageControl();
                var newStorageFileName = tbNewStorageName.Text;
                sControl.CreateNewStorage(newStorageFileName);

                refreshStorageFiles();
            }
            catch (Exception ex)
            {

                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
            }
            SetUIBlockState(block: false);
        }

        private void btScanObjects_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            try
            {
                dbObjectsParser parser = new dbObjectsParser(ConfigurationManager.AppSettings["OracleConnection"]);
                parser.PopulateTable();
                //parser.OutputTable();
                tbOutput.AppendText("\r\nObjects scanning complete!\r\n");
                ScrollToEnd();
            }
            catch (Exception ex)
            {
                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
                
            }
            
            SetUIBlockState(block: false);
        }

        private void btDownloadPck_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            //tbOutput.AppendText($"Check out package names at {ConfigurationManager.AppSettings["packagenamesFile"]}!!\r\n");
            try
            {
                DBControl dbControl = new DBControl();
                dbControl.DownloadPackageBodies();
                tbOutput.AppendText($"Downloading done!\r\n");
                ScrollToEnd();
            }
            catch (Exception ex)
            {

                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
            }

            
            SetUIBlockState(block: false);
        }

        private void btScanPackages_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            try
            {
                var proc = new PackageProcessor();
                proc.startProcessing();
                tbOutput.AppendText($"Scanning and processing complete!\r\n");
            }
            catch (Exception ex)
            {

                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
            }
            
            SetUIBlockState(block: false);
        }

        

        

        

        private void btGetFK_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            try
            {
                var schemaFetcher = new SchemaFetcher(ConfigurationManager.AppSettings["OracleConnection"]);
                schemaFetcher.CreateLinkList();
                tbOutput.AppendText($"FKs fetched!\r\n");
                schemaFetcher.AddLinksToStorage();
                tbOutput.AppendText($"Links added to db!\r\n");
            }
            catch (Exception ex)
            {

                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
            }
            
            SetUIBlockState(block: false);

        }

        private void btN_MostPopular_Click(object sender, EventArgs e)
        {
            SetUIBlockState(block: true);
            try
            {
                var popularity = new PopularityControl();
                var output = popularity.Get_N_MostPopularJson(StringToInt(tbN_MostPopular.Text), Indented: true);
                tbOutput.AppendText($"\r\nPRINTING {tbN_MostPopular.Text} MOST POPULAR LINKS:\r\n{output}");
                ScrollToEnd();
            }
            catch (Exception ex)
            {

                tbOutput.AppendText($"\r\nException occured: {ex.Message}\r\n");
            }

            SetUIBlockState(block: false);
        }

        private void ScrollToEnd() {
            tbOutput.SelectionStart = tbOutput.Text.Length;
            tbOutput.ScrollToCaret();
        }

        private void SetUIBlockState(bool block) {
            if (block)
            {
                tbOutput.AppendText("!!!PROCESSING REQUEST:please wait!!!\r\n");
                ScrollToEnd();
                tbOutput.Enabled = false;
                btFindPath.Enabled = false;
                label1.Enabled = false;
                label2.Enabled = false;
                tbPathStart.Enabled = false;
                tbPathFinish.Enabled = false;
                btPrintTree.Enabled = false;
                label3.Enabled = false;
                tbTreeHead.Enabled = false;
                tbDepth.Enabled = false;
                label4.Enabled = false;
                groupBox1.Enabled = false;
                btSwitchStorage.Enabled = false;
                label5.Enabled = false;
                lbStorageFiles.Enabled = false;
                btRefresh.Enabled = false;
                tbNewStorageName.Enabled = false;
                btnCreateNewStorage.Enabled = false;
                btScanObjects.Enabled = false;
                btDownloadPck.Enabled = false;
                btScanPackages.Enabled = false;
                btGetFK.Enabled = false;
                btN_MostPopular.Enabled = false;
                tbN_MostPopular.Enabled = false;
                this.Text = "Processing...";
            }
            else
            {
                tbOutput.Enabled = true;
                btFindPath.Enabled = true;
                label1.Enabled = true;
                label2.Enabled = true;
                tbPathStart.Enabled = true;
                tbPathFinish.Enabled = true;
                btPrintTree.Enabled = true;
                label3.Enabled = true;
                tbTreeHead.Enabled = true;
                tbDepth.Enabled = true;
                label4.Enabled = true;
                groupBox1.Enabled = true;
                btSwitchStorage.Enabled = true;
                label5.Enabled = true;
                lbStorageFiles.Enabled = true;
                btRefresh.Enabled = true;
                tbNewStorageName.Enabled = true;
                btnCreateNewStorage.Enabled = true;
                btScanObjects.Enabled = true;
                btDownloadPck.Enabled = true;
                btScanPackages.Enabled = true;
                btGetFK.Enabled = true;
                btN_MostPopular.Enabled = true;
                tbN_MostPopular.Enabled = true;
                this.Text = "Database Detective";
                
            }


        }

        
    }
}
