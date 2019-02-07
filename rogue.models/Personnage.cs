using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class Personnage
    {
        [Key]
        public int IdPersonnage { get; set; }
        public string NomPersonnage { get; set; }
        public string Classe { get; set; }
        public int? SpeedPerso { get; set; }
        public int? HpPerso { get; set; }
        public string DescriptionPerso { get; set; }
        public int? AtkPerso { get; set; }
    }
}
