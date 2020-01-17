using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Ef31SqlServerSpatialBug
{
    [Table("FooTable")]
    public class FooTable
    {
        [Key]
        public int Id { get; set; }

        public Point Location { get; set; }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<FooTable> FooTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=.;Database=Ef31SqlServerSpatialBug;Trusted_Connection=True;";
            var connection = new SqlConnection(connectionString);
            optionsBuilder.UseSqlServer(connection, x => x.UseNetTopologySuite());
            base.OnConfiguring(optionsBuilder);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var context = new DatabaseContext();

                var fooTableEntry = new FooTable
                {
                    Id = 1,
                    Location = new Point(1, 1) { SRID = 4326 }
                };

                context.FooTable.Add(fooTableEntry);
                context.SaveChanges();

                Console.WriteLine("Hello World!");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
