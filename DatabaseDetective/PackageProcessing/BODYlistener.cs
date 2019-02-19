using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DatabaseDetective
{
    class BODYlistener : PlSqlParserBaseListener
    {
       public List<string> relationalExprConditions { get; set; }
        public List<string> relationalOperatorConditions { get; set; }

        public int expCount { get; set; }
        public int expCountFiltered { get; set; }
        public int realConditionCount { get; set; }

        

        

        public List<Join> joinsList { get; set; }
        public BODYlistener()
        {
            joinsList = new List<Join>();

           
            relationalExprConditions = new List<string>();
            
            
        }

        

        
        public override void EnterPackage_obj_body(PlSqlParser.Package_obj_bodyContext context)
        {

            
            StringBuilder TraceString = new StringBuilder();

            TraceString.Append($"{context.ToStringTree()}");

            

            
            
            
        }
        /// <summary>
        /// Exit a parse tree produced by <see cref="PlSqlParser.select_statement"/>.
        /// <para>The default implementation does nothing.</para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        
        private Regex regex = new Regex(@"[a-zA-Z0-9_]+[.][a-zA-Z0-9_]+[=][a-zA-Z0-9_]+[.][a-zA-Z0-9_]+");

        public override void EnterRelational_expression([NotNull] PlSqlParser.Relational_expressionContext context)
        {
            string tempExpr = context.GetText();
            expCount++;
            Match match = regex.Match(tempExpr);
            if (match.Success && joinsList.Any())
            {
                joinsList.Last().links.Add(new Link(tempExpr));

               
            }

           
        }

       

        public override void EnterQuery_block([NotNull] PlSqlParser.Query_blockContext context)
        {
            
            Dictionary<string, string> aliasTable; 
           
            List<string> parseResults = new List<string>();
            
            bool anyJoins = false;
           
            var join = new Join();
           
            try
            {
                foreach (var item in context.from_clause().table_ref_list().table_ref())
                {
                    try
                    {
                        if (item.join_clause().Any())
                        {

                            anyJoins = true;


                            string firstTable = item.table_ref_aux().table_ref_aux_internal().GetText();

                            join.InitialTable = firstTable;


                            string firstAlias = item.table_ref_aux().table_alias().GetText();
                            join.Aliases.Add(firstAlias, firstTable);



                            



                            try
                            {
                                foreach (var joinItem in item.join_clause())
                                {
                                   

                                    string secondTable = joinItem.table_ref_aux().table_ref_aux_internal().GetText();

                                    
                                    string secondAlias = joinItem.table_ref_aux().table_alias().GetText();

                                    
                                    join.Aliases.Add(secondAlias, secondTable);



                                    string condition = joinItem.join_on_part().FirstOrDefault().condition().expression().GetText();

                                    join.TableCondition.Add(condition);
                                    
                                    joinsList.Add(join);
                                    Trace.WriteLine($"Added a join with {join.InitialTable}...");
                                    
                                }
                            }
                            catch (System.Exception e)
                            {


                            }
                            


                        }
                    }
                    catch (System.Exception e2)
                    {
                        

                    }



                }
            }
            catch (System.Exception e3)
            {
                Trace.WriteLine($"5: {e3.Message}");
                
            }

            
            
                

            
            


         
        }
    }
}
