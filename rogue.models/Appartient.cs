using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    public partial class Appartient
    {
        [Key, Column(Order = 0)]
        public int IdDonjon { get; set; }
        [Key, Column(Order = 1)]
        public int IdSalle { get; set; }
        [Key, Column(Order = 2)]
        public int IdItem { get; set; }
        public int Etage { get; set; }
        public int? NumeroSalle { get; set; }

        //public Donjon IdDonjonNavigation { get; set; }
        //public Item IdItemNavigation { get; set; }
        //public Salle IdSalleNavigation { get; set; }
    }
}
