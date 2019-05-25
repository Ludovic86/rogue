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

        [HttpGet]
        [Route("api/Game/GetCurrentPartie/")]
        public PartieVM GetCurrentPartie()
        {
            return dal.CreeViewModelPartieEnCours(HttpContext.User.Identity.Name);
        }

        [HttpGet]
        [Route("api/Game/CreateNewGame")]
        public Game CreateNewGame()
        {
            return dal.CreePartie();
        }

        [HttpGet]
        [Route("api/Game/GetSavedGame")]
        public Game GetSavedGame()
        {
            return dal.ConstruirePartieSauvegardee(HttpContext.User.Identity.Name);
        }


        [HttpPost]
        [Route("api/Game/SaveGame")]
        public void SaveGame([FromBody] Game game)
        {

            dal.SauvegarderPartie(game, HttpContext.User.Identity.Name);
        }

        [HttpGet]
        [Route("api/Game/HistoriqueParties")]
        public IEnumerable<PartieVM> HistoriqueParties()
        {
            return dal.GetHistoriqueParties(HttpContext.User.Identity.Name);
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
            var i = 0;
            return donjons;
        }
    }
}