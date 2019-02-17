using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataModelActualizer
{
    class LinkDBFormatConverter
    {
        public List<Link> Result { get; set; }
        public  LinkDBFormatConverter(List<Link> inputList)
        {
            Result = new List<Link>();
            using (var storage = new StorageContext(StorageContext.constr))
            {
               var dbObjectsList = storage.actualDbObjects.ToList();

                foreach (var link in inputList)
                {
                    link.id1 = dbObjectsList.SingleOrDefault(t => t.Name == link.firstTable).ID;
                    link.id2 = dbObjectsList.SingleOrDefault(t => t.Name == link.secondTable).ID;
                    link.toText = link.ToText();

                    Result.Add(link);
                }
                
            }

        }

        public LinkDBFormatConverter(List<Link> inputList, bool format)
        {
            Result = new List<Link>();
            //var tmpResult = new List<Link>();
            using (var storage = new StorageContext(StorageContext.constr))
            {
                var dbObjectsList = storage.actualDbObjects.ToList();

                foreach (var link in inputList)
                {
                    link.Format();
                    try
                    {
                        link.id1 = dbObjectsList.SingleOrDefault(t => t.Name == link.firstTable).ID;
                        
                    }
                    catch (NullReferenceException exnull)
                    {
                        Trace.WriteLine($"NullReferenceException occured! Referred tables|views are not present in the storage. Check {link.firstTable}'s properties like owner for example to correct your SQL quiries.");

                        break;
                    }

                    try
                    {
                        link.id2 = dbObjectsList.SingleOrDefault(t => t.Name == link.secondTable).ID;
                    }
                    catch (NullReferenceException exnull)
                    {

                        Trace.WriteLine($"NullReferenceException occured! Referred tables|views are not present in the storage. Check {link.secondTable}'s properties like owner for example to correct your SQL quiries.");
                        break;
                    }
                   

                    link.toText = link.ToText();

                    //tmpResult.Add(link);
                    if (!Result.Exists(l => l.toText == link.toText))
                    {
                        Result.Add(link);
                    }
                }

                



            }
        }
    }
}
