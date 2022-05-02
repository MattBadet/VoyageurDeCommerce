using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations.genetique
{
    //Gestion d'un génération précise pour l'algoGénétique
    class Generation
    {
        private Tournee[] population = new Tournee[50]; //Population de l'algo composé d'un certains nombres d'individus (Tournee)
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
        }
        //Renvoie le meilleur individu d'une population
        public Tournee Meilleur(Tournee[] pop) {
            return null;
        }

        //Renvoie les 20% meilleurs individus de la population
        private List<Tournee> Meilleurs() {
            List<Tournee> meilleurs = new List<Tournee>();
            //Creation d'une copie
            Tournee[] copiePop = new Tournee[50];
            for (int i = 0; i < Population.Length; i++) {
                copiePop[i] = Population[i];
            }

            for (int i = 0; i < (Population.Length/5); i++) {
                
            }
            return meilleurs;
        }
        //Melange deux individu pour en creer un nouveau
        private Tournee Melange(Tournee t1, Tournee t2) {
            return null;
        }
    }
}
