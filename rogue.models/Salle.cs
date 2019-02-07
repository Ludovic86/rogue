using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    public partial class Salle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSalle { get; set; }
        public string NomSalle { get; set; }
        public int? TypeSalle { get; set; }
        public string TexteSalle { get; set; }
    }
}
