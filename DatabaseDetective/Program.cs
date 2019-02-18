using Antlr4.Runtime;
using System;
using System.IO;
using Antlr4;
using Antlr4.Runtime.Tree;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace DatabaseDetective
{
    class Program
    {


        [STAThread]
        static void Main(string[] args)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string storageFilesDir = ConfigurationManager.AppSettings["storageFilesDir"];
            string absoluteStorageFilesDir = baseDir + storageFilesDir;
            
            if (!Directory.Exists(absoluteStorageFilesDir))
            {
                Directory.CreateDirectory(absoluteStorageFilesDir);
                StorageControl sControl = new StorageControl();
                sControl.CreateNewStorage("objectsStorage.db");
            }

            string packageSourceDir = ConfigurationManager.AppSettings["packageSourceDir"];
            Directory.CreateDirectory(baseDir + packageSourceDir);
            string packageDownloadDir = ConfigurationManager.AppSettings["packageDownloadDir"];
            Directory.CreateDirectory(baseDir + packageDownloadDir);




            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());








            
        }
    }

    
}
