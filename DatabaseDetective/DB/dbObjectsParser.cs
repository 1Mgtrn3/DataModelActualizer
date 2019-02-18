using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Oracle.DataAccess.Client;
using System.Diagnostics;

namespace DatabaseDetective
{
    class dbObjectsParser
    {
        //string filePathTables = ConfigurationManager.AppSettings["tables"];
        //string filePathViews = ConfigurationManager.AppSettings["views"];

        private string filePathTables { get; set; }
        private string filePathViews { get; set; }
        private string filePathPackages { get; set; }


        //private Dictionary<string,string> result;

        public List<KeyValuePair<string, string>> Result
        {
            get { return result; }
            set { result = value; }
        }

        private List<KeyValuePair<string, string>> result;

        

        public dbObjectsParser(string FilePathTables, string FilePathViews, string FilePathPackages)
        {
            filePathTables = FilePathTables;
            filePathViews = FilePathViews;
            filePathPackages = FilePathPackages;

            Result = new List<KeyValuePair<string, string>>();

            var tables = File.ReadAllLines(filePathTables);
            var views = File.ReadAllLines(filePathViews);
            var packages = File.ReadAllLines(filePathPackages);
            foreach (var item in tables)
            {
                Result.Add(new KeyValuePair<string, string>(item, "table"));
            }

            foreach (var item in views)
            {
                Result.Add(new KeyValuePair<string, string>(item, "view"));
            }

            foreach (var item in packages)
            {
                Result.Add(new KeyValuePair<string, string>(item, "package"));
            }




        }


        public dbObjectsParser(string connectionString)
        {
            try
            {
                List<string> tables = new List<string>();
                List<string> views = new List<string>();
                List<string> packages = new List<string>();
                var tablesSet = new HashSet<string>();
                var viewsSet = new HashSet<string>();
                var packageSet = new HashSet<string>();


                Result = new List<KeyValuePair<string, string>>();
                using (OracleConnection connect = new OracleConnection())
                {
                    connect.ConnectionString = connectionString;
                    connect.Open();
                    var command = connect.CreateCommand();

                    string query = ConfigurationManager.AppSettings["objectsQuery"];
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = Convert.ToString(reader["OBJECT_TYPE"]);

                            switch (type)
                            {
                                case "TABLE":
                                    {

                                        string tableName = Convert.ToString(reader["OBJECT_NAME"]);
                                        if (!tablesSet.Contains(tableName))
                                        {
                                            tablesSet.Add(tableName);
                                            tables.Add(tableName);
                                        }

                                        break;
                                    }
                                case "VIEW":
                                    {
                                        string viewName = Convert.ToString(reader["OBJECT_NAME"]);

                                        if (!viewsSet.Contains(viewName))
                                        {
                                            viewsSet.Add(viewName);
                                            views.Add(viewName);
                                        }
                                        break;
                                    }
                                case "PACKAGE BODY":
                                    {
                                        string packageName = Convert.ToString(reader["OBJECT_NAME"]);

                                        if (!packageSet.Contains(packageName))
                                        {
                                            packageSet.Add(packageName);
                                            packages.Add(packageName);
                                        }

                                        break;
                                    }

                            }





                            //if (type == "TABLE")
                            //{
                            //    string tableName = Convert.ToString(reader["OBJECT_NAME"]);
                            //    if (!tablesSet.Contains(tableName))
                            //    {
                            //        tablesSet.Add(tableName);
                            //        tables.Add(tableName);
                            //    }

                            //}
                            //else
                            //{
                            //    string viewName = Convert.ToString(reader["OBJECT_NAME"]);

                            //    if (!viewsSet.Contains(viewName))
                            //    {
                            //        viewsSet.Add(viewName);
                            //        views.Add(viewName);
                            //    }


                            //}


                        }

                    }
                    connect.Close();
                }



                foreach (var item in tables)
                {

                    Result.Add(new KeyValuePair<string, string>(item, "table"));

                }

                foreach (var item in views)
                {
                    Result.Add(new KeyValuePair<string, string>(item, "view"));
                }

                foreach (var item in packages)
                {
                    Result.Add(new KeyValuePair<string, string>(item, "package"));
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
            
        }




        public void PopulateTable() {

            try
            {
                using (var storage = new StorageContext(StorageContext.constr))
                {
                    storage.Database.ExecuteSqlCommand("DELETE FROM ActualDbObjects");
                    //storage.actualDbObjects.
                    foreach (var result in Result)
                    {

                        var dbObject = new ActualDbObject();
                        dbObject.Name = result.Key;
                        dbObject.Type = result.Value;

                        storage.actualDbObjects.Add(dbObject);

                    }
                    storage.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }

            
        }

        public void OutputTable()
        {
            
                using (var storage = new StorageContext(StorageContext.constr))
                {
                    foreach (var dbObject in storage.actualDbObjects.ToList())
                    {
                        
                            Trace.WriteLine($"dbObject: id = {dbObject.ID} name = {dbObject.Name} type = {dbObject.Type}");
                        
                    }

                }

            
        }
    }
}
