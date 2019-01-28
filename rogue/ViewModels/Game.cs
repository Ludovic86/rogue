using rogue.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rogue.ViewModels
{
    public class Game
    {
        public Personnage Personnage { get; set; }
        public Donjon Donjon { get; set; }
        public IEnumerable<Salle> Salles { get; set; }
        public IEnumerable<Item> Objets { get; set; }
        public IEnumerable<Item> Inventaire { get; set; }
        public IEnumerable<Ennemi> Ennemis { get; set; }
    }
}
