using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment.Model
{
    public class SamToSepoMapping
    {
        [Key, Column(Order = 0)]
        public int propId { get; set; }

        [Key, Column(Order = 1)]
        public int sepoPropId { get; set; }

        [ForeignKey("propId")]
        public virtual PropertyDefinition PropertyDefinition { get; set; }

        [ForeignKey("sepoPropId")]
        public virtual SepoProperty SepoProperty { get; set; }
    }
}
