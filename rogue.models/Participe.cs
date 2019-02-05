using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    public partial class Participe
    {
        [Key, Column(Order = 0)]
        public int IdJoueur { get; set; }
        [Key, Column(Order = 1)]
        public int IdDonjon { get; set; }
        [Key, Column(Order = 2)]
        public int IdPersonnage { get; set; }
        [Key, Column(Order = 3)]
        public int IdPartie { get; set; }

        public int HpLeft { get; set; }
        public int NbreSalle { get; set; }
        public bool EnCours { get; set; }

        //public Donjon IdDonjonNavigation { get; set; }
        //public Joueur IdJoueurNavigation { get; set; }
        //public Partie IdPartieNavigation { get; set; }
        //public Personnage IdPersonnageNavigation { get; set; }
    }
}
