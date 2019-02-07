using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    [Table("dbo.ITEM")]
    public partial class Item
    {
        [Key, Column("ID_ITEM")]
        public int IdItem { get; set; }
        [Column("NOM_ITEM")]
        public string NomItem { get; set; }

        public string Description { get; set; }

        public int AtkItem { get; set; }
        public int SpeedItem { get; set; }
        public int HpItem { get; set; }
    }
}
