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
    class AlgoInsertionProche:Algorithme
    {
        public override string Nom => "Algorithme Insertion Proche";
        
        /// <summary>
        /// Heuristique par Insertion Proche
        /// </summary>
        /// <param name="listeLieux"></param>
        /// <param name="listeRoute"></param>
        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            // variable local
            Stopwatch chrono = new Stopwatch(); // temps d'execution
            chrono.Start();
            int tailleTournee = 0; // distance entre maxD & maxA
            Lieu maxD = null; // lieux de départ le plus proche
            Lieu maxA = null; // lieux d'arrivé le plus proche
            int tempMin; // variable temporaire pour la comparaison avec min
            Lieu tempLieu; // variable temporaire pour échanger les places des sommets dans la liste
            Lieu tempLieu2=null; //


            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            // liste des lieux non visité de la tournée
            List<Lieu> NonVisite = new List<Lieu>();
            AlgoInsertion.initNV(ref NonVisite, ref listeLieux);

            // parcour les lieux, sauvegarde la distance la plus grande entre 2 points et sauvegarde ces points
            AlgoInsertion.plusGrandEcart(ref tailleTournee, ref maxD, ref maxA, ref listeLieux);
            NonVisite.Remove(maxA); // enlève les points les plus éloignés de la liste NonVisite
            NonVisite.Remove(maxD);
            this.Tournee.Add(maxA); // ajoute les points les plus éloignée à la tournée
            this.Tournee.Add(maxD);

            int minPlusProche; // détour le plus court entre les sommets et le chemin de maxD à maxA
            Lieu lieuPlusProche=null;
            // construction de la tournée
            while (NonVisite.Any() == true) // tant que la liste NonVisite n'est pas vide
            {
                minPlusProche = int.MaxValue;
                // parcour la liste NonVisite pour cherhcher le plus proche de la tournée
                for (int i=0; i<NonVisite.Count; i++)
                {
                    tempMin = AlgoInsertion.distanceLieuCouple(maxA, maxD, NonVisite[i]);
                    if (tempMin<=minPlusProche) 
                    { 
                        minPlusProche = tempMin;
                        lieuPlusProche = NonVisite[i];
                    }
                }
                NonVisite.Remove(lieuPlusProche);
                int x = 0;
                // cherche le bonne emplacement pour placer lieuPlusProche
                while (x + 1 < this.Tournee.ListeLieux.Count)
                {
                    // emplacement trouvé
                    if (AlgoInsertion.distanceLieuCouple(this.Tournee.ListeLieux[x], this.Tournee.ListeLieux[x + 1], lieuPlusProche) == minPlusProche )
                    {
                        tailleTournee += minPlusProche;
                        // place lieuPlusProche
                        tempLieu = this.Tournee.ListeLieux[x];
                        this.Tournee.ListeLieux[x] = lieuPlusProche;
                        // décale la suite de la liste
                        while (x + 1 < this.Tournee.ListeLieux.Count)
                        {
                            tempLieu2 = this.Tournee.ListeLieux[x];
                            this.Tournee.ListeLieux[x] = tempLieu;
                            tempLieu = tempLieu2;
                            x++;
                        }
                        this.Tournee.Add(tempLieu2); // ajoute le dernier temp à la fin de la liste
                    }
                    x++;
                }
                chrono.Stop();
                this.NotifyPropertyChanged("Tournee"); // prend une photo de la tournée après ajout d'un lieu
                chrono.Start();
            }
            chrono.Stop();
            this.TempsExecution = chrono.ElapsedMilliseconds;
        }
    }
}
