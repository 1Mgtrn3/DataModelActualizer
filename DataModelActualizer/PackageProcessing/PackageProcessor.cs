using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Antlr4.Runtime;
using Antlr4;
using Antlr4.Runtime.Tree;
using System.Diagnostics;

namespace DataModelActualizer
{
    class PackageProcessor
    {

        private string packageSourceDir { get; set; }
        
        private List<ActualDbObject> dbObjectsList { get; set; }
        private Dictionary<string, string> dbObjectNames { get; set; } //dictionary.ContainsKey is faster than HashSet.Contains


        public PackageProcessor()
        {
            try
            {
                packageSourceDir = ConfigurationManager.AppSettings["packageSourceDir"];

                dbObjectNames = new Dictionary<string, string>();
                using (var storage = new StorageContext(StorageContext.constr))
                {
                    dbObjectsList = storage.actualDbObjects.ToList();

                    foreach (var dbobj in dbObjectsList)
                    {
                        dbObjectNames.Add(dbobj.Name, "");
                    }

                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
               

        }


        public void formatPackages() {

            DirectoryInfo directoryInfo = new DirectoryInfo(packageSourceDir);

            foreach (FileInfo file in directoryInfo.GetFiles("*.bdy"))
            {
                string fileTmp = File.ReadAllText(file.FullName);
                fileTmp = fileTmp.ToUpper();
                if (!fileTmp.Contains("CREATE OR REPLACE PACKAGE BODY") && fileTmp.Contains("PACKAGE BODY"))
                {
                    
                    var indexStart = fileTmp.IndexOf("PACKAGE BODY");
                    fileTmp = "CREATE OR REPLACE " + fileTmp.Substring(indexStart);
                   
                    string firstline;
                    

                    using (StringReader reader = new StringReader(fileTmp))
                    {
                        
                            firstline = reader.ReadLine();
                       
                        

                    }

                    

                    string lastLine = ";";
                    fileTmp = fileTmp.Substring(0, fileTmp.LastIndexOf(lastLine)+lastLine.Length);
                   
                    
                


                }
                fileTmp = fileTmp.ToUpper().Replace("OUTER JOIN", "JOIN").Replace("INNER JOIN", "JOIN");
                File.WriteAllText(file.FullName + ".cnv", fileTmp);

            }
        }


        public void startProcessing() {
            try
            {
                formatPackages();

                DirectoryInfo directoryInfo = new DirectoryInfo(packageSourceDir);

                foreach (FileInfo file in directoryInfo.GetFiles("*.cnv"))
                {




                    processPackage(file.FullName);


                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }

        }


        public void processPackage(string inputFilePath) {

            #region ANTLR4 necessary stuff
           


            var input = new AntlrInputStream(new StreamReader(inputFilePath));
           

            var lexer = new PlSqlLexer(input);
            
            var tokens = new CommonTokenStream(lexer);
            
            var parser = new PlSqlParser(tokens);

            PlSqlParser.Create_package_bodyContext context1 = parser.create_package_body();

            ParseTreeWalker walker = new ParseTreeWalker();

            var bListener = new BODYlistener();

            walker.Walk(bListener, context1);
            #endregion

            //you can start making changes form this point

            var converter = new JoinLinksConverter(bListener.joinsList);
            var uniqueLinks = converter.MakeUniqueLinksList();



            using (var storage = new StorageContext(StorageContext.constr))
            {
                

                Dictionary<string, string> linkToText = new Dictionary<string, string>();

                foreach (var link in storage.links.ToList()) {
                    linkToText.Add(link.toText, "");
                }

                

                List<Link> linkListToConvert = new List<Link>();

                linkListToConvert.AddRange(uniqueLinks.Where(link => dbObjectNames.ContainsKey(link.firstTable) && dbObjectNames.ContainsKey(link.secondTable) && !linkToText.ContainsKey(link.ToText())));

                LinkDBFormatConverter linkConverter = new LinkDBFormatConverter(linkListToConvert);

                foreach (var link in linkConverter.Result)
                {
                    storage.links.Add(link);
                }


                storage.SaveChanges();
            }



        }

       


    }
}
