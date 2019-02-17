using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Oracle.DataAccess.Client;

namespace DataModelActualizer
{
    class packageDownloader
    {
        public packageDownloader(string connString)
        {
            connectionString = connString;
            packageNames = new List<string>();
        }

        public packageDownloader(string connString, List<string> PackageNames)
        {
            connectionString = connString;
            packageNames = PackageNames;
        }

        public string connectionString { get; set; }
        public List<string> packageNames { get; set; }


        public void PrintPackageNames(string path) {
            

            using(StreamWriter sw = new StreamWriter(path,append: true))
            {
                foreach (var name in packageNames)
                {
                    sw.WriteLine(name);
                }
            }

        }



        public void DownloadPackageBodies(string dir)
        {

            using (OracleConnection connect = new OracleConnection())
            {
                connect.ConnectionString = connectionString;
                connect.Open();

                foreach (var packageName in packageNames)
                {

                    var command = connect.CreateCommand();

                    string tmpQuery = ConfigurationManager.AppSettings["packageSourcesQuery"];

                    string query = tmpQuery.Replace("[PACKAGE_NAME_PLACEHOLDER]", packageName);
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            File.AppendAllText(dir + packageName+".bdy", reader.GetString(0));

                        }


                    }


                }

                
                connect.Close();
            }

        }



        //public void DownloadPackageNames() {



        //    using (OracleConnection connect = new OracleConnection())
        //    {
        //        connect.ConnectionString = connectionString;
        //        connect.Open();
        //        var command = connect.CreateCommand();

        //        string query = ConfigurationManager.AppSettings["packageNamesQuery"];
        //        command.CommandText = query.ToUpper();

        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                packageNames.Add(Convert.ToString(reader["NAME"]));
                        


        //            }
                   

        //        }


        //        connect.Close();
        //    }


        //}
    }
}
