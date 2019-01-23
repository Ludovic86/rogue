using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class Ennemi
    {
        public Ennemi()
        {
            HistoEnnemi = new HashSet<HistoEnnemi>();
        }
        [Key]
        public int IdEnnemi { get; set; }
        public string NomEnemi { get; set; }
        public int? AtkEnnemi { get; set; }
        public int? SpeedEnnemi { get; set; }
        public int? PvEnnemi { get; set; }
        public bool? Isboss { get; set; }

        public ICollection<HistoEnnemi> HistoEnnemi { get; set; }
    }
}
