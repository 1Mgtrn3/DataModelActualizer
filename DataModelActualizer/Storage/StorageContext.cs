using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelActualizer
{
    class StorageContext:DbContext
    {
       public static ConnectionStringSettings constr = ConfigurationManager.ConnectionStrings[$"objectsStorage.db"];
       
         
        //public static bool present = false;

        //public StorageContext():base(constr)
        //{
        //    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StorageContext>());

            
        //}

        //public StorageContext(string nameOrConnectionString) {
        //    Database.SetInitializer(new CreateDatabaseIfNotExists<StorageContext>());
        //}

        public StorageContext(ConnectionStringSettings connection) : base(connection.Name)
        {
            
                Database.SetInitializer(new CreateDatabaseIfNotExists<StorageContext>());
           

                
            
            
        }

      
        public DbSet<ActualDbObject> actualDbObjects { get; set; }
        public DbSet<Link> links { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
               var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<StorageContext>(modelBuilder);
            


            
            Database.SetInitializer(sqliteConnectionInitializer);
            
        }
        
    }
}
