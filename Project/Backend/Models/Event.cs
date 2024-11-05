namespace CalendifyApp.Models;
public class Event
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public DateOnly EventDate { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public required string Location { get; set; }

    public bool Admin_approval { get; set; }

    public required List<Event_Attendance> Event_Attendances { get; set; }
}


