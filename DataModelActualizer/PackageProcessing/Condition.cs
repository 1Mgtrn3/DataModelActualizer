using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelActualizer
{
    class Condition
    {
        public List<Link> linkList { get; set; }

        public Condition()
        {
            linkList = new List<Link>(); ;
        }

        private string conditionText;

        public string ConditionText
        {
            get { return conditionText; }
            set { conditionText = value; }
        }



        public void parseConditionText()
        {
            if (conditionText.Count(f => f == '=') > 2)
            {

            }
        }

    }
}
