using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class Personnage
    {
        public Personnage()
        {
            Participe = new HashSet<Participe>();
        }
        [Key]
        public int IdPersonnage { get; set; }
        public string NomPersonnage { get; set; }
        public string Classe { get; set; }
        public int? SpeedPerso { get; set; }
        public int? HpPeso { get; set; }
        public string DescriptionPerso { get; set; }
        public int? AtkPerso { get; set; }

        public ICollection<Participe> Participe { get; set; }
    }
}
