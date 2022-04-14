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
    class AlgorithmeVoisin:Algorithme
    {
        public override string Nom => "Tournée plus proche voisin";
        public AlgorithmeVoisin() : base() { }

        override public void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch cronos = new Stopwatch();
            cronos.Start();
            Lieu lieuEnCours = listeLieux[0];
            List<Lieu> VoisinLieu = INI_VoisinLieu(listeLieux);
            Dictionary<Lieu,int> voisin = new Dictionary<Lieu,int>();
            List<Lieu> listeLieuVisite = new List<Lieu>();
            listeLieuVisite.Add(lieuEnCours);
            VoisinLieu.Remove(lieuEnCours);

            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            Tournee.Add(lieuEnCours);
            cronos.Stop();
            this.NotifyPropertyChanged("Tournee");
            cronos.Start();
            while (listeLieux.Count() != listeLieuVisite.Count())
            {
                voisin = voisinDe(lieuEnCours, listeRoute, listeLieuVisite);
                lieuEnCours = minVoisin(lieuEnCours, voisin,VoisinLieu);
                listeLieuVisite.Add(lieuEnCours);
                VoisinLieu.Remove(lieuEnCours);
                Tournee.Add(lieuEnCours);
                cronos.Stop();
                this.NotifyPropertyChanged("Tournee");
                cronos.Start();
            }
            this.TempsExecution = cronos.ElapsedMilliseconds;
        }
        public List<Lieu> INI_VoisinLieu(List<Lieu> ListeLieu)
        {
            List<Lieu> res = new List<Lieu>();
            foreach(var i in ListeLieu)
            {
                res.Add(i);
            }
            return res;
        }
        public Dictionary<Lieu,int> voisinDe(Lieu lieuEnCours, List<Route> ListeRoute, List<Lieu> listeLieuVisite)
        {
            Dictionary<Lieu,int> res = new Dictionary<Lieu,int>();
            foreach(var i in ListeRoute)
            {
                if(i.Arrivee == lieuEnCours && !listeLieuVisite.Contains(i.Depart))
                {
                    res.Add(i.Depart, i.Distance);
                }
                else if(i.Depart == lieuEnCours && !listeLieuVisite.Contains(i.Arrivee))
                {
                    res.Add(i.Arrivee,i.Distance);
                }
            }
            return res;
        }
        public Lieu minVoisin(Lieu lieuEnCours, Dictionary<Lieu,int> voisin,List<Lieu> VoisinLieu)
        {
            int minValeur = int.MaxValue;
            Lieu res = null;
            foreach(var i in voisin)
            {
                if (i.Value < minValeur)
                {
                    minValeur = i.Value;
                    res = i.Key;
                }
            }
            if (minValeur == int.MaxValue)
            {
                int min = int.MaxValue;
                foreach(var y in VoisinLieu)
                {
                    if((FloydWarshall.Distance(lieuEnCours, y) < min)||(FloydWarshall.Distance(y, lieuEnCours) < min))
                        {
                            res = y;
                        }

                }
            }
            
            return res;
        }
        
    }
}
