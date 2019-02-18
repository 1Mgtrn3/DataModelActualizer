using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDetective
{
    class VertexNode
    {
        public int Vertex { get; set; }  //vertex number
        public VertexNode Next { get; set; }  //reachable vertex
        public VertexNode(int val)
        {
            Vertex = val;
            Distance = Int32.MaxValue;
            Color = 'W';
        }

        public int Distance { get; set; } //distance from source
        public char Color { get; set; }    //initial color
        public VertexNode Parent { get; set; }  //parent node for keep tracking direction
    }
}
