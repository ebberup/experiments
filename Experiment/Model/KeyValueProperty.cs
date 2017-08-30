using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.Model
{
    public class KeyValueProperty
    {
        [Key , Column(Order = 0)]
        public int id { get; set; }

        [Required, Column(Order = 1)]
        public string key { get; set; }

        [Column(Order = 2)]
        public string value { get; set; }

    }
}
