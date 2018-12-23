using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class Appartient
    {
        public int IdDonjon { get; set; }
        public int IdSalle { get; set; }
        public int IdItem { get; set; }
        public int Etage { get; set; }
        public int? NumeroSalle { get; set; }

        public Donjon IdDonjonNavigation { get; set; }
        public Item IdItemNavigation { get; set; }
        public Salle IdSalleNavigation { get; set; }
    }
}
