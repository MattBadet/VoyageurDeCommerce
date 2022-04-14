using System;

namespace GeneticAlgorithm
{
    public static class Parameters
    {
        public static int individualsNb = 20;
        public static int generationsMaxNb = 50;
        public static int initialGenesNb = 10;
        public static int minFitness = 0;

        public static double mutationsRate = 0.20;
        public static double mutationAddRate = 0.20;
        public static double mutationDeleteRate = 0.10;
        public static double crossoverRate = 0.60;

        public static Random randomGenerator = new Random();
    }
}
