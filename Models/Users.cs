using System;
using System.Collections.Generic;

namespace RestoreFromLocal.Models
{
    public partial class Users
    {
        public Users()
        {
            Messages = new HashSet<Messages>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
    }
}
