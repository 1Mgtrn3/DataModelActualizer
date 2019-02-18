using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDetective
{
    class Join
    {
        private string initialTable;

        public string InitialTable
        {
            get { return initialTable; }
            set { initialTable = value; }
        }

        private Dictionary<string,string> aliases;

        public Dictionary<string,string> Aliases
        {
            get { return aliases; }
            set { aliases = value; }
        }

        //public Dictionary<string, string>
        private List<string> tableCondition;

        public List<string> TableCondition
        {
            get { return tableCondition; }
            set { tableCondition = value; }
        }

        public List<Link> links { get; set; }



        public Join()
        {
            links = new List<Link>();

            Aliases = new Dictionary<string, string>();
            TableCondition = new List<string>();

        }
    }
}
