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
    class AlgoRechercheLocal : Algorithme
    {
        public override string Nom
        {
            get
            {
                if (copieur) return "RechercheLocalCopie";
                else return "RechercheLocalBase";
            }
        }
        private Stopwatch timeur = new Stopwatch();
        private bool copieur;

        public AlgoRechercheLocal(bool copieur) { this.copieur = copieur; }

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            this.timeur.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            //AlgoVoisin pour faire une tournée de base
            AlgorithmeVoisin BOB = new AlgorithmeVoisin();
            BOB.Executer(listeLieux, listeRoute);
            this.Tournee = BOB.Tournee;
            int i = 0;
            Tournee copie = new Tournee();
            copie = this.copieTo(this.Tournee, copie);
            do
            {
                if(this.copieur) copie = this.copieTo(this.Tournee, copie);
                Lieu temp = copie.ListeLieux[i];
                copie.ListeLieux[i] = copie.ListeLieux[i + 1];
                copie.ListeLieux[i + 1] = temp;
                if (copie.Distance < this.Tournee.Distance)
                {
                    this.timeur.Stop(); this.NotifyPropertyChanged("Tournee"); this.timeur.Start(); 
                    this.Tournee = this.copieTo(copie, this.Tournee);
                    i = -1;
                }
                i++;
            } while (i < copie.ListeLieux.Count - 1);
            this.timeur.Stop();
            this.NotifyPropertyChanged("Tournee");
            this.TempsExecution = timeur.ElapsedMilliseconds;
            this.timeur.Reset();
        }
        //Fait une copie de la tournée de "aCopier" dans "copie" sans créer un objet ni s'associer au même
        private Tournee copieTo(Tournee aCopier, Tournee copie){
            copie.ListeLieux.Clear();
            foreach(Lieu l in aCopier.ListeLieux) {
                copie.ListeLieux.Add(l);
            }
            return copie;
        }
    }
}
