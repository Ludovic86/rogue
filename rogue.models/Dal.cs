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

        public PartieVM ConstructPartie(Participe partie)
        {
            List<Item> items = new List<Item>();
            var donjon = bdd.Donjon.Where(d => d.IdDonjon == partie.IdDonjon).First();
            var perso = bdd.Personnage.Where(p => p.IdPersonnage == partie.IdPersonnage).First();
            var inventaire = bdd.GagnerObjet.Where(i => i.IdPartie == partie.IdPartie).ToList();
            foreach (var item in inventaire)
            {
                items.Add(bdd.Item.Where(i => i.IdItem == item.IdItem).FirstOrDefault());
            }
            var partieBuilt = new PartieVM()
            {
                NomDonjon = donjon.NomDonjon,
                NomPersonnage = perso.NomPersonnage,
                HpLeft = partie.HpLeft,
                Inventaire = items.ToArray(),
                NbrSalle = partie.NbreSalle
            };
            return partieBuilt;
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

    }
}
