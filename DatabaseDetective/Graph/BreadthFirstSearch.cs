using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDetective
{
    class BreadthFirstSearch
    {
        public int v;
        public VertexNode[] adjList;
        public BreadthFirstSearch(int totalVertix)
        {
            v = totalVertix;
            adjList = new VertexNode[totalVertix];
            for (int i = 0; i < adjList.Length; i++)
                adjList[i] = new VertexNode(i);

        }


        public void AddV(int u, int v)
        {
            VertexNode tempU = adjList[u];
            //finding if there is already same vertex v is connected 
            while (tempU.Next != null)
            {
                if (tempU.Vertex != v) //reaching last
                    tempU = tempU.Next;
                else             //v is already defined
                    return;
            }
            tempU.Next = new VertexNode(v);   // connecting new vertex v to u

            //for undirected graph we have to connect vertex v also to u
            VertexNode tempV = adjList[v];
            while (tempV.Next != null)
                tempV = tempV.Next;

            tempV.Next = new VertexNode(u);  //connecting v to u
        }

        private void BFS(int source)
        {
            Queue<VertexNode> queue = new Queue<VertexNode>(); //initializing vertex Queue
            VertexNode src = adjList[source]; //finding source
            src.Color = 'G'; //giving color grey to source
            src.Distance = 0; //0 distance to source
            src.Parent = null;  // np parent for source

            //marking all other vertices color to white distance is max and parent is null
            for (int i = 0; i < adjList.Length; i++)
            {
                VertexNode u = adjList[i];
                if (u.Vertex != source)
                {
                    u.Color = 'W';
                    u.Distance = Int32.MaxValue;
                    u.Parent = null;
                }
            }

            queue.Enqueue(src); //enquing source
            while (queue.Count > 0)
            {
                VertexNode u = queue.Dequeue();
                VertexNode v = u.Next; //finding linked vertex
                while (v != null)
                {
                    VertexNode mainV = adjList[v.Vertex];  // getting actual vertex
                    if (mainV.Color == 'W')  //process only if white
                    {
                        mainV.Color = 'G'; //grey for currently processing
                        mainV.Distance = u.Distance + 1; // distance 1+ from parent
                        mainV.Parent = u; //assigning parent
                        queue.Enqueue(mainV); //enqueue for finding connected nodes to this
                    }

                    v = v.Next; //another node connected to u
                }

                u.Color = 'B'; //once completed mark color as black
            }
        }


        private List<VertexNode> RecPrint(VertexNode u, VertexNode v, List<VertexNode> VertexList)
        {


            if (u == v)
                VertexList.Add(u);
                //Console.WriteLine(u.Vertex);
            else if (v.Parent == null)
                VertexList.Clear();
            else
            {
                VertexList.Add(v);
                RecPrint(u, v.Parent, VertexList);
                //Console.WriteLine(v.Vertex + " ");
            }

            return VertexList;
        }


        public List<VertexNode> FindPath(int u, int v)
        {
            BFS(u);
            var VertexList = new List<VertexNode>();
            RecPrint(adjList[u], adjList[v], VertexList);
            return VertexList;
        }
    }
}
