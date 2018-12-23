using System;
using System.Collections.Generic;

namespace rogue.Models
{
    public partial class Joueur
    {
        public Joueur()
        {
            Participe = new HashSet<Participe>();
        }

        public int IdJoueur { get; set; }
        public string NomJoueur { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }

        public ICollection<Participe> Participe { get; set; }
    }
}
