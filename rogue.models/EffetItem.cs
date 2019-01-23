using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rogue.models
{
    public partial class EffetItem
    {
        [Key]
        public int Iditem { get; set; }
        public int AtkItem { get; set; }
        public int SpeedItem { get; set; }
        public int HpItem { get; set; }
    }
}
