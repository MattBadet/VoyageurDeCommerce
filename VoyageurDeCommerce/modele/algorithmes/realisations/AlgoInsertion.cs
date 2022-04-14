using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    /// <summary>
    /// Class regroupent les méthodes communes à InsertionProche et InsertionLoin
    /// </summary>
    public static class AlgoInsertion
    {
        /// <summary>
        /// liste les lieux non visité de la tournée (pour l'instant tous)
        /// </summary>
        /// <param name="NonVisite"></param>
        /// <param name="listeLieux"></param>
        public static void initNV(ref List<Lieu> NonVisite, ref List<Lieu> listeLieux)
        {
            foreach (Lieu lieu in listeLieux) { NonVisite.Add(lieu); }
        }

        /// <summary>
        /// parcour les lieux, sauvegarde la distance la plus grande entre 2 points et sauvegarde ces points
        /// </summary>
        /// <param name="max"></param>
        /// <param name="maxD"></param>
        /// <param name="maxA"></param>
        /// <param name="listeLieux"></param>
        public static void plusGrandEcart(ref int max, ref Lieu maxD, ref Lieu maxA, ref List<Lieu> listeLieux)
        {
            int temp;
            foreach (Lieu lieuD in listeLieux)
            {
                foreach (Lieu lieuA in listeLieux)
                {
                    temp = FloydWarshall.Distance(lieuD, lieuA);
                    if (temp > max)
                    {
                        max = temp;
                        maxD = lieuD;
                        maxA = lieuA;
                    }
                }
            }
            max += max; // car nous faisons l'aller et le retour
        }

        /// <summary>
        /// renvoie la distance d’un lieu à un couple de lieux
        /// </summary>
        /// <param name="lieuA"></param>
        /// <param name="lieuB"></param>
        /// <param name="lieuL"></param>
        /// <returns></returns>
        public static int distanceLieuCouple(Lieu lieuA, Lieu lieuB, Lieu lieuL)
        {
            return FloydWarshall.Distance(lieuA, lieuL) + FloydWarshall.Distance(lieuL, lieuB) - FloydWarshall.Distance(lieuA, lieuB);
        }

        /// <summary>
        /// Distance d’un lieu à une tournée
        /// </summary>
        /// <param name="tournee"></param>
        /// <param name="lieuL"></param>
        /// <returns></returns>
        public static int distanceLieuTournee(List<Lieu> tournee, Lieu lieuL)
        {
            int temp;
            int res = 0;
            // pour chaque route de la tournée
            for (int i = 0; i + 1 < tournee.Count; i++)
            {
                temp = distanceLieuCouple(tournee[i], tournee[i + 1], lieuL);
                if (temp < res) { res = temp; } // prend la distance la plus courte
            }
            return res; // renvoie la distance du lieu à une tournée 
        }
    }
}
