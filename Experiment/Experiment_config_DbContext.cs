using Experiment.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    public class Experiment_config_DbContext: DbContext
    {
        public Experiment_config_DbContext() : base(ConfigurationManager.ConnectionStrings["Experiment_config_Db_Connection"].ConnectionString) { }
        public DbSet<KeyValueProperty> KeyValueProperties { get; set; }
    }
}
