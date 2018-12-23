using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class Partie
    {
        public Partie()
        {
            GagnerObjet = new HashSet<GagnerObjet>();
            HistoEnnemi = new HashSet<HistoEnnemi>();
            Historique = new HashSet<Historique>();
            Participe = new HashSet<Participe>();
        }

        public int IdPartie { get; set; }

        public ICollection<GagnerObjet> GagnerObjet { get; set; }
        public ICollection<HistoEnnemi> HistoEnnemi { get; set; }
        public ICollection<Historique> Historique { get; set; }
        public ICollection<Participe> Participe { get; set; }
    }
}
