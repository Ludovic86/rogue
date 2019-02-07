using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class Partie
    {
        [Key]
        public int IdPartie { get; set; }
        public bool EnCours { get; set; }
    }
}
