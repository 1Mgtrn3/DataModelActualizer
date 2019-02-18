using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DatabaseDetective
{
    class GraphOperations
    {
        public GraphOperations()
        {

        }

        public  List<Node> findPath(int startItemId, int searchItemId)
        {
            
            List<Node> tmpResultList = new List<Node>();

            var tmpResult = new StringBuilder();
            using (var storage = new StorageContext(StorageContext.constr))
            {
                var objects = storage.actualDbObjects.Where(O => O.Type == "view" || O.Type == "table").ToList();
                var count = objects.Count();
                

                BreadthFirstSearch demo = new BreadthFirstSearch(count);
                


                var links = storage.links.ToList();
                foreach (var link in links)
                {
                    demo.AddV(link.id1 - 1, link.id2 - 1);
                }
                
                var path = demo.FindPath(startItemId - 1, searchItemId - 1);

                if (path.Any())
                {
                    path.Reverse();
                    tmpResult.AppendLine("Nodes:");
                    foreach (var item in path)
                    {
                        var tmpObject = objects[item.Vertex];
                        
                        
                        tmpResultList.Add(new Node(tmpObject));

                    }


                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        var tmpLink = new Link();
                        
                        tmpLink.firstTable = objects[path[i].Vertex].Name;
                        tmpLink.secondTable = objects[path[i + 1].Vertex].Name;
                        tmpLink.Format();
                        

                        tmpResultList[i].Links.AddRange( links.Where(l => l.firstTable == tmpLink.firstTable && l.secondTable == tmpLink.secondTable).Select(l=>l.toText).ToList());
                    }
                }
                else
                {

                    tmpResultList.Add(new Node("No possible way found!", -1));
                }
            }

            return tmpResultList;
        }







        public List<Node> showNeighborNodes(int headId) {
            using (var storage = new StorageContext(StorageContext.constr))
            {

                var resultNames = new List<Node>();


                List<int> neighborIds = new List<int>();

                var tmpId1 = storage.links.Where(l => l.id1 == headId).GroupBy(l => l.id2).Select(g => g.FirstOrDefault()).Select(l=>l.id2).ToList();
                
                var tmpId2 = storage.links.Where(l => l.id2 == headId).GroupBy(l => l.id1).Select(g => g.FirstOrDefault()).Select(l => l.id1).ToList();
                foreach (var id2 in tmpId1)
                {
                    resultNames.Add(new Node() { Id = id2, Links = storage.links.Where(l => l.id1 == headId & l.id2 == id2).Select(l => l.toText).ToList(), Name = storage.actualDbObjects.SingleOrDefault(o=>o.ID == id2).Name });
                }

                foreach (var id1 in tmpId2) {

                    resultNames.Add(new Node() { Id = id1, Links = storage.links.Where(l => l.id2 == headId & l.id1 == id1).Select(l => l.toText).ToList(), Name = storage.actualDbObjects.SingleOrDefault(o => o.ID == id1).Name });
                }

                return resultNames;

            }


        }



        public List<string> showNeighbors(int headId)
        {

            using (var storage = new StorageContext(StorageContext.constr))
            {
                var resultNames = new List<string>();
                foreach (var link in storage.links.Where(l => l.id1 == headId || l.id2 == headId).ToList())
                {
                    if (link.id1 == headId)
                    {
                        resultNames.Add(link.secondTable);
                    }
                    else
                    {
                        resultNames.Add(link.firstTable);
                    }
                }
                return resultNames;
                

            }
        }




    }
}
