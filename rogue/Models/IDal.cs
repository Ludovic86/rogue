using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rogue.Models
{
    public interface IDal : IDisposable
    {
        Joueur TrouverJoueurParEmail(Joueur joueur);
        void AjouterJoueurDb(Joueur joueur);
        bool AuthentifierJoueur(Joueur joueur);
    }
}
