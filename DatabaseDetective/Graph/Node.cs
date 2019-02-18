using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDetective
{
    //public class Neighbor {
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public string link { get; set; }
    //    public Neighbor(int Id, string Name)
    //    {
    //        id = Id;
    //        name = Name;
    //        link = "";
    //    }
    //    public Neighbor()
    //    {

    //    }
    //    public Neighbor(int Id, string Name, string Link)
    //    {
    //        id = Id;
    //        name = Name;
    //        link = Link;
    //    }

    //}

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

        public Node(string name, int id, string link) {
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



        // method name
        //public decimal Time; // time spent in method

        public void appendChildrenNodes() {

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

        //public void appendChildren()
        //{
        //    var GraphOps = new GraphOperations();
        //    foreach (var neighbor in GraphOps.showNeighborsIDs(Id))
        //    {
        //        if (neighbor.id != Parent)
        //        {
        //            Node Child = new Node(neighbor.id, Id);
        //            Child.Name = neighbor.name;
        //            Child.Links.Add(neighbor.link);
        //            Child.ParentName = Name;
        //            Children.Add(Child);
        //        }

        //    }

        //}


        //public void appendChildren( bool duplicates)
        //{
        //    var GraphOps = new GraphOperations();

        //    if (duplicates)
        //    {
        //        foreach (var neighbor in GraphOps.showNeighborsIDs(Id))
        //        {
        //            if (neighbor.id != Parent)
        //            {
        //                Node Child = new Node(neighbor.id, Id);
        //                Child.Name = neighbor.name;
        //                Child.Link = neighbor.link;
        //                Children.Add(Child);
        //            }

        //        }
        //    }
        //    else
        //    {
        //        foreach (var neighbor in GraphOps.showNeighborsIDsNoDuplicates(Id))
        //        {
        //            if (neighbor.id != Parent)
        //            {
        //                Node Child = new Node(neighbor.id, Id);
        //                Child.Name = neighbor.name;
        //                Child.Link = "";
        //                Children.Add(Child);
        //            }

        //        }

        //    }
            

        //}

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


        //public void appendChildrenRec(int Depth, bool duplicates)
        //{

            
        //        if (Depth != 0)
        //        {
        //            appendChildren(duplicates);
        //            foreach (var child in Children)
        //            {
        //                child.appendChildrenRec(Depth - 1, duplicates);
        //            }
        //        }
           

        //}

        //public StringBuilder outputResult { get; set; }

        //public void PrintPretty(string indent, bool last, ref StringBuilder outPutResult)
        //{
        //    outPutResult.Append(indent);
        //    //Console.Write(indent);
        //    if (last)
        //    {
        //        outPutResult.Append("\\--");
        //        //Console.Write("\\--");
        //        indent += "  ";
        //    }
        //    else
        //    {
        //        outPutResult.Append("|-");
        //        //Console.Write("|-");
        //        indent += "| ";
        //    }
        //    //Console.WriteLine($"{Name}\r\n");
        //    if (Link != "")
        //    {
        //        outPutResult.AppendLine($"{Name} : {Link}");
        //        //Console.WriteLine($"{Name} : {Link}");
        //    }
        //    else
        //    {
        //        outPutResult.AppendLine(Name);
        //        //Console.WriteLine(Name);
        //    }

            


        //    for (int i = 0; i < Children.Count; i++)
        //        Children[i].PrintPretty(indent, i == Children.Count - 1, ref outPutResult);
        //}

    }

    

   //public class TreeOutput
   // {
   //    public string outputResult { get; set; }
   //     public TreeOutput(int headId,  int depth, bool duplicates)
   //     {
   //         Node tree = new Node(headId, 0);

   //         tree.appendChildrenRec(depth, duplicates);
   //         var sbOutput = new StringBuilder();
   //         tree.PrintPretty("", true, ref sbOutput);
   //         outputResult = sbOutput.ToString();



   //     }

       


   // }


}
