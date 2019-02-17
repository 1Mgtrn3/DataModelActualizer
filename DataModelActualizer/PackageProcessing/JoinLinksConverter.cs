using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelActualizer
{
    class JoinLinksConverter
    {
        public List<Join> joinsList { get; set; }

        public JoinLinksConverter(List<Join> listToConvert)
        {
            joinsList = listToConvert;
            Convert();
        }


        

        public List<Link> MakeUniqueLinksList()
        {
            Dictionary<string, Link> tmpDic = new Dictionary<string, Link>();
            foreach (var join in joinsList)
            {
                foreach (var link in join.links)
                {
                    link.Format();
                    var tmpLinkText = link.ToText();
                    if (!tmpDic.ContainsKey(tmpLinkText))
                    {
                        tmpDic.Add(tmpLinkText, link);
                    }

                }
            }
            
            List<Link> resultList = tmpDic.Values.ToList();

            return resultList;

        }

        private void Convert() {

            foreach (var join in joinsList)
            {

                foreach (var link in join.links)
                {
                    foreach (var condition in join.TableCondition)
                    {
                        var exp =  link.ToText();
                        if (condition.Contains(exp))
                        {
                            /// sw.WriteLine($"\t\tcontains: true");
                            //var link = new Link();
                            //var tmp = exp.Replace(".", " .").Replace("=", " = ");
                            //sw.WriteLine($"\t\ttmp: {tmp}");
                            //var tmpspace = tmp.Split(' ');
                            //sw.WriteLine("\t\ttmpspace: ");
                            //foreach (var tmpItem in tmpspace)
                            //{
                            //    sw.WriteLine($"\t\ttmpItem{tmpspace.}: ");
                            //}

                            //for (int i = 0; i < tmpspace.Length; i++)
                            //{
                            //    sw.WriteLine($"\t\ttmpItem{i}: {tmpspace[i]}");
                            //}

                            //if(aliases.Con)
                            if (join.Aliases.ContainsKey(link.firstTable))
                            {
                                link.firstTable = join.Aliases[link.firstTable];

                            }
                           
                            
                            
                            if (join.Aliases.ContainsKey(link.secondTable))
                            {
                                link.secondTable = join.Aliases[link.secondTable];

                            }
                            
                           

                            

                            

                            //sw.WriteLine($"\tResult Link: {link.ToText()}");
                        }
                        //else
                        //{
                        //    sw.WriteLine($"\t\tcontains: false");
                        //}
                    }
                }

            }
        }

    }
}
