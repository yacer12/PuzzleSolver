using Markov.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markov.Models.Markov
{
    public class Neighbour
    {
        public char Character { get; set; }
        public Coordinate Position { get; set; }
        public List<Coordinate> Neighbours { get; set; }
    }
}
