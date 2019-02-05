using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    public partial class EstDe
    {
        [Key, Column(Order = 0)]
        public int IdItem { get; set; }
        [Key, Column(Order = 1)]
        public int IdType { get; set; }

        //public Item IdItemNavigation { get; set; }
        //public TypeItem IdTypeNavigation { get; set; }
    }
}
