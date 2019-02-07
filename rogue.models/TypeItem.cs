using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rogue.models
{
    public partial class TypeItem
    {
        [Key]
        public int IdType { get; set; }
        public string NomType { get; set; }
    }
}
