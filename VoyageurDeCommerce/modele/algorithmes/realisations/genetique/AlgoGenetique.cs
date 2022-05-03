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

        public override string Nom => "AlgoGénétique"+ duree/1000 +"s";
        private int duree;
        Stopwatch cronos = new Stopwatch();
        public AlgoGenetique(int duree) : base() { this.duree = duree; }

        override public void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            cronos.Start();
            Generation gen = new Generation(listeLieux, listeRoute);
            gen.Randomise(); //Remplis la gen d'individus aléatoire (pour commencer quelquepart)
            while(cronos.ElapsedMilliseconds < duree) { //tant que le tant d'execution prévue n'est pas demander
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
