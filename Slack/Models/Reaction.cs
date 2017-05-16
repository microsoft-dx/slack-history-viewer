using System;
using System.Collections.Generic;
using System.Text;

namespace RestoreFromLocal
{
    public class Reaction
    {
        public string Name { get; set; }
        public IEnumerable<string> Users { get; set; }
        public int? Count { get; set; }
    }
}
