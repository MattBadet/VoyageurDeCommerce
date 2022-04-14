using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    struct Cell
    {
        public int i;
        public int j;

        public Cell(int _i, int _j)
        {
            i = _i;
            j = _j;
        }
    }

    public static class Maze
    {
        /*
         * Générateurs de labyrinthes :
         * http://www.desmoulins.fr/index.php?pg=divers!jeux!labyrinthes&ses=1
         * http://www.echodelta.net/mafalda/rectang/dessinlab.php4
         * 
         */

        public static String Maze1 = "*--*--*--*--*\n" +
                                     "E           |\n" +
                                     "*  *  *--*--*\n" +
                                     "|  |  |     |\n" +
                                     "*  *--*  *  *\n" +
                                     "|        |  |\n" +
                                     "*  *--*--*  *\n" +
                                     "|        |  S\n" +
                                     "*--*--*--*--*";

        public static String Maze2 = "*--*--*--*--*--*--*\n" +
                                     "E        |  |     |\n" +
                                     "*--*--*  *  *  *--*\n" +
                                     "|     |     |     |\n" +
                                     "*  *  *  *  *  *  *\n" +
                                     "|  |  |  |     |  |\n" +
                                     "*--*  *  *--*--*  *\n" +
                                     "|     |  |     |  |\n" +
                                     "*  *--*--*  *  *  *\n" +
                                     "|  |        |  |  |\n" +
                                     "*  *  *  *--*  *  *\n" +
                                     "|     |     |     S\n" +
                                     "*--*--*--*--*--*--*";

        private static List<Tuple<Cell, Cell>> paths;
        private static Cell entrance;
        private static Cell exit;

        public enum Direction { Top, Bottom, Left, Right };

        public static void Init(String s)
        {
            paths = new List<Tuple<Cell, Cell>>();

            String[] lines = s.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            int nbLines = 0;
            foreach (String line in lines)
            {
                if (nbLines % 2 != 0)
                {
                    // Ligne impaire, donc contenu d'un couloir
                    SearchForEntrance(nbLines, line);
                    SearchForExit(nbLines, line);
                    for (int column = 0; column < line.Length / 3; column++)
                    {
                        String caseStr = line.Substring(column * 3, 3);
                        if (!caseStr.Contains("|") && !caseStr.Contains("E") && !caseStr.Contains("S"))
                        {
                            paths.Add(new Tuple<Cell, Cell>(new Cell(nbLines / 2, column - 1), new Cell(nbLines / 2, column)));
                        }
                    }
                }
                else
                {
                    // Ligne paire, donc murs
                    String[] cases = line.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                    int column = 0;
                    foreach (String mur in cases) {
                        if (mur.Equals("  "))
                        {
                            paths.Add(new Tuple<Cell, Cell>(new Cell(nbLines / 2 - 1, column), new Cell(nbLines / 2, column)));
                        }
                        column++;
                    }
                }
                nbLines++;
            }
        }

        private static void SearchForEntrance(int nbLines, string line)
        {
            int index = line.IndexOf('E');
            if (index != -1)
            {
                if (index == line.Length - 1)
                {
                    index--;
                }
                entrance = new Cell(nbLines / 2, index / 3);
            }
        }

        private static void SearchForExit(int nbLines, string line)
        {
            int index = line.IndexOf('S');
            if (index != -1)
            {
                if (index == line.Length - 1)
                {
                    index--;
                }
                exit = new Cell(nbLines / 2, index / 3);
            }
        }

        private static Cell currentPosition;
        private static bool end;
        internal static double Evaluate(MazeIndividual individual)
        {
            currentPosition = entrance;
            end = false;

            foreach (MazeGene g in individual.genome)
            {
                switch (g.direction)
                {
                    case Direction.Bottom:
                        GoDown();
                        break;
                    case Direction.Top:
                        GoUp();
                        break;
                    case Direction.Right:
                        GoRight();
                        break;
                    case Direction.Left:
                        GoLeft();
                        break;
                }
                if (currentPosition.Equals(exit)) { 
                    return 0; 
                }
            }

            int distance = Math.Abs(exit.i - currentPosition.i) + Math.Abs(exit.j - currentPosition.j);
            return distance;
        }

        private static void GoLeft()
        {
            while (IsPossible(currentPosition, new Cell(currentPosition.i, currentPosition.j - 1)) && !end)
            {
                currentPosition.j--;
                end = IsJunction(currentPosition) || currentPosition.Equals(exit);
            }
            end = false;
        }

        private static void GoRight()
        {
            while (IsPossible(currentPosition, new Cell(currentPosition.i, currentPosition.j + 1)) && !end)
            {
                currentPosition.j++;
                end = IsJunction(currentPosition) || currentPosition.Equals(exit);
            }
            end = false;
        }

        private static void GoUp()
        {
            while (IsPossible(currentPosition, new Cell(currentPosition.i - 1, currentPosition.j)) && !end)
            {
                currentPosition.i--;
                end = IsJunction(currentPosition) || currentPosition.Equals(exit);
            }
            end = false;
        }

        private static void GoDown()
        {
            while (IsPossible(currentPosition, new Cell(currentPosition.i + 1, currentPosition.j)) && !end)
            {
                currentPosition.i++;
                end = IsJunction(currentPosition) || currentPosition.Equals(exit);
            }
            end = false;
        }

        private static bool IsPossible(Cell pos1, Cell pos2)
        {
            return paths.Contains(new Tuple<Cell, Cell>(pos1, pos2)) || paths.Contains(new Tuple<Cell, Cell>(pos2, pos1));
        }

        private static bool IsJunction(Cell pos)
        {
            int nbRoads = paths.Count(x => (x.Item1.Equals(pos) || x.Item2.Equals(pos)));
            return nbRoads > 2;
        }
    }
}
