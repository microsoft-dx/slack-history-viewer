using System;
using System.Collections.Generic;
using System.Text;

namespace RestoreFromLocal
{
    public class Channel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool? Is_channel { get; set; }
        public int? Created { get; set; }
        public string Creator { get; set; }
        public bool? Is_archived { get; set; }
        public bool? Is_general { get; set; }
        public string Name_normalized { get; set; }
        public bool? Is_member { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public Topic Topic { get; set; }
        public Purpose Purpose { get; set; }
        public IEnumerable<string> Previous_names { get; set; }
        public int? Num_members { get; set; }
    }
}
