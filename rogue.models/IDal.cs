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
        PartieVM CreeViewModelPartieEnCours(string email);
        void TerminePartie(string email);
        Game CreePartie();
        IEnumerable<PartieVM> GetHistoriqueParties(string email);
        void SauvegarderPartie(Game game, string email);
        Game ConstruirePartieSauvegardee(string email);

    }
}
