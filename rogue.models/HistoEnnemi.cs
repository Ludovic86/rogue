using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    public partial class HistoEnnemi
    {
        [Key, Column(Order = 0)]
        public int IdSalle { get; set; }
        [Key, Column(Order = 1)]
        public int IdEnnemi { get; set; }
        [Key, Column(Order = 2)]
        public int IdPartie { get; set; }
    }
}
