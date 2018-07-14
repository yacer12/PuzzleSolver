using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markov.Models.Puzzle
{
   public class Breakdown
    {
        [JsonProperty("character")]
        public char Character { get; set; }
        [JsonProperty("row")]
        public int Row { get; set; }
        [JsonProperty("column")]
        public int Column { get; set; }
    }
}
