using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class Item
    {
        public Item()
        {
            Appartient = new HashSet<Appartient>();
            EstDe = new HashSet<EstDe>();
        }

        public int IdItem { get; set; }
        public string NomItem { get; set; }

        public ICollection<Appartient> Appartient { get; set; }
        public ICollection<EstDe> EstDe { get; set; }
    }
}
