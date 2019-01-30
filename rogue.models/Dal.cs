using rogue.models.ViewModels;
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

        public IEnumerable<Personnage> GetPersoFromDb()
        {
            return bdd.Personnage.ToArray();
        }

        public IEnumerable<Donjon> GetDonjonsFromDb()
        {
            return bdd.Donjon.ToArray();
        }

        public IEnumerable<Salle> GetSalles()
        {
            return bdd.Salle.ToArray();
        }

        public IEnumerable<Item> GetItems()
        {
            return bdd.Item.ToArray();
        }

        public IEnumerable<Ennemi> GetEnnemis()
        {
            return bdd.Ennemi.ToArray();
        }

        public IEnumerable<Participe> TrouverParticipationParId(int id)
        {
            var participation = bdd.Participe.Where(p => p.IdJoueur == id);
            return participation;
        }

        public Participe TrouverPartieEnCours(int id)
        {
            return bdd.Participe.Where(p => p.IdJoueur == id && p.EnCours == true).FirstOrDefault();
        }

        public IEnumerable<GagnerObjet> TrouverGagnerObjet(int id)
        {
            return bdd.GagnerObjet.Where(g => g.IdPartie == id);
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

        public void TerminePartie(string email)
        {
            var joueurTrouve = this.TrouverJoueurParStringEmail(email);
            var participation = this.TrouverParticipationParId(joueurTrouve.IdJoueur);

            if (participation.Any())
            {
                foreach (var partie in participation)
                {
                    if (partie.EnCours)
                    {
                        partie.EnCours = false;                        
                    }
                }
            }
            bdd.SaveChanges();
        }

        public void SauvegarderPartie(Game game, string email)
        {
            var joueur = TrouverJoueurParStringEmail(email);
            var participation = TrouverPartieEnCours(joueur.IdJoueur);
            if (participation != null)
            {
                var items = TrouverGagnerObjet(participation.IdPartie);
                participation.HpLeft = game.HpLeft;
                participation.NbreSalle = game.NbreSalle;
                if (game.Inventaire != null)
                {
                    if (items.Count() == 0)
                    {
                        
                    }
                }
                bdd.SaveChanges();
                return;
            }
            var partie = new Partie() { IdPartie = bdd.Partie.Count() + 1 };
            bdd.Partie.Add(partie);
            bdd.SaveChanges();
            bdd.Participe.Add(new Participe()
            {
                IdJoueur = joueur.IdJoueur,
                IdDonjon = game.Donjon.IdDonjon,
                IdPersonnage = game.Personnage.IdPersonnage,
                IdPartie = partie.IdPartie,
                HpLeft = game.HpLeft,
                NbreSalle = game.NbreSalle,
                EnCours = true
            });
            if (game.Inventaire != null)
            {
                foreach (var item in game.Inventaire)
                {
                    bdd.GagnerObjet.Add(new GagnerObjet { IdItem = item.IdItem, IdPartie = partie.IdPartie });
                }
            }
            bdd.SaveChanges();
            return;
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

    }
}
