using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class HistoEnnemi
    {
        public int IdSalle { get; set; }
        public int IdEnnemi { get; set; }
        public int IdPartie { get; set; }

        public Ennemi IdEnnemiNavigation { get; set; }
        public Partie IdPartieNavigation { get; set; }
        public Salle IdSalleNavigation { get; set; }
    }
}
