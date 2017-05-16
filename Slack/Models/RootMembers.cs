using System.Collections.Generic;

namespace RestoreFromLocal
{
    public class RootMembers
    {
        public bool? Ok { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public int Cache_ts { get; set; }
    }
}
