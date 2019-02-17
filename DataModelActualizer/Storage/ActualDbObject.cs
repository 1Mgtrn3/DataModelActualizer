using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataModelActualizer
{
    public class ActualDbObject
    {

        public ActualDbObject()
        {

        }
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
