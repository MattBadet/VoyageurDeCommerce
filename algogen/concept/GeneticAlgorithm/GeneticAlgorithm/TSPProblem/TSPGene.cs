using System;

namespace GeneticAlgorithm
{
    internal class TSPGene : IGene
    {
        City city;
        
        public TSPGene(City _city)
        {
            city = _city;
        }

        public TSPGene(TSPGene g)
        {
            city = g.city;
        }

        internal int getDistance(TSPGene g)
        {
            return TSP.getDistance(city, g.city);
        }

        public override string ToString()
        {
            return city.ToString();
        }

        public void Mutate()
        {
            throw new NotImplementedException();
        }
    }
}
