using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations.genetique
{
    //Gestion d'un génération précise pour l'algoGénétique
    class Generation
    {
        private Tournee[] population = new Tournee[10]; //Population de l'algo composé d'un certains nombres d'individus (Tournee)
        public Tournee[] Population { get => population; set => population = value; }
        Random alea = new Random();
        AlgoRandom algoRandom = new AlgoRandom();
        List<Lieu> listeLieux = new List<Lieu>();
        List<Route> listeRoute = new List<Route>();

        public Generation(List<Lieu> listeLieux, List<Route> listeRoute) {
            this.listeLieux = listeLieux;
            this.listeRoute = listeRoute;
        }

        //Remplis la population d'individus aléatoire
        public void Randomise() {
            for(int i = 0; i < Population.Length; i++) {
                algoRandom.Executer(listeLieux, listeRoute);
                Population[i] = algoRandom.Tournee;
            }
        }
        //Remplace la population actuel par une nouvelle composé d'un mélange des meilleurs individus de l'ancienne
        public void Evolue() {
            List<Tournee> meilleurs = new List<Tournee>();
            meilleurs = this.Meilleurs();
            for (int i = 0; i < Population.Length; i++)
            {
                this.population[i] = this.Melange(meilleurs[alea.Next(meilleurs.Count - 1)], meilleurs[alea.Next(meilleurs.Count - 1)]);
            }
        }
        //Renvoie le meilleur individu d'une population
        public Tournee Meilleur() {
            Tournee tourneeMax = this.Population[0];
            foreach(Tournee individu in this.Population) {
                if (individu.Distance > tourneeMax.Distance) tourneeMax = individu;
            }
            return tourneeMax;
        }

        //Renvoie les 20% meilleurs individus de la population
        private List<Tournee> Meilleurs() {
            List<Tournee> meilleurs = new List<Tournee>();
            //Creation d'une copie
            List<Tournee> copiePop = population.ToList();
            //Recupération à l'aide de la copie des 20% meilleurs dans une list
            for (int i = 0; i < (Population.Length/5); i++) {
                meilleurs.Add(this.Meilleur());
                copiePop.Remove(this.Meilleur());
            }
            return meilleurs;
        }
        //Melange deux individu pour en creer un nouveau
        private Tournee Melange(Tournee t1, Tournee t2) {
            Tournee melange = new Tournee();
            melange.Add(t1.ListeLieux[0]);
            for(int i = 1;i < t1.ListeLieux.Count - 1; i++) {
                //Si les 2 sont possible, selection aléatoire
                if (!melange.ListeLieux.Contains(t1.ListeLieux[i]) && !melange.ListeLieux.Contains(t1.ListeLieux[i]))
                {
                    switch (alea.Next(2))
                    {
                        case 0: melange.Add(t1.ListeLieux[i]); break;
                        case 1: melange.Add(t2.ListeLieux[i]); break;
                    }
                }
                //Si seulement l'un des deux est possible, le prendre
                else if (!melange.ListeLieux.Contains(t1.ListeLieux[i]))
                {
                    melange.Add(t1.ListeLieux[i]);
                }
                else if (!melange.ListeLieux.Contains(t1.ListeLieux[i]))
                {
                    melange.Add(t2.ListeLieux[i]);
                }
                //Si aucun n'est possible, choisir le premier lieu possible pas encore dans la liste
                else
                {
                    bool trouve = false; int y = 0;
                    while(!trouve) {
                        if (!melange.ListeLieux.Contains(t1.ListeLieux[y])) {
                            melange.Add(t1.ListeLieux[y]);
                            trouve = true;
                        }
                        y++;
                        if (y > t1.ListeLieux.Count-1) trouve = true;
                    }
                }
            }
            return melange;
        }
    }
}
