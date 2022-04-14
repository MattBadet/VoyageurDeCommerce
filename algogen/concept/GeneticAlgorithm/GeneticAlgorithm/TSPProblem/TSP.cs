using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    internal struct City
    {
        String name;

        public City(string p)
        {
            name = p;
        }

        public override string ToString()
        {
            return name;
        }
    }

    public static class TSP
    {
        static List<City> cities;
        static int[][] distances;

        public static void Init()
        {
            cities = new List<City>()
            {
                new City("Paris"),
                new City("Lyon"),
                new City("Marseille"),
                new City("Nantes"),
                new City("Bordeaux"),
                new City("Toulouse"),
                new City("Lille")
            };

            distances = new int[cities.Count][];

            distances[0] = new int[] { 0, 462, 772, 379, 546, 678, 215 }; // Paris
            distances[1] = new int[] { 462, 0, 326, 598, 842, 506, 664 }; // Lyon
            distances[2] = new int[] { 772, 326, 0, 909, 555, 407, 1005 }; // Marseille
            distances[3] = new int[] { 379, 598, 909, 0, 338, 540, 584 }; // Nantes
            distances[4] = new int[] { 546, 842, 555, 338, 0, 250, 792 }; // Bordeaux
            distances[5] = new int[] { 678, 506, 407, 540, 250, 0, 926 }; // Toulouse
            distances[6] = new int[] { 215, 664, 1005, 584, 792, 926, 0 }; // Lille
        }

        internal static int getDistance(City _city1, City _city2)
        {
            return distances[cities.IndexOf(_city1)][cities.IndexOf(_city2)];
        }

        internal static List<City> getCities() {
            List<City> listCities = new List<City>();
            listCities.AddRange(cities);
            return listCities;
        }
    }
}
