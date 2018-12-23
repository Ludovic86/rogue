using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class Donjon
    {
        public Donjon()
        {
            Appartient = new HashSet<Appartient>();
            Participe = new HashSet<Participe>();
        }

        public int IdDonjon { get; set; }
        public string NomDonjon { get; set; }
        public byte[] ImageDonjon { get; set; }

        public ICollection<Appartient> Appartient { get; set; }
        public ICollection<Participe> Participe { get; set; }
    }
}
