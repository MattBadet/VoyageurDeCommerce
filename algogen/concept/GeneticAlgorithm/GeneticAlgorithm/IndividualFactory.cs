using System;

namespace GeneticAlgorithm
{
    internal class IndividualFactory
    {
        private static IndividualFactory instance;

        private IndividualFactory() { }

        public static IndividualFactory getInstance()
        {
            if (instance == null)
            {
                instance = new IndividualFactory();
            }
            return instance;
        }

        public Individual getIndividual(String type) {
            Individual ind = null;
            switch (type)
            {
                case "Maze" :
                    ind = new MazeIndividual();
                    break;
                case "TSP":
                    ind = new TSPIndividual();
                    break;
            }
            return ind;
        }

        public Individual getIndividual(String type, Individual father)
        {
            Individual ind = null;
            switch (type)
            {
                case "Maze" :
                    ind = new MazeIndividual((MazeIndividual) father);
                    break;
                case "TSP":
                    ind = new TSPIndividual((TSPIndividual)father);
                    break;
            }
            return ind;
        }

        public Individual getIndividual(String type, Individual father, Individual mother)
        {
            Individual ind = null;
            switch (type)
            {
                case "Maze" :
                    ind = new MazeIndividual((MazeIndividual)father, (MazeIndividual) mother);
                    break;
                case "TSP":
                    ind = new TSPIndividual((TSPIndividual)father, (TSPIndividual)mother);
                    break;
            }
            return ind;
        }

        internal void Init(string type)
        {
            switch (type)
            {
                case "Maze":
                    Maze.Init(Maze.Maze2);
                    break;
                case "TSP":
                    TSP.Init();
                    break;
            }
        }
    }
}
