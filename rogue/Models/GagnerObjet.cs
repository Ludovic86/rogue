using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class GagnerObjet
    {
        public int IdPartie { get; set; }
        public int IdSalle { get; set; }
        public int IdItem { get; set; }

        public Partie IdPartieNavigation { get; set; }
        public Salle IdSalleNavigation { get; set; }
    }
}
