using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDetective
{
    class DBControl
    {
        public DBControl()
        {

        }

        public void DownloadDbObjectNames()
        {
            dbObjectsParser parser = new dbObjectsParser(ConfigurationManager.AppSettings["OracleConnection"]);
            parser.PopulateTable();
            


        }

        public void DownloadPackageBodies() {

            using (var storage = new StorageContext(StorageContext.constr))
            {
                List<string> packageNames = storage.actualDbObjects.Where(o => o.Type == "package").Select(o => o.Name).ToList(); 

                packageDownloader downloader = new packageDownloader(ConfigurationManager.AppSettings["OracleConnection"], packageNames);
                downloader.DownloadPackageBodies(ConfigurationManager.AppSettings["packageDownloadDir"]);

            }


               
        }

        public void DownloadForeignKeys() {
            var schemaFetcher = new SchemaFetcher(ConfigurationManager.AppSettings["OracleConnection"]);
            schemaFetcher.CreateLinkList();
            schemaFetcher.AddLinksToStorage();
            

        }
    }
}
