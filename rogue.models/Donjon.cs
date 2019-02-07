using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class Donjon
    {
        [Key]
        public int IdDonjon { get; set; }
        public string NomDonjon { get; set; }
        public byte[] ImageDonjon { get; set; }
        public string DescriptionDonjon { get; set; }
    }
}
