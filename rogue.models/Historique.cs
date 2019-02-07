using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    public partial class Historique
    {
        [Key, Column(Order = 0)]
        public int IdPartie { get; set; }
        [Key, Column(Order = 1)]
        public int IdSalle { get; set; }
        public int? Ordre { get; set; }
    }
}
