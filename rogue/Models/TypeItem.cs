using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class TypeItem
    {
        public TypeItem()
        {
            EstDe = new HashSet<EstDe>();
        }

        public int IdType { get; set; }
        public string NomType { get; set; }

        public ICollection<EstDe> EstDe { get; set; }
    }
}
