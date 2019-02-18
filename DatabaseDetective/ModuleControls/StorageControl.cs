using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;
using System.Diagnostics;

namespace DatabaseDetective
{
    class StorageControl
    {
        public StorageControl()
        {

        }

        public void SwitchStorage(string NewStorageName) {

            try
            {
                if (ConfigurationManager.ConnectionStrings[$"{NewStorageName}"] == null)
                {
                    //Trace.WriteLine("Adding new storage");
                    string storageFilesDir = ConfigurationManager.AppSettings["storageFilesDir"];
                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(NewStorageName, $@"Data Source=|DataDirectory|\{storageFilesDir}{NewStorageName}", "System.Data.SQLite.EF6"));

                    config.Save(ConfigurationSaveMode.Minimal);

                    ConfigurationManager.RefreshSection(config.ConnectionStrings.SectionInformation.SectionName);
                    //Trace.WriteLine("Connections section refreshed");

                }

                var newConnectionString = ConfigurationManager.ConnectionStrings[$"{NewStorageName}"];
                //Trace.WriteLine(newConnectionString);
                StorageContext.constr = newConnectionString;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
            
        }

        public void CreateNewStorage(string NewStorageName) {

            var newStorageFileName = NewStorageName;



            

            string storageFilesDir = ConfigurationManager.AppSettings["storageFilesDir"];
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(newStorageFileName, $@"Data Source=|DataDirectory|\{storageFilesDir}{newStorageFileName}", "System.Data.SQLite.EF6"));

            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection(config.ConnectionStrings.SectionInformation.SectionName);

            


            var newConnectionString = ConfigurationManager.ConnectionStrings[$"{newStorageFileName}"];
            



            string relPath = AppDomain.CurrentDomain.BaseDirectory;
            SQLiteConnection.CreateFile(relPath + newConnectionString.ConnectionString.Substring(27));

            SQLiteConnection m_dbConnection = new SQLiteConnection(newConnectionString.ConnectionString);
            m_dbConnection.Open();

            string sql = "CREATE TABLE 'ActualDbObjects' ([ID] INTEGER PRIMARY KEY, [Name] nvarchar, [Type] nvarchar)";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE 'Links' ([toText] nvarchar (128) NOT NULL PRIMARY KEY, [id2] int NOT NULL, [id1] int NOT NULL, [firstTable] nvarchar, [secondTable] nvarchar, [firstTableField] nvarchar, [secondTableField] nvarchar, [popularity] int NOT NULL)";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

           
            m_dbConnection.Dispose();
           

        }
    }
}
