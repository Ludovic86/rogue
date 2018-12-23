using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class Participe
    {
        public int IdJoueur { get; set; }
        public int IdDonjon { get; set; }
        public int IdPersonnage { get; set; }
        public int IdPartie { get; set; }

        public Donjon IdDonjonNavigation { get; set; }
        public Joueur IdJoueurNavigation { get; set; }
        public Partie IdPartieNavigation { get; set; }
        public Personnage IdPersonnageNavigation { get; set; }
    }
}
