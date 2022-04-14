using System.Collections.Generic;
using VoyageurDeCommerce.exception.realisations;
using VoyageurDeCommerce.modele.algorithmes.realisations;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes
{
    /// <summary> Fabrique des algorithmes </summary>
    public class FabriqueAlgorithme
    {
        /// <summary>
        /// Méthode de fabrication
        /// </summary>
        /// <param name="type">Type de l'algorithme à construire</param>
        /// <param name="listeLieux">Liste des lieux</param>
        /// <returns>L'algorithme créé</returns>
        public static Algorithme Creer(TypeAlgorithme type)
        {
            Algorithme algo;
            switch (type)
            {
                case TypeAlgorithme.ALGOEXEMPLE: algo = new AlgoExemple(); break;
                case TypeAlgorithme.CROISSANT: algo = new AlgortihmeCroissant(); break;
                case TypeAlgorithme.VOISIN: algo = new AlgorithmeVoisin(); break;
                case TypeAlgorithme.RECHERCHELOCALCOPIE: algo = new AlgoRechercheLocal(true); break;
                case TypeAlgorithme.RECHERCHELOCAL: algo = new AlgoRechercheLocal(false); break;
                case TypeAlgorithme.INSERTIONCOURT: algo = new AlgoInsertionProche();break;
                case TypeAlgorithme.INSERTIONLONG: algo = new AlgoInsertionLoin();break;
                case TypeAlgorithme.RANDOM: algo = new AlgoRandom();break; 

                default: throw new ExceptionAlgorithme("Vous n'avez pas modifié la fabrique des algorithmes !");
            }

            return algo;
        }
    }
}
