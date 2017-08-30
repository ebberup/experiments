using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.Model
{
    public enum DestinationType : int
    {
        KeyValueFile = 1
    }
    public class Destination
    {
        [Key, Column(Order = 0)]
        public int destinationId { get; set; }

        [Required, Column(Order = 1)]
        public DestinationType destinationType { get;set; }

        [Required, Column(Order = 2)]
        public string destinationPath { get; set; }


        [Required, Column(Order = 3)]
        public string destinationName { get; set; }
    }

}
