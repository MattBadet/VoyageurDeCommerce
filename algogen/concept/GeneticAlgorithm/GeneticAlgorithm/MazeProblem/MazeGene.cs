namespace GeneticAlgorithm
{
    internal class MazeGene : IGene
    {
        public Maze.Direction direction;

        public MazeGene()
        {
            direction = (Maze.Direction)Parameters.randomGenerator.Next(4);
        }

        public MazeGene(MazeGene g)
        {
            direction = g.direction;
        }

        public override string ToString()
        {
            return direction.ToString().Substring(0,1);
        }

        public void Mutate()
        {
            direction = (Maze.Direction) Parameters.randomGenerator.Next(4);
        }
    }
}
