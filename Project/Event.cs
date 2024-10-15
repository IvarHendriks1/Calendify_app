public class Event
{
    public int Id { get; set; }
    public string? Title { get; set; }  // Nullable
    public string? Description { get; set; }  // Nullable
    public DateTime EventDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }  // Nullable
}
