using System.Collections.Generic;

public class DetailedEventDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Location { get; set; }
    public bool AdminApproval { get; set; }

    public List<EventAttendanceDTO> EventAttendances { get; set; }
}
