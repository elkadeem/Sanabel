using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.DAL
{
    public class CommonSettingDataContext : DbContext
    {
        public CommonSettingDataContext() : base("CommonSettingConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<CommonSettingDataContext>(null);
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
