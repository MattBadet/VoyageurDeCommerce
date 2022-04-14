using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoMatemric:Algorithme
    {
        public override string Nom => "Matemric";
        private Stopwatch timeur = new Stopwatch();

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            this.timeur.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            //AlgoCroissant pour faire une tournée de base
            AlgorithmeVoisin BOB = new AlgorithmeVoisin();
            BOB.Executer(listeLieux, listeRoute);
            this.Tournee = BOB.Tournee;
            Tournee copie = new Tournee();
            for (int i = 1; i < this.Tournee.ListeLieux.Count - 5; i++) {
                for (int j = i + 2; j < this.Tournee.ListeLieux.Count - 3; j++) {
                    copie = this.CopieTo(this.Tournee, copie);
                    copie = this.InverseTo(copie,i,j);
                    if (copie.Distance < this.Tournee.Distance)
                    {
                        this.timeur.Stop(); this.NotifyPropertyChanged("Tournee"); this.timeur.Start();
                        this.Tournee = this.CopieTo(copie, this.Tournee);
                    }
                }
            }
            this.timeur.Stop();
            this.NotifyPropertyChanged("Tournee");
            this.TempsExecution = timeur.ElapsedMilliseconds;
            this.timeur.Reset();
        }
        //Fait une copie de la tournée de "aCopier" dans "copie" sans créer un objet ni s'associer au même
        private Tournee CopieTo(Tournee aCopier, Tournee copie)
        {
            copie.ListeLieux.Clear();
            foreach (Lieu l in aCopier.ListeLieux)
            {
                copie.ListeLieux.Add(l);
            }
            return copie;
        }
        //Inverse les position (i;i+1) et (j;j+1) des lieux d'une tournée
        private Tournee InverseTo(Tournee aInverser, int i, int j)
        {
            Tournee temp = new Tournee();
            temp = CopieTo(aInverser, temp);                                                            //Si tournée T = 1 2 3 4 5 6 7 8 avec 2 3 à inverser avec 6 7
            temp.ListeLieux[i] = aInverser.ListeLieux[j]; temp.ListeLieux[i+1] = aInverser.ListeLieux[j+1];           // T = 1 2 3 4 5 6 7 8 | temp = 1 6 7 3 4 5 6 7 8
            aInverser.ListeLieux[j] = aInverser.ListeLieux[i]; aInverser.ListeLieux[j+1] = aInverser.ListeLieux[i+1]; // T = 1 2 3 4 5 2 3 8 | temp = 1 6 7 3 4 5 6 7 8
            aInverser.ListeLieux[i] = temp.ListeLieux[j]; aInverser.ListeLieux[i+1] = temp.ListeLieux[j+1];           // T = 1 6 7 4 5 2 3 8 | temp = 1 6 7 3 4 5 6 7 8
            return aInverser;
        }
    }
}
