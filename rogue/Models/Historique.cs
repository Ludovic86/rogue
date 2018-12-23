using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class Historique
    {
        public int IdPartie { get; set; }
        public int IdSalle { get; set; }
        public int? Ordre { get; set; }

        public Partie IdPartieNavigation { get; set; }
        public Salle IdSalleNavigation { get; set; }
    }
}
