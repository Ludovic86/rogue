using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rogue.models
{
    public partial class ValeurItem
    {
        [Key]
        public int IdItem { get; set; }
        public int valeurItem { get; set; }
    }
}
