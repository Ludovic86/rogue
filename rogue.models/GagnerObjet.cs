using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    public partial class GagnerObjet
    {
        [Key, Column(Order = 0)]
        public int IdPartie { get; set; }
        [Key, Column(Order = 1)]
        public int IdSalle { get; set; }
        [Key, Column(Order = 2)]
        public int IdItem { get; set; }

        //public Partie IdPartieNavigation { get; set; }
        //public Salle IdSalleNavigation { get; set; }
    }
}
