using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDetective
{


    public class Node
    {
        public string Name { get; set; }
        public string ParentName { get; set; }

        public int Parent { get; set; }

        public int Id { get; set; }
        //public string Link { get; set; }
        public List<string> Links { get; set; }


        public List<Node> Children { get; set; }

        public Node()
        {
            Children = new List<Node>();
            Links = new List<string>();
        }

        public Node(string name, int id, string link)
        {
            Name = name;
            Id = id;
            //Link = link;
            Children = new List<Node>();
            Links = new List<string>();
            Links.Add(link);
        }

        public Node(string name, int parentId)
        {
            Name = name;
            Parent = parentId;
            Children = new List<Node>();
            Links = new List<string>();
            //outputResult = new StringBuilder();
        }

        public Node(int id, int parentId)
        {
            Id = id;
            Parent = parentId;
            Children = new List<Node>();
            Links = new List<string>();
            //outputResult = new StringBuilder();
        }

        public Node(ActualDbObject dbObject)
        {
            Id = dbObject.ID;
            Parent = -1;
            Name = dbObject.Name;
            Links = new List<string>();

        }





        public void appendChildrenNodes()
        {

            var GraphOps = new GraphOperations();

            foreach (var cNode in GraphOps.showNeighborNodes(Id))
            {
                if (cNode.Id != Parent)
                {
                    cNode.Parent = Id;
                    cNode.ParentName = Name;
                    Children.Add(cNode);

                }
            }


        }



        public void appendChildrenRec(int Depth)
        {


            if (Depth != 0)
            {
                appendChildrenNodes();
                foreach (var child in Children)
                {
                    child.appendChildrenRec(Depth - 1);
                }
            }


        }





    }
}
