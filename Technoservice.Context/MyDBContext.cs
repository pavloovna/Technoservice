using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technoservice.Context.Models;
using System.Data.Entity;

namespace Technoservice.Context
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base(@"Data Source=LAPTOP-667E4RFT;Initial Catalog=DB_TECHNOSERVICE;Integrated Security=True;")
        {

        }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SparesCount> SparesCounts { get; set; }
        public DbSet<SparesType> SparesTypes { get; set; }
        public DbSet<Status> Statuss { get; set; }
        public DbSet<TypeBroken> TypeBrokens { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
