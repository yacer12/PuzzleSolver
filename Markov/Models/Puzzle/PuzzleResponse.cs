using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markov.Models.Puzzle
{
    /// <summary>
    /// Class used to handle the serialized response per word.
    /// </summary>
   public class PuzzleResponse
    {
        /// <summary>
        /// Contains the value of the word.
        /// </summary>
        [JsonProperty("word")]
        public string Word { get; set; }
        /// <summary>
        /// Contains the list of each character associated to its coordinate in the 2-D Array.
        /// </summary>
        [JsonProperty("breakdown")]
        public List<Breakdown> Breakdown { get; set; }
    }
}
