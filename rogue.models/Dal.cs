using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rogue.models
{
    public class Dal : IDal
    {
        private rogueContext bdd;

        public Dal (rogueContext context)
        {
            bdd = context;
        }

        public string Encode(string motDePasse)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(motDePasse);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String mdpHash = System.Text.Encoding.ASCII.GetString(data);
            return mdpHash;
        }

        public Joueur TrouverJoueurParEmail(Joueur joueur)
        {
            var joueurTrouve = bdd.Joueur.Where(j => j.Email == joueur.Email).FirstOrDefault();
            if (joueurTrouve != null)
            {
                return joueurTrouve;
            }
            return null;
        }

        public Joueur TrouverJoueurParStringEmail(string email)
        {
            var joueurTrouve = bdd.Joueur.Where(j => j.Email == email).FirstOrDefault();
            return joueurTrouve;
        }

        public bool AuthentifierJoueur(Joueur joueur)
        {
            var joueurTrouve = bdd.Joueur.Where(j => j.Email == joueur.Email && j.MotDePasse == this.Encode(joueur.MotDePasse)).FirstOrDefault();
            if (joueurTrouve != null)
                return true;
            return false;
        }

        public void AjouterJoueurDb(Joueur joueur)
        {
            string mdpSalt = Encode(joueur.MotDePasse);
            joueur.MotDePasse = mdpSalt;
            int id = bdd.Joueur.Count();
            joueur.IdJoueur = id++;
            bdd.Joueur.Add(joueur);
            bdd.SaveChanges();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

    }
}
