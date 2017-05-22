using System.Collections.Generic;

namespace SlackHistoryViewer.Database.Models
{
    public partial class Users
    {
        public Users(string id, string name)
        {
            Messages = new HashSet<Messages>();

            this.UserId = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
    }
}
