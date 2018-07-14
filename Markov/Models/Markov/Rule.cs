using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markov.Models.Markov
{
    class Rule
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Replacement { get; set; }
    }
}
