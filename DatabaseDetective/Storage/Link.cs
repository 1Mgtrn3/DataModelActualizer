using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DatabaseDetective
{
    class Link
    {   [Key]
        public string toText { get; set; }
        public int id2 { get; set; }
        public int id1 { get; set; }
        public string firstTable { get; set; }
        public string secondTable { get; set; }
        public string firstTableField { get; set; }
        public string secondTableField { get; set; }

        public Link()
        {

        }

        public Link(string table1, string field1, string table2, string field2)
        {
            firstTable = table1;
            secondTable = table2;
            firstTableField = field1;
            secondTableField = field2;
        }

        public Link(string exp)
        {
            var tmp = exp.Replace(".", " .").Replace("=", " = ");
            //sw.WriteLine($"\t\ttmp: {tmp}");
            var tmpspace = tmp.Split(' ');
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
            firstTable = tmpspace[0];

            //if (join.Aliases.ContainsKey(tmpspace[0]))
            //{
            //    link.firstTable = join.Aliases[tmpspace[0]];

            //}
            //else
            //{ link.firstTable = tmpspace[0]; }


            firstTableField = tmpspace[1].Substring(1);

            secondTable = tmpspace[3];
            //if (join.Aliases.ContainsKey(tmpspace[3]))
            //{
            //    link.secondTable = join.Aliases[tmpspace[3]];

            //}
            //else
            //{ link.secondTable = tmpspace[3]; }

            secondTableField = tmpspace[4].Substring(1);
        }

        public void Format()
        {
            var listToSort = new List<String>();
            listToSort.Add(firstTable);
            listToSort.Add(secondTable);
            listToSort.Sort();

            if (listToSort[0] == secondTable)
            {
                

                var tmpt2field = secondTableField;

                secondTableField = firstTableField;

                firstTableField = tmpt2field;

                firstTable = listToSort[0];
                secondTable = listToSort[1];
            }
        

        }


        public string ToText()
        {
            return $"{firstTable}.{firstTableField}={secondTable}.{secondTableField}";
        }
    }
}
