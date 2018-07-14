using Markov.Models;
using Markov.Models.Markov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markov.Helper
{
    public class Puzzle
    {
        #region Public Methods    
        public static string CheckAdjacents(string word, int starterX, int starterY, char[,] array)
        {
            char[] wordChars = word.ToCharArray();
            string response = string.Empty;
            for (int i = 0; i < wordChars.Length; i++)
            {
                var coord = GetCoordinate(wordChars[i], array);

                for (int j = 0; j < coord.Count; j++)
                {
                    var nValues = GetNeighbours(coord[j], array);
                    char[] trimmedChar = word.Remove(0, 1).ToCharArray();
                    int nextChar = i + 1;
                    if (nextChar < wordChars.Length)
                    {
                        char[] arr = GetNeighbourValues(nValues.Neighbours.ToList(), array);
                        char next = wordChars[nextChar];

                        foreach (var n in nValues.Neighbours)
                        {
                            char currentValue = array[n.X, n.Y];
                            if (currentValue == next)
                            {
                                string w = new string(trimmedChar);
                                response += coord[j].X.ToString() + " - " + coord[j].Y.ToString() + " | ";
                                Console.WriteLine(response);
                                CheckAdjacents(w, n.X, n.Y, array);
                            }
                        }
                    }
                    // We need to check for the final element of the word
                    else
                    {
                        response += coord[j].X.ToString() + " - " + coord[j].Y.ToString() + " | \n";
                        break;
                    }
                }
            }
            return response;
        }
        public static List<Coordinate> CheckAdjacentsC(string word, int starterX, int starterY, char[,] array)
        {
            char[] wordChars = word.ToCharArray();
            string response = string.Empty;
            var coord = GetCoordinate(wordChars[0], array);
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            string first = string.Empty;
     
            List<Coordinate> coordinates = new List<Coordinate>();
            bool isFirstAssigned = false;
            bool isSolved = false;
            foreach (var co in coord)
            {
               
                if (!isSolved)
                {
                    int i = co.X;
                    int j = co.Y;
                    for (int c = 0; c < wordChars.Length; c++)
                    {
                        bool isValid = false;                       

                        if (wordChars[c] == array[i, j])
                        {
                            var cdn = new Coordinate() { X = i, Y = j };
                            if (!isFirstAssigned)
                            {
                                coordinates.Add(cdn);
                                isFirstAssigned = true;
                            
                            }

                            var nValues = GetNeighbours(cdn, array);
                            string aaa = array[cdn.X, cdn.Y].ToString();
                            int nextCharIndex = c + 1;
                            if (nextCharIndex < wordChars.Length)
                            {
                                foreach (var n in nValues.Neighbours)
                                {
                                    char currentValue = array[n.X, n.Y];

                                    char next = wordChars[nextCharIndex];
                                    if (currentValue == next)
                                    {
                                        var exists = coordinates.Where(a => a.X == n.X && a.Y == n.Y);

                                        if (exists.Count() < 1)
                                        {
                                            isValid = true;
                                        
                                            i = n.X;
                                            j = n.Y;

                                            coordinates.Add(n);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (!isValid)
                        {
                            isFirstAssigned = false;
                            coordinates = new List<Coordinate>();
                            break;
                        }
                        if (IsSolved(coordinates, array, word))
                        {
                            isSolved = true;
                            break;
                        }
                    }
                
                        
                }
            }
            return coordinates;
        }
        #endregion
        #region Private Methods
        private static bool IsSolved(List<Coordinate> coords, char[,] array, string originalWord)
        {
            StringBuilder sBuilder = new StringBuilder();
            foreach (var coord in coords)
            {
                sBuilder.Append(array[coord.X, coord.Y]);
            }

            if (sBuilder.ToString().Equals(originalWord))
                return true;

            return false;
        }

        private static Neighbour GetNeighbours(Coordinate c, char[,] array)
        {
            Neighbour neighbourResponse = new Neighbour();

            List<Coordinate> coordinateList = new List<Coordinate>();
            //Expression uset to get the neighbors
            var neighbours = from x in Enumerable.Range(0, array.GetLength(0)).Where(x => Math.Abs(x - c.X) <= 1)
                             from y in Enumerable.Range(0, array.GetLength(1)).Where(y => Math.Abs(y - c.Y) <= 1)
                             select new Coordinate { X = x, Y = y };

            coordinateList = neighbours.ToList<Coordinate>();

            neighbourResponse.Character = array[c.X, c.Y];
            neighbourResponse.Position = c;
            neighbourResponse.Neighbours = coordinateList;

            return neighbourResponse;
        }
        /// <summary>
        /// Retrieves the neighbors elements given specific coordinates.
        /// </summary>
        /// <param name="coords">List of coordinates to check.</param>
        /// <param name="array">The 2D array containing the char values.</param>
        /// <returns></returns>
        private static char[] GetNeighbourValues(IEnumerable<Coordinate> coords, char[,] array)
        {
            string res = string.Empty;
            List<Neighbour> response = new List<Neighbour>();

            foreach (var item in coords)
            {
                res += array[item.X, item.Y].ToString();
            }
          
            return res.ToCharArray();
        }

        /// <summary>
        /// Gets a list of coordinates when searching a specific char in the array.
        /// </summary>
        /// <param name="value">The value to search.</param>
        /// <param name="array">The 2D array containing the char values.</param>
        /// <returns></returns>
        private static List<Coordinate> GetCoordinate(char value, char[,] array)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (value == array[i, j])
                        coordinates.Add(new Coordinate { X = i, Y = j });
                }
            }
            return coordinates;
        }
        #endregion
    }


}