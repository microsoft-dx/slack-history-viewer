using System.Collections.Generic;
using System.Text;

namespace RestoreFromLocal
{
    public class RootMessages
    {
        public bool? Ok { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public bool? Has_more { get; set; }
        public bool? Is_limited { get; set; }
    }
}
