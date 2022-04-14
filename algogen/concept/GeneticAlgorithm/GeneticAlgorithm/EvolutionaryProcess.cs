using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class EvolutionaryProcess
    {
        protected List<Individual> population;
        protected int generationNb = 0;
        protected IGUI program = null;
        protected string problem;

        public EvolutionaryProcess(IGUI _program, string _problem)
        {
            program = _program;
            problem = _problem;
            IndividualFactory.getInstance().Init(problem);
            population = new List<Individual>();
            for (int i = 0; i < Parameters.individualsNb; i++)
            {
                population.Add(IndividualFactory.getInstance().getIndividual(problem));
            }
        }

        private double bestFitness;
        private Individual bestInd;
        List<Individual> newGeneration;
        public void Run()
        {
            bestFitness = Parameters.minFitness + 1;
            while (generationNb < Parameters.generationsMaxNb && bestFitness > Parameters.minFitness)
            {
                EvaluatePopulation();
                UpdateBestIndAndStats();
                
                newGeneration = new List<Individual>();
                newGeneration.Add(bestInd); // élitisme
                for (int i = 0; i < Parameters.individualsNb - 1; i++)
                {
                    Reproduction();
                }

                Survival(newGeneration);
                generationNb++;
            }
        }

        private void EvaluatePopulation()
        {
            foreach (Individual ind in population)
            {
                ind.Evaluate();
            }
        }
        
        private void UpdateBestIndAndStats()
        {
            bestInd = population.OrderBy(x => x.Fitness).FirstOrDefault();
            program.PrintBestIndividual(bestInd, generationNb);
            bestFitness = bestInd.Fitness;
        }

        private void Reproduction()
        {
            bool twoParents = (Parameters.randomGenerator.NextDouble() < Parameters.crossoverRate);
            if (twoParents)
            {
                Individual father = Selection();
                Individual mother = Selection();
                newGeneration.Add(IndividualFactory.getInstance().getIndividual(problem, father, mother));
            }
            else
            {
                Individual father = Selection();
                newGeneration.Add(IndividualFactory.getInstance().getIndividual(problem, father));
            }
        }
        
        private void Survival(List<Individual> newGeneration)
        {
            // Survie déterministe : remplacement total
            population = newGeneration;
        }

        private Individual Selection()
        {
            // Roulette biaisée sur le rang
            int totalRanks = Parameters.individualsNb * (Parameters.individualsNb + 1) / 2;
            int rand = Parameters.randomGenerator.Next(totalRanks);

            int indIndex = 0;
            int nbParts = Parameters.individualsNb;
            int totalParts = 0;

            while(totalParts < rand) {
                indIndex++;
                totalParts += nbParts;
                nbParts --;
            }

            return population.OrderBy(x => x.Fitness).ElementAt(indIndex);
        }
    }
}
