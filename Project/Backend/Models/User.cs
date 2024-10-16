namespace Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? First_name { get; set; }  // Nullable
        public string? Last_name { get; set; }
        public string? Email { get; set; }  // Nullable
        public string? Password { get; set; }
        public string? Recurring_days { get; set; }
    }

}
