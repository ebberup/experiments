using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.Model
{
    public enum ValueFormat : int
    {
        single = 0,
        csv = 1,
        file = 2
    }
    public class SepoProperty
    {
        [Key, Column(Order = 0)]
        public int sepoPropId { get; set; }

        [Column(Order = 1)]
        public int destinationId { get; set; }
        public virtual Destination Destination { get; set; }

        [Column(Order = 2)]
        public ValueFormat valueFormat { get; set; }
    }
}
