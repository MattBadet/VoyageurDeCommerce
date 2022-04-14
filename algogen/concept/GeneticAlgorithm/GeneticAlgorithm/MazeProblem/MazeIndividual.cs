using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    internal class MazeIndividual : Individual
    {
        public MazeIndividual()
        {
            genome = new List<IGene>();
            for (int i = 0; i < Parameters.initialGenesNb; i++)
            {
                genome.Add(new MazeGene());
            }
        }
        
        public MazeIndividual(MazeIndividual father)
        {
            this.genome = new List<IGene>();
            foreach (MazeGene g in father.genome)
            {
                this.genome.Add(new MazeGene(g));
            }
            Mutate();
        }
        
        public MazeIndividual(MazeIndividual father, MazeIndividual mother)
        {
            this.genome = new List<IGene>();
            int cuttingPoint = Parameters.randomGenerator.Next(father.genome.Count);
            foreach (MazeGene g in father.genome.Take(cuttingPoint))
            {
                this.genome.Add(new MazeGene(g));
            }
            foreach (MazeGene g in mother.genome.Skip(cuttingPoint))
            {
                this.genome.Add(new MazeGene(g));
            }
            Mutate();
        }

        internal override void Mutate()
        {
            MutateByDeletion();
            MutateByAddition();
            MutateByChangingValue();
        }

        private void MutateByAddition()
        {
            if (Parameters.randomGenerator.NextDouble() < Parameters.mutationAddRate)
            {
                genome.Add(new MazeGene());
            }
        }

        private void MutateByChangingValue()
        {
            foreach (MazeGene g in genome)
            {
                if (Parameters.randomGenerator.NextDouble() < Parameters.mutationsRate)
                {
                    g.Mutate();
                }
            }
        }

        private void MutateByDeletion()
        {
            if (Parameters.randomGenerator.NextDouble() < Parameters.mutationDeleteRate)
            {
                int geneIndex = Parameters.randomGenerator.Next(genome.Count);
                genome.RemoveAt(geneIndex);
            }
        }

        internal override double Evaluate()
        {
            fitness = Maze.Evaluate(this);
            return fitness;
        }
    }
}
