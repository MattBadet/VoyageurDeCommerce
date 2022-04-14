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
    class AlgoRandom:Algorithme
    {
        public override string Nom => "Algorithme aléatoire";
        private Random rnd = new Random();

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch cronos = new Stopwatch();
            cronos.Start();
            List<Lieu> Visite = new List<Lieu>();
            Visite.Add(listeLieux[0]);
            List<Lieu> LieuAVisite = new List<Lieu>(listeLieux);
            LieuAVisite.Remove(LieuAVisite[0]);

            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            this.Tournee.Add(listeLieux[0]);
            cronos.Stop();
            this.NotifyPropertyChanged("Tournee");
            cronos.Start();

            while (LieuAVisite.Count != 0)
            {
                Lieu lieuChoisi = LieuAVisite[rnd.Next(0,LieuAVisite.Count())];
                this.Tournee.Add(lieuChoisi);
                cronos.Stop();
                this.NotifyPropertyChanged("Tournee");
                cronos.Start();
                Visite.Add(lieuChoisi);
                LieuAVisite.Remove(lieuChoisi);
            }
            this.TempsExecution = cronos.ElapsedMilliseconds;
        }
    }
}
