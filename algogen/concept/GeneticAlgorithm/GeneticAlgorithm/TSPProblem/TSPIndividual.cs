using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    internal class TSPIndividual : Individual
    {
        public TSPIndividual()
        {
            genome = new List<IGene>();
            List<City> cities = TSP.getCities();
            while (cities.Count != 0)
            {
                int index = Parameters.randomGenerator.Next(cities.Count);
                genome.Add(new TSPGene(cities.ElementAt(index)));
                cities.RemoveAt(index);
            }
        }

        public TSPIndividual(TSPIndividual father)
        {
            this.genome = new List<IGene>();
            foreach (TSPGene g in father.genome)
            {
                this.genome.Add(new TSPGene(g));
            }
            Mutate();
        }

        public TSPIndividual(TSPIndividual father, TSPIndividual mother)
        {
            this.genome = new List<IGene>();
            int cuttingPoint = Parameters.randomGenerator.Next(father.genome.Count);
            foreach (TSPGene g in father.genome.Take(cuttingPoint))
            {
                this.genome.Add(new TSPGene(g));
            }
            foreach (TSPGene g in mother.genome)
            {
                if (!genome.Contains(g))
                {
                    this.genome.Add(new TSPGene(g));
                }
            }
            Mutate();
        }

        internal override void Mutate()
        {
            if (Parameters.randomGenerator.NextDouble() < Parameters.mutationsRate)
            {
                int index1 = Parameters.randomGenerator.Next(genome.Count);
                TSPGene g = (TSPGene)genome.ElementAt(index1);
                genome.RemoveAt(index1);
                int index2 = Parameters.randomGenerator.Next(genome.Count);
                genome.Insert(index2, g);
            }
        }

        internal override double Evaluate()
        {
            int totalKm = 0;
            TSPGene oldGene = null;
            foreach (TSPGene g in genome)
            {
                if (oldGene != null)
                {
                    totalKm += g.getDistance(oldGene);
                }
                oldGene = g;
            }
            totalKm += oldGene.getDistance((TSPGene)genome.FirstOrDefault());
            fitness = totalKm;
            return fitness;
        }
    }
}
