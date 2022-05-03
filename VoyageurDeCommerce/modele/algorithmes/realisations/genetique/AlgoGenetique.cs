using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.algorithmes.realisations.genetique;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    public class AlgoGenetique:Algorithme
    {

        public override string Nom => "AlgoGénétique "+ etapes +" étapes";
        private int etapes;
        Stopwatch cronos = new Stopwatch();
        public AlgoGenetique(int etapes) : base() { this.etapes = etapes; }

        override public void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            cronos.Start();
            Generation gen = new Generation(listeLieux, listeRoute);
            gen.Randomise(); //Remplis la gen d'individus aléatoire (pour commencer quelquepart)
            for(int i = 0; i < etapes; i++) { //Fait le nombre voulu de nouvelle génération
                gen.Evolue(); //Remplace l'ancienne gen par une nouvelle composé d'un mélange des meilleurs de l'ancienne
                this.majMeilleur(gen);
            }
            this.TempsExecution = cronos.ElapsedMilliseconds;
        }
        //Met le meilleur de la gen actuel dans this.Tournee et en fait une image pour l'ihm
        private void majMeilleur(Generation gen) {
            this.Tournee = gen.Meilleur();
            this.cronos.Stop(); this.NotifyPropertyChanged("Tournee"); this.cronos.Start();
        }
    }
}
