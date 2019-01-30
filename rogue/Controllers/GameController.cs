using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rogue.models;
using rogue.models.ViewModels;

namespace rogue.Controllers
{
    public class GameController : Controller
    {

        public rogueContext bdd;
        private IDal dal;

        public GameController(rogueContext context)
        {
            bdd = context;
            dal = new Dal(bdd);
        }

        [HttpPost]
        [Route("api/Game/GetCurrentPartie/")]
        public PartieVM GetCurrentPartie([FromBody]JoueurVM joueur)
        {
            var partieVM = new PartieVM();
            var joueurTrouve = dal.TrouverJoueurParStringEmail(joueur.Email);
            var participation = bdd.Participe.Where(p => p.IdJoueur == joueurTrouve.IdJoueur);

            if (participation.Any())
            {
                foreach (var partie in participation)
                {
                    if (partie.EnCours)
                    {
                        partieVM = dal.ConstructPartie(partie);
                        return partieVM;
                    }
                }
            }
            return null;
        }

        [HttpGet]
        [Route("api/Game/CreateNewGame")]
        public Game CreateNewGame()
        {
            return new Game()
            {
                Salles = dal.GetSalles(),
                Objets = dal.GetItems(),
                Ennemis = dal.GetEnnemis()
            };
        }

        [HttpPost]
        [Route("api/Game/SaveGame")]
        public void SaveGame([FromBody] Game game)
        {

            dal.SauvegarderPartie(game, HttpContext.User.Identity.Name);
        }

        [HttpPost]
        [Route("api/Game/TerminerPartie")]
        public void TerminerPartie([FromBody] JoueurVM joueur)
        {
            dal.TerminePartie(joueur.Email);
        }

        [HttpGet]
        [Route("api/Game/GetPersonnages")]
        public IEnumerable<Personnage> GetPersonnages()
        {
            var personnages = dal.GetPersoFromDb();
            return personnages;
        }

        [HttpGet]
        [Route("api/Game/GetDonjons")]
        public IEnumerable<Donjon> GetDonjons()
        {
            var donjons = dal.GetDonjonsFromDb();
            return donjons;
        }
    }
}