using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NapredniObrazec.Models;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NapredniObrazec.DataBase
{
    public class Database : IdentityDbContext<User>
    {
        public Database()
        {
            ///this.Database.EnsureCreated();
        }

        public Database(DbContextOptions<Database> options): base(options) { }

        public Microsoft.EntityFrameworkCore.DbSet<Car> Cars { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Car>().HasKey(c => c.Id);
        //    modelBuilder.Entity<User>().HasKey(u => u.Id);
        //}

        public static void Clear<T>(Microsoft.EntityFrameworkCore.DbSet<T> dbSet) where T : class  
        {
            dbSet.RemoveRange(dbSet);
        }
    }

    //public class IdentityDropCreateInitializer :
    //    DropCreateDatabaseAlways<Database>
    //{
    //    protected override void Seed(Context context)
    //    {
    //        //Seed identity tables here
    //    }
    //}
}
