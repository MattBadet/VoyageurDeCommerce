using GeneticAlgorithm;
using System;

namespace MazeProgram
{
    class Program : IGUI
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        // STATS :
        //int lastGen = 0;
        //int sumGen = 0;

        public void Run()
        {
            /*
            // Init
            Parameters.crossoverRate = 0.6;
            Parameters.mutationsRate = 0.1;
            Parameters.mutationAddRate = 0.2;
            Parameters.mutationDeleteRate = 0.1;
            Parameters.minFitness = 0;
            Parameters.generationsMaxNb = 200;

            // Lancement
            //EvolutionaryProcess geneticAlgoMaze = new EvolutionaryProcess(this, "Maze");
            //geneticAlgoMaze.Run();

            // STATS : 
            /*for (int i = 0; i < 1000; i++)
            {
                EvolutionaryProcess geneticAlgoMaze = new EvolutionaryProcess(this, "Maze");
                geneticAlgoMaze.Run();
                sumGen += lastGen; // STATS
                if (lastGen == 200) { Console.WriteLine("Not Found"); }
            }
            Console.WriteLine(sumGen / 1000.0);*/

            //Init
            Parameters.crossoverRate = 0.0;
            Parameters.mutationsRate = 0.3;
            Parameters.mutationAddRate = 0.0;
            Parameters.mutationDeleteRate = 0.0;
            Parameters.minFitness = 2579;
            Parameters.generationsMaxNb = 500;

            // Lancement
            EvolutionaryProcess geneticAlgoTSP = new EvolutionaryProcess(this, "TSP");
            geneticAlgoTSP.Run();

            // STATS : 
            /*for (int i = 0; i < 1000; i++)
            {
                EvolutionaryProcess geneticAlgoTSP = new EvolutionaryProcess(this, "TSP");
                geneticAlgoTSP.Run();
                sumGen += lastGen; // STATS
                if (lastGen == 500) { Console.WriteLine("Not Found"); }
            }
            Console.WriteLine(sumGen / 1000.0);*/

            while (true) ;
        }

        public void PrintBestIndividual(Individual individual, int generation)
        {
            //lastGen = generation; // STATS
            Console.WriteLine(generation + " -> " + individual);
        }
    }
}
