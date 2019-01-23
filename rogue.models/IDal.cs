using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rogue.models
{
    public interface IDal : IDisposable
    {
        Joueur TrouverJoueurParEmail(Joueur joueur);
        void AjouterJoueurDb(Joueur joueur);
        bool AuthentifierJoueur(Joueur joueur);
        Joueur TrouverJoueurParStringEmail(string email);
    }
}
