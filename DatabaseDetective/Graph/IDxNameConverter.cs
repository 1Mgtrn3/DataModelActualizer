using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDetective
{
    class IDxNameConverter
    {
        public string name { get; set; }
        public int Id { get; set; }
        
        public IDxNameConverter(int ID)
        {
           
            name = ConvertIDtoName(ID);


        }

        public IDxNameConverter(string Name)
        {
            Id = ConvertNametoID(Name);
        }

        public IDxNameConverter()
        {

        }

        public int ConvertNametoID(string Name) {
            using (var storage = new StorageContext(StorageContext.constr))
            {
                
                if (storage.actualDbObjects.SingleOrDefault(o => o.Name == Name) != null)
                {
                    return storage.actualDbObjects.SingleOrDefault(o => o.Name == Name).ID;
                }
                else
                { return 0; }

                
            }
        }

        public string ConvertIDtoName(int ID) {

            using (var storage = new StorageContext(StorageContext.constr))
            {
                if (storage.actualDbObjects.SingleOrDefault(o => o.ID == ID) != null)
                {
                    return storage.actualDbObjects.SingleOrDefault(o => o.ID == ID).Name;
                }
                else
                {
                    return "";
                }
               
            }
             

        }






    }
}
