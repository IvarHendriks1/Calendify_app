using System.Collections.Generic;

namespace CalendifyApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public required string First_name { get; set; }

        public required string Last_name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        // A comma-separated string that could look like this: "mo,tu,we,th,fr"
        public required string Recurring_days { get; set; }
    }
}
