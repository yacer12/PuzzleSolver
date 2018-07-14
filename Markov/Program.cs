using Markov.Helper;
using Markov.Models.Puzzle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markov
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SolvePuzzle();
            Console.WriteLine("Please enter a Key to exit the operation");
            Console.ReadKey();
        }
        static void SolvePuzzle()
        {
            var rules = MarkovDecoder.ReadRules();
            var values = MarkovDecoder.ReadValues();

            Console.WriteLine("***********************Decoding Characters using Markov Algorithm***************");
            char[,] array = MarkovDecoder.Decypher(rules, values);
            Console.WriteLine("***********************Generating Puzzle****************************************");
            string[] wordsToCheck = MarkovDecoder.ReadWords().ToArray();

            Console.WriteLine("***********************List of possible Words: *********************************");
            foreach (var word in wordsToCheck)
            {
                Console.Write(word + " - ");
            }
            Console.WriteLine();

            List<PuzzleResponse> puzzleResponseList = new List<PuzzleResponse>();
            List<Coordinate> coordinatesToPaint = new List<Coordinate>();
            StringBuilder sBuilderFoundWords = new StringBuilder();
            foreach (var word in wordsToCheck)
            {
                var coordinatesPerWord = Puzzle.CheckAdjacentsC(word, 0, 0, array);
                PuzzleResponse puzzleResponseTemp = new PuzzleResponse();
                puzzleResponseTemp.Breakdown = new List<Breakdown>();
                //We need to check whether or not the query returned values.
                if (coordinatesPerWord.Count > 0)
                {
                    puzzleResponseTemp.Word = word;

                    coordinatesToPaint.AddRange(coordinatesPerWord);
                    foreach (var coord in coordinatesPerWord)
                    {
                        Breakdown brkDown = new Breakdown()
                        {
                            Character = array[coord.X, coord.Y],
                            Row = coord.X,
                            Column = coord.Y
                        };
                        puzzleResponseTemp.Breakdown.Add(brkDown);
                    }
                    sBuilderFoundWords.Append(word + "-");
                }
                puzzleResponseList.Add(puzzleResponseTemp);
            }
            foreach (var word in wordsToCheck)
            {
                var solution = PuzzleSolver.SolvePuzzle(array, word);
                if (solution.Word != null)
                    puzzleResponseList.Add(solution);
            }

            foreach (var puzzleSolution in puzzleResponseList)
            {
                foreach (var item in puzzleSolution.Breakdown)
                {
                    Coordinate c = new Coordinate()
                    {
                        X = item.Row,
                        Y = item.Column
                    };
                    coordinatesToPaint.Add(c);
                }
                if (!String.IsNullOrWhiteSpace(puzzleSolution.Word))
                {
                    if (!sBuilderFoundWords.ToString().Contains(puzzleSolution.Word))
                        sBuilderFoundWords.Append(puzzleSolution.Word + "-");
                }
            }

            var output = JsonConvert.SerializeObject(puzzleResponseList.Where(p => p.Word != null), Formatting.Indented);
            Console.WriteLine("JSON Output Generated: ");
            Console.WriteLine(output);

            Console.WriteLine(string.Format("WORDS FOUND IN PUZZLE: {0} ", sBuilderFoundWords.ToString()));
            Console.WriteLine();
            MarkovDecoder.PrintSolvedGrid(array, coordinatesToPaint);
            Console.WriteLine();
        }
    }
}