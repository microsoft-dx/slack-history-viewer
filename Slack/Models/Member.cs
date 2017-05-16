using System;
using System.Collections.Generic;
using System.Text;

namespace RestoreFromLocal
{
    public class Member
    {
        public string Id { get; set; }
        public string Team_id { get; set; }
        public string Name { get; set; }
        public bool? Deleted { get; set; }
        public Profile Profile { get; set; }
        public bool? Is_bot { get; set; }
        public int? Updated { get; set; }
        public object Status { get; set; }
        public string Color { get; set; }
        public string Real_name { get; set; }
        public string Tz { get; set; }
        public string Tz_label { get; set; }
        public int? Tz_offset { get; set; }
        public bool? Is_admin { get; set; }
        public bool? Is_owner { get; set; }
        public bool? Is_primary_owner { get; set; }
        public bool? Is_restricted { get; set; }
        public bool? Is_ultra_restricted { get; set; }
        public bool? Has_2fa { get; set; }
        public string Two_factor_type { get; set; }
    }
}
