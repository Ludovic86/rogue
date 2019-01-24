using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rogue.models
{
    public class PartieVM
    {
        public string NomDonjon { get; set; }
        public string NomPersonnage { get; set; }
        public int HpLeft { get; set; }
        public IEnumerable<Item> Inventaire { get; set; }
        public int NbrSalle { get; set; }
    }
}
