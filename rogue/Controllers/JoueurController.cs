using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using rogue.Models;

namespace rogue.Controllers
{
    public class JoueurController : Controller
    {
        public rogueContext bdd;
        private IDal dal;

        public JoueurController(rogueContext context)
        {
            bdd = context;
            dal = new Dal(bdd);
        }

        [HttpPost]
        [Route("api/Joueur/AjouterJoueur")]
        public string AjouterJoueur([FromBody] Joueur joueur)
        {
            var joueurExistant = dal.TrouverJoueurParEmail(joueur);
            if (joueurExistant != null)
            {
                return "existe";
            }
            else
            {
                dal.AjouterJoueurDb(joueur);
                return "ok";
            }
        }

        [HttpGet]
        [Route("api/Joueur/AuthCheck")]
        public string AuthCheck()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var loggedJoueur = dal.TrouverJoueurParStringEmail(HttpContext.User.Identity.Name);
                return loggedJoueur.Email;
            }
            return null;
        }

        [HttpPost]
        [Route("api/Joueur/Authentification")]
        public async Task<string> Authentification([FromBody] Joueur joueur)
        {
            Joueur joueurLogin = new Joueur();
            joueurLogin.Email = joueur.Email;
            joueurLogin.MotDePasse = joueur.MotDePasse;
            bool ok = dal.AuthentifierJoueur(joueurLogin);

            if (ok)
            {
                var joueurOk = dal.TrouverJoueurParEmail(joueur);
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, joueurOk.Email)
                        //new Claim("Nom Joueur", joueurOk.NomJoueur),
                        //new Claim(ClaimTypes.Email, joueurOk.Email),
                    };
                var claimsIdendity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120),
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(claimsIdendity), authProperties);
                return "ok";
            }
            else
            {
                return "notOk";
            }
        }

        [HttpGet]
        [Route("api/Joueur/Unlog")]
        public async Task UnLog()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}