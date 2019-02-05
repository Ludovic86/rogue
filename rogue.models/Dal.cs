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

        public Game CreePartie()
        {
            return new Game()
            {
                Salles = GetSalles(),
                Objets = GetItems(),
                Ennemis = GetEnnemis()
            };
        }

        public PartieVM CreeViewModelPartieEnCours(string email)
        {
            var joueur = TrouverJoueurParStringEmail(email);
            var participation = TrouverPartieEnCours(joueur.IdJoueur);
            if (participation != null)
            {
                return ConstructPartie(participation);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Personnage> GetPersoFromDb()
        {
            return bdd.Personnage.ToArray();
        }

        private Personnage TrouverPersonnageParId(int id)
        {
            return bdd.Personnage.Where(p => p.IdPersonnage == id).FirstOrDefault();
        }

        public IEnumerable<Donjon> GetDonjonsFromDb()
        {
            return bdd.Donjon.ToArray();
        }

        private Donjon TrouverDonjonParId(int id)
        {
            return bdd.Donjon.Where(d => d.IdDonjon == id).FirstOrDefault();
        }

        private List<Salle> GetSalles()
        {
            return bdd.Salle.ToList();
        }

        private List<Item> GetItems()
        {
            return bdd.Item.ToList();
        }

        private IEnumerable<Ennemi> GetEnnemis()
        {
            return bdd.Ennemi.ToArray();
        }

        private IEnumerable<Participe> TrouverParticipationsParId(int id)
        {
            var participation = bdd.Participe.Where(p => p.IdJoueur == id);
            return participation;
        }

        private List<Participe> TrouverPartiesTerminees(int id)
        {
            var participations = bdd.Participe.Where(p => p.IdJoueur == id && p.EnCours == false);
            return participations.ToList();
        }

        public Participe TrouverPartieEnCours(int id)
        {
            return bdd.Participe.Where(p => p.IdJoueur == id && p.EnCours == true).FirstOrDefault();
        }

        public List<Historique> TrouverHistoriqueSalles(int id)
        {
            return bdd.Historique.Where(h => h.IdPartie == id).ToList();
        }

        private List<Salle> TrouverSallesPartie(List<Historique> historiqueSalle)
        {
            var salles = new List<Salle>();
            foreach(var histo in historiqueSalle)
            {
                salles.Add(bdd.Salle.Where(s => s.IdSalle == histo.IdSalle).FirstOrDefault());
            }
            return salles;
        }

        public List<GagnerObjet> TrouverGagnerObjet(int id)
        {
            return bdd.GagnerObjet.Where(g => g.IdPartie == id).ToList();
        }

        private List<Item> TrouverItemsPartie(List<GagnerObjet> histoObjets)
        {
            var items = new List<Item>();
            foreach(var histo in histoObjets)
            {
                items.Add(bdd.Item.Where(i => i.IdItem == histo.IdItem).FirstOrDefault());
            }
            return items;
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

        public IEnumerable<PartieVM> GetHistoriqueParties(string email)
        {
            var parties = new List<PartieVM>();

            var joueur = TrouverJoueurParStringEmail(email);
            var participations = TrouverPartiesTerminees(joueur.IdJoueur);
            foreach (var participation in participations)
            {
                List<Item> items = new List<Item>();
                var donjon = bdd.Donjon.Where(d => d.IdDonjon == participation.IdDonjon).FirstOrDefault();
                var personnage = bdd.Personnage.Where(p => p.IdPersonnage == participation.IdPersonnage).FirstOrDefault();
                var inventaire = bdd.GagnerObjet.Where(g => g.IdPartie == participation.IdPartie);
                foreach (var item in inventaire)
                {
                    items.Add(bdd.Item.Where(i => i.IdItem == item.IdItem).FirstOrDefault());
                }
                var partieJouee = new PartieVM()
                {
                    NomDonjon = donjon.NomDonjon,
                    NomPersonnage = personnage.NomPersonnage,
                    HpLeft = participation.HpLeft,
                    Inventaire = items.ToArray(),
                    NbrSalle = participation.NbreSalle
                };
                parties.Add(partieJouee);
            }
            return parties.ToArray();
        }

        public Game ConstruirePartieSauvegardee(string email)
        {
            var gameSauvegardee = new Game();
           
            var joueur = TrouverJoueurParStringEmail(email);
            var participation = TrouverPartieEnCours(joueur.IdJoueur);
            var personnage = TrouverPersonnageParId(participation.IdPersonnage);
            var donjon = TrouverDonjonParId(participation.IdDonjon);
            var historiqueSalles = TrouverHistoriqueSalles(participation.IdPartie);
            var sallesFull = GetSalles();
            var sallesDone = TrouverSallesPartie(historiqueSalles);

            foreach (var salle in sallesDone)
            {
                sallesFull.Remove(salle);
            }
            var histoObjets = TrouverGagnerObjet(participation.IdPartie);
            var itemFull = GetItems();
            var itemObtenus = TrouverItemsPartie(histoObjets);

            foreach (var item in itemObtenus)
            {
                itemFull.Remove(item);
            }
            gameSauvegardee.Donjon = donjon;
            gameSauvegardee.Personnage = personnage;
            gameSauvegardee.Inventaire = itemObtenus;
            gameSauvegardee.Objets = itemFull;
            gameSauvegardee.NbreSalle = participation.NbreSalle;
            gameSauvegardee.HpLeft = participation.HpLeft;
            gameSauvegardee.Salles = sallesFull;
            gameSauvegardee.SallesParcourues = sallesDone;
            gameSauvegardee.Ennemis = GetEnnemis();
            return gameSauvegardee;
        }

        public void TerminePartie(string email)
        {
            var joueurTrouve = this.TrouverJoueurParStringEmail(email);
            var participation = this.TrouverParticipationsParId(joueurTrouve.IdJoueur);

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
                var itemsEnDb = TrouverGagnerObjet(participation.IdPartie);
                var itemsEnInventaire = new List<GagnerObjet>();
                var salles = TrouverHistoriqueSalles(participation.IdPartie);
                var listSalles = new List<Historique>();
                participation.HpLeft = game.HpLeft;
                participation.NbreSalle = game.NbreSalle;
                if (game.Inventaire != null)
                {
                    foreach (var item in game.Inventaire)
                    {
                        itemsEnInventaire.Add(new GagnerObjet() { IdItem = item.IdItem, IdPartie = participation.IdPartie });
                    }

                    foreach (var itemInventaire in itemsEnInventaire)
                    {
                        if (itemsEnDb.Any())
                        {
                            foreach (var itemDb in itemsEnDb)
                            {
                                if (itemInventaire.IdItem != itemDb.IdItem)
                                {
                                    bdd.GagnerObjet.Add(new GagnerObjet { IdItem = itemInventaire.IdItem, IdPartie = participation.IdPartie });
                                }
                            }
                        }
                        else
                        {
                            bdd.GagnerObjet.Add(new GagnerObjet { IdItem = itemInventaire.IdItem, IdPartie = participation.IdPartie });
                        } 
                    }
                }
                bdd.SaveChanges();
                foreach (var salle in game.SallesParcourues)
                {
                    salles.Add(new Historique() { IdPartie = participation.IdPartie, IdSalle = salle.IdSalle });
                }
                foreach (var salle in salles)
                {
                    var salleExistante = salles.Find(s => s.IdSalle == salle.IdSalle);
                    if (salleExistante == null)
                    {
                        bdd.Historique.Add(salle);
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
