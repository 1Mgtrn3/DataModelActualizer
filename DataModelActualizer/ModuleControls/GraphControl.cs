using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataModelActualizer
{
    class GraphControl
    {
        public IDxNameConverter converter { get; set; }

        public GraphControl()
        {
            converter = new IDxNameConverter();
        }

        public int NameValidation(string name) {

            int rusultId = converter.ConvertNametoID(name);
            if (rusultId == 0)
            {
                return 0;

            }else
            {

                return rusultId;
            }
        }

        public string toJson(object o, bool Indented) {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            if (Indented)
            {
                settings.Formatting = Formatting.Indented;
            }


            return JsonConvert.SerializeObject(o, settings);


        }


        public string FindPathJson(int startId, int finishId, bool Indented) {

            
            var pathResult = FindPath(startId, finishId);
            
            return toJson(pathResult, Indented);
        }


        public string FindPathJson(string start, string finish, bool Indented) {

            var pathResult = FindPath( start,  finish);
                return toJson(pathResult, Indented);

        }



        public List<Node> FindPath(int startId, int finishId) {


            var go = new GraphOperations();

            return go.findPath(startId, finishId);

        }


        public List<Node> FindPath(string start, string finish) {
            
            int startId = NameValidation(start.ToUpper());
            int finishId = NameValidation(finish.ToUpper());


            if (startId==0)
            {
                return new List<Node> { new Node("Error - starting point does not exist!", -1) };
                
            }
            
            if (finishId==0)
            {
                return new List<Node> { new Node("Error - finish point does not exist!", -1) };
                
                
            }

            return FindPath(startId, finishId);
                

        }

        public Node CreateTree(int headId, int depth) {

            Node tree = new Node(headId, 0);
            tree.Name = this.converter.ConvertIDtoName(headId);
            tree.appendChildrenRec(depth);
            return tree;
        }


        

        public string CreateTreeJson(int headId, int depth, bool Indented)
        {

            try
            {
                var tree = CreateTree(headId, depth);

                return toJson(tree, Indented);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
        }


    }
}
