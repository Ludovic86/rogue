using rogue.models.ViewModels;
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
        PartieVM ConstructPartie(Participe partie);
        IEnumerable<Personnage> GetPersoFromDb();
        IEnumerable<Donjon> GetDonjonsFromDb();
        IEnumerable<Participe> TrouverParticipationParId(int id);
        void TerminePartie(string email);
        IEnumerable<Salle> GetSalles();
        IEnumerable<Item> GetItems();
        IEnumerable<Ennemi> GetEnnemis();
        void SauvegarderPartie(Game game, string email);

    }
}
