using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rogue.models
{
    public partial class Salle
    {
        //public Salle()
        //{
        //    Appartient = new HashSet<Appartient>();
        //    GagnerObjet = new HashSet<GagnerObjet>();
        //    HistoEnnemi = new HashSet<HistoEnnemi>();
        //    Historique = new HashSet<Historique>();
        //}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSalle { get; set; }
        public string NomSalle { get; set; }
        public int? TypeSalle { get; set; }
        public string TexteSalle { get; set; }

        //public ICollection<Appartient> Appartient { get; set; }
        //public ICollection<GagnerObjet> GagnerObjet { get; set; }
        //public ICollection<HistoEnnemi> HistoEnnemi { get; set; }
        //public ICollection<Historique> Historique { get; set; }
    }
}
