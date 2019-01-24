using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rogue.models;

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
        [Route("api/Game/GetCurrentPartie")]
        public PartieVM GetCurrentPartie(string email)
        {
            var partieVM = new PartieVM();
            var joueur = dal.TrouverJoueurParStringEmail(email);
            var participation = bdd.Participe.Where(p => p.IdJoueur == joueur.IdJoueur);

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
    }
}