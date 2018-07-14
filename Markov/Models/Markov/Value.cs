using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markov.Models.Markov
{
    class Value
    {
        public int Order { get; set; }
        public int Rule { get; set; }
        public bool IsTermination { get; set; }
    }
}
