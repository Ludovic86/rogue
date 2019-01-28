using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class Donjon
    {
        public Donjon()
        {
            Appartient = new HashSet<Appartient>();
            Participe = new HashSet<Participe>();
        }
        [Key]
        public int IdDonjon { get; set; }
        public string NomDonjon { get; set; }
        public byte[] ImageDonjon { get; set; }
        public string DescriptionDonjon { get; set; }

        public ICollection<Appartient> Appartient { get; set; }
        public ICollection<Participe> Participe { get; set; }
    }
}
