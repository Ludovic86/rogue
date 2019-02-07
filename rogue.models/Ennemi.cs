using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class Ennemi
    {
        [Key]
        public int IdEnnemi { get; set; }
        public string NomEnnemi { get; set; }
        public int? AtkEnnemi { get; set; }
        public int? SpeedEnnemi { get; set; }
        public int? PvEnnemi { get; set; }
        public bool? Isboss { get; set; }
    }
}
