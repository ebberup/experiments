using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Experiment.Model;
using System.Configuration;

namespace Experiment
{
    public class Experiment_DbContext : DbContext
    {
        public Experiment_DbContext() : base(ConfigurationManager.ConnectionStrings["Experiment_Db_Connection"].ConnectionString) { }
        public DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public DbSet<SamToSepoMapping> SamToSepoMap { get; set; }
        public DbSet<SepoProperty> SepoProperties { get; set; }
        public DbSet<Destination> Destinations { get; set; }
    }
}
