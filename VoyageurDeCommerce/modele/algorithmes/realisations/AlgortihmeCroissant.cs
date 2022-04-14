using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;
using VoyageurDeCommerce.vuemodele;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgortihmeCroissant:Algorithme
    {

        public override string Nom => "Tournée croissante";
        public AlgortihmeCroissant() : base() { }

        override public void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch cronos = new Stopwatch();
            cronos.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            foreach (var v in listeLieux)
            {
                this.Tournee.Add(v);
                cronos.Stop();
                this.NotifyPropertyChanged("Tournee");
                cronos.Start();
            }
            this.TempsExecution = cronos.ElapsedMilliseconds;
            
        }
        /// <summary>Distance totale de la tournée</summary>
    }
}
