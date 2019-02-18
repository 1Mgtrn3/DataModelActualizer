using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using System.IO;
using System.Diagnostics;

namespace DatabaseDetective
{
    class SchemaFetcher
    {
        public SchemaFetcher(string connString)
        {
            connectionString = connString;

        }
        public string connectionString { get; set; }
        
        public List<Link> linkList { get; set; }

        public void AddLinksToStorage() {
            //Trace.WriteLine($"ADD LINKS TO DB. StorageContext.constr={StorageContext.constr.ConnectionString}");
            try
            {
                LinkDBFormatConverter linkConverter = new LinkDBFormatConverter(linkList, format: true);
                //Trace.WriteLine($"linkConverter CREATED!!");



                using (var storage = new StorageContext(StorageContext.constr))
                {

                    foreach (var link in linkConverter.Result)
                    {
                        //Trace.WriteLine(link.ToText());
                        storage.links.AddIfNotExists(link, l => l.toText == link.toText);
                    }




                    storage.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
            


        }

        public void CreateLinkList() {
            try
            {
                var FKColumns = FetchSchema("ForeignKeyColumns").AsEnumerable();
                var FKs = FetchSchema("ForeignKeys").AsEnumerable();
                var PKs = FetchSchema("PrimaryKeys").AsEnumerable();
                var IndexColumns = FetchSchema("IndexColumns").AsEnumerable();


                var enumrblLinks = from fk in FKs
                                   join fkc in FKColumns on fk.Field<string>("FOREIGN_KEY_CONSTRAINT_NAME") equals fkc.Field<string>("CONSTRAINT_NAME")
                                   join pk in PKs on fk.Field<string>("PRIMARY_KEY_CONSTRAINT_NAME") equals pk.Field<string>("CONSTRAINT_NAME")
                                   join ic in IndexColumns on pk.Field<string>("INDEX_NAME") equals ic.Field<string>("INDEX_NAME")
                                   select new Link(fkc.Field<string>("TABLE_NAME"), fkc.Field<string>("COLUMN_NAME"), ic.Field<string>("TABLE_NAME"), ic.Field<string>("COLUMN_NAME"));

                linkList = enumrblLinks.ToList();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
           

            //Trace.WriteLine($"FK fetched!");
            

            

        }
        
        public DataTable FetchSchema(string collectionName) {
            try
            {
                using (OracleConnection connect = new OracleConnection())
                {
                    connect.ConnectionString = connectionString;
                    connect.Open();

                    tableSchema = connect.GetSchema(collectionName);

                    connect.Close();

                }
                return tableSchema;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
            
        }
        public DataTable tableSchema { get; set; }


        public List<string> GetColumns() {

            List<string> columns = new List<string>();

            foreach ( DataColumn column in tableSchema.Columns)
            {
                columns.Add($"Name: {column.ColumnName} Type: {column.DataType} Caption: {column.Caption}"); 
            }
            return columns;
        }





        public List<string> GetData() {
            List<string> foreignKeys = new List<string>();

            


            foreach (DataRow row in tableSchema.Rows)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataColumn column in tableSchema.Columns)
                {
                    sb.Append($"{row[column]}|");
                }

                foreignKeys.Add(sb.ToString());
            }


            return foreignKeys;

        }

        

        public List<string> GetConstraints() {
            List<string> constraints = new List<string>();

            foreach (var constraint in tableSchema.Constraints)
            {
                if (constraint is ForeignKeyConstraint)
                {
                    var fk = (ForeignKeyConstraint)constraint;
                    StringBuilder columns1 = new StringBuilder();
                    foreach (var column in fk.RelatedColumns)
                    {
                        columns1.Append($"{column.ColumnName},");
                    }
                    StringBuilder columns2 = new StringBuilder();
                    foreach (var column in fk.Columns)
                    {
                        columns2.Append($"{column.ColumnName},");
                    }




                    constraints.Add($"Name: {fk.ConstraintName} Table1: {fk.RelatedTable.TableName} column1: {columns1.ToString()} Table2: {fk.Table.TableName} column2: {columns2.ToString()}");
                }
            }


            return constraints;
        }
        
    }
}
