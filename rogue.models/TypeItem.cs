using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class TypeItem
    {
        public TypeItem()
        {
            EstDe = new HashSet<EstDe>();
        }
        [Key]
        public int IdType { get; set; }
        public string NomType { get; set; }

        public ICollection<EstDe> EstDe { get; set; }
    }
}
