using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//ADDED
using System.Data.Entity;
using VehicleProject.Models;

namespace MVCVechicleLoanProject.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() : base("VehicleDBConString")
        {
            Database.SetInitializer(new VehicleDBInitializer());
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }

    public class VehicleDBInitializer : DropCreateDatabaseIfModelChanges<AppDBContext>
    {
        protected override void Seed(AppDBContext context)
        {
            var admins = new List<Admin> {
                new Admin { UserName = "Admin", Passsword = "Admin123" },
            };
            admins.ForEach(s => context.Admins.Add(s));
            context.SaveChanges();
        }
    }
}