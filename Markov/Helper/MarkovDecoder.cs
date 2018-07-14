using Markov.Models.Markov;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markov.Helper
{
    class MarkovDecoder
    {
        /// <summary>
        /// This section could be reading from configuration values, in case it is required.
        /// </summary>
        ///
        #region Readonly Variables
        static readonly string _WordsFileName = "words.json";
        static readonly string _CypherFileName = "cypher.json";
        static readonly string _RulesFileName = "rules.json";
        static readonly string _ValuesFileName = "values.json";
        #endregion
        /// <summary>
        /// Uses the Markov's Algorithm for string replacement to decode a list of characters in order to generate a 2D char array
        /// Check details here: https://en.wikipedia.org/wiki/Markov_algorithm
        /// </summary>
        /// <param name="rules">List of rules used for decode the values.</param>
        /// <param name="nodes">List of Values to compare with the rules and the cypher to decode and perform the replacements.</param>
        /// <returns>2D Array of characters containing the puzzle without solution.</returns>
        public static char[,] Decypher(List<Rule> rules, List<NodeValue> nodes)
        {
            StringBuilder sBuilder = new StringBuilder();
            var cypher = ReadCypher(_CypherFileName);

            foreach (var node in nodes)
            {
                var nodeValues = node.Values;
                List<string> nodeTemp = new List<string>();
                string cypherText = GetCypherText(cypher, node.Id);
                List<Rule> rulesProcessed = new List<Rule>();
                string res = string.Empty;

                foreach (var subNode in nodeValues)
                {
                    var rule = GetRuleById(subNode.Rule, rules);

                    string ss = string.Empty;
                    if (cypherText.Contains(rule.Source))
                    {
                        ss = sBuilder.ToString();
                        cypherText = cypherText.Replace(rule.Source, rule.Replacement);

                        rulesProcessed.Add(rule);
                        if (subNode.IsTermination)
                            break;
                    }
                    sBuilder.Append(rule.Replacement);
                }
            }

            string arrayString = sBuilder.ToString();
            int length = arrayString.Length;

            return BuildGridFromString(15, 40, arrayString);            
        }
        /// <summary>
        /// Read the words from the JSON file specified.
        /// </summary>
        /// <returns>List of string containing the words obtained from the file.</returns>
        public static List<string> ReadWords()
        {
            List<string> response = new List<string>();
            try
            {
                string fileLocation = _WordsFileName;

                var rules = File.ReadAllText(fileLocation);
                var obj = JArray.Parse(rules);
                if (obj != null && obj.Count > 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {
                        response.Add(obj[i].ToString());
                    }
                }
            }
            catch (IOException ioEx)
            {
                //TODO SOME Logging
                Console.WriteLine(ioEx.Message.ToString());
            }
            catch (Exception ex)
            {
                //TODO SOME Logging
                Console.WriteLine(ex.Message.ToString());
            }
            return response;
        }
        public static List<Rule> ReadRules()
        {
            List<Rule> response = new List<Rule>();
            try
            {
                string fileLocation = _RulesFileName;

                var rules = File.ReadAllText(fileLocation);
                var obj = JArray.Parse(rules);
                if (obj != null && obj.Count > 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {
                        response.Add(new Rule
                        {
                            Id = i,
                            Replacement = obj[i]["replacement"].ToString(),
                            Source = obj[i]["source"].ToString(),
                        });
                    }
                }
            }
            catch (IOException ioEx)
            {
                //TODO SOME Logging
                Console.WriteLine(ioEx.Message.ToString());
            }
            catch (Exception ex)
            {
                //TODO SOME Logging
                Console.WriteLine(ex.Message.ToString());
            }
            return response;
        }
        /// <summary>
        /// Read Node values from a file.
        /// </summary>
        /// <param name="fileName">The name of the file containing the node values.</param>
        /// <returns>List of node already parsed.</returns>
        public static List<NodeValue> ReadValues()
        {
            List<NodeValue> response = new List<NodeValue>();
            try
            {
                string fileLocation = _ValuesFileName;

                var rules = File.ReadAllText(fileLocation);
                var obj = JArray.Parse(rules);
                if (obj != null && obj.Count > 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {
                        NodeValue node = new NodeValue();
                        node.Id = i;

                        List<Value> values = new List<Value>();
                        for (int j = 0; j < obj[i].Count(); j++)
                        {
                            // Console.WriteLine(obj[i][j]);
                            values.Add(new Value
                            {
                                Order = Convert.ToInt32(obj[i][j]["order"]),
                                Rule = Convert.ToInt32(obj[i][j]["rule"].ToString()),
                                IsTermination = Convert.ToBoolean(obj[i][j]["isTermination"].ToString())
                            });
                        }
                        node.Values = values.OrderBy(n => n.Order).ToList();
                        response.Add(node);
                    }
                }
            }
            catch (IOException ioEx)
            {
                //TODO SOME Logging
                Console.WriteLine(ioEx.Message.ToString());
            }
            catch (Exception ex)
            {
                //TODO SOME Logging
                Console.WriteLine(ex.Message.ToString());
            }
            return response;
        }
        /// <summary>
        /// Print in the console the solution of a word search puzle 2D array.
        /// </summary>
        /// <param name="matrix">The array to print</param>
        /// <param name="coordinates">The coordinates to highlight and show in different color to indicate the findings.</param>
        public static void PrintSolvedGrid(char[,] matrix, List<Coordinate> coordinates)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    var cTemp = coordinates.Where(c => c.X == i && c.Y == j);
                    if (cTemp.Count() > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write(string.Format("{0} ", matrix[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
        }
        /// <summary>
        /// Print a 2D Array.
        /// </summary>
        /// <param name="matrix">The 2D array going to be processed.</param>
        public static void PrintGrid(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(string.Format("{0} ", matrix[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
        }
        #region Private Methods
        /// <summary>
        /// Generates a 2D Array from a string value.
        /// </summary>
        /// <param name="rows">Number of rows for the grid.</param>
        /// <param name="cols">Number of columns for the grid.</param>
        /// <param name="stringValue">The string value to be parsed.</param>
        /// <returns></returns>
        private static char[,] BuildGridFromString(int rows, int cols, string stringValue)
        {
            IEnumerable<string> chunks = Split(stringValue, cols);
            char[,] wordMatrix = new char[rows, cols];

            List<char[]> charList = new List<char[]>();
            foreach (var item in chunks)
            {
                char[] chars = item.ToCharArray();
                charList.Add(chars);
            }

            for (int i = 0; i < charList.Count; i++)
            {
                char[] tempChar = charList[i];
                for (int j = 0; j < tempChar.Length; j++)
                {
                    wordMatrix[i, j] = tempChar[j];
                }
            }
            return wordMatrix;
        }
       

        /// <summary>
        /// Split a string into chunks for 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        private static IEnumerable<string> Split(string str, int chunkSize)
        {
            IEnumerable<string> response = new List<string>();
            if (!String.IsNullOrWhiteSpace(str))
                response = Enumerable.Range(0, str.Length / chunkSize)
                    .Select(i => str.Substring(i * chunkSize, chunkSize));
            return response;
        }

        /// <summary>
        /// Get the text of a cypher based on its ID.
        /// </summary>
        /// <param name="cypher">Cypher list of the values.</param>
        /// <param name="id">Id of the cypher to review.</param>
        /// <returns></returns>
        private static string GetCypherText(List<Cypher> cypher, int id)
        {
            return cypher.Where(c => c.Id == id).FirstOrDefault().CypherText;
        }
        /// <summary>
        /// Read cypher values from a file.
        /// </summary>      
        /// <returns>List of cypher already parsed.</returns>
        private static List<Cypher> ReadCypher(string fileName)
        {
            List<Cypher> response = new List<Cypher>();
            try
            {
                string fileLocation = fileName;

                var rules = File.ReadAllText(fileLocation);
                var obj = JArray.Parse(rules);
                if (obj != null && obj.Count > 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {
                        response.Add(new Cypher
                        {
                            Id = i,
                            CypherText = obj[i].ToString(),
                        });
                    }
                }
            }
            catch (IOException ioEx)
            {
                //TODO SOME Logging
                Console.WriteLine(ioEx.Message.ToString());
            }
            catch (Exception ex)
            {
                //TODO SOME Logging
                Console.WriteLine(ex.Message.ToString());
            }
            return response;
        }
        /// <summary>
        /// Read rule values from a file.
        /// </summary>
        /// <param name="fileName">The name of the file containing the rules values.</param>
        /// <returns>List of rules already parsed.</returns>
     
        /// <summary>
        /// Gets a rule given an ID.
        /// </summary>
        /// <param name="id">Id of the rule to check.</param>
        /// <param name="rules">Collection with the rule values to verify.</param>
        /// <returns>The rule in case it exists.</returns>
        private static Rule GetRuleById(int id, List<Rule> rules)
        {
            return rules.Where(r => r.Id == id).FirstOrDefault();
        }
        #endregion
    }
}
