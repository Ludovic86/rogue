using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class EstDe
    {
        public int IdItem { get; set; }
        public int IdType { get; set; }

        public Item IdItemNavigation { get; set; }
        public TypeItem IdTypeNavigation { get; set; }
    }
}
