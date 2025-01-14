
import React, { useState } from 'react'; 
import './CalenderPage.css';


type Event = {
  id: number;
  title: string;
  date: string;
  time: string;
};

export const CalendarPage: React.FC = () => {
  const [currentWeekStart, setCurrentWeekStart] = useState<Date>(() => {
    const today = new Date();
    const startOfWeek = new Date(today);
    startOfWeek.setDate(today.getDate() - today.getDay() + 1); 
    return startOfWeek;
  });

  const getWeekDates = (start: Date): Date[] => {
    return Array.from({ length: 7 }, (_, index) => {
      const date = new Date(start);
      date.setDate(start.getDate() + index);
      return date;
    });
  };

  const weekDates = getWeekDates(currentWeekStart);

  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
  const [events, setEvents] = useState<Event[]>([
    { id: 1, title: 'Team Meeting', date: '2025-01-15', time: '11:00' },
    { id: 2, title: 'Project Demo', date: '2025-01-15', time: '14:00' },
    { id: 3, title: 'Code Review', date: '2025-01-16', time: '10:00' },
  ]);

  const goToNextWeek = () => {
    const nextWeekStart = new Date(currentWeekStart);
    nextWeekStart.setDate(currentWeekStart.getDate() + 7);
    setCurrentWeekStart(nextWeekStart);
  };

  const goToPreviousWeek = () => {
    const prevWeekStart = new Date(currentWeekStart);
    prevWeekStart.setDate(currentWeekStart.getDate() - 7);
    setCurrentWeekStart(prevWeekStart);
  };

  const handleEventSelect = (event: Event) => {
    setSelectedEvent(event);
  };

  const handleCreateEvent = () => {
    console.log('Create Event clicked');
  };

  const handleAttendEvent = () => {
    if (selectedEvent) {
      console.log(`Attending event: ${selectedEvent.title}`);
    }
  };

  const [statusMessage, setStatusMessage] = useState<string | null>(null);

  const checkUserLoggedIn = async () => {
    try {
      const response = await fetch('http://localhost:5001/api/IsUserLoggedIn', {
        credentials: 'include',
      });
      if (response.ok) {
        setStatusMessage('User is logged in.');
      } else {
        setStatusMessage('User is not logged in.');
      }
    } catch (error) {
      console.error('Error checking user login status:', error);
      setStatusMessage('Error checking user login status.');
    }
  };

  const checkAdminLoggedIn = async () => {
    try {
      const response = await fetch('http://localhost:5001/api/IsAdminLoggedIn', {
        credentials: 'include',
      });
      if (response.ok) {
        setStatusMessage('Admin is logged in.');
      } else {
        setStatusMessage('Admin is not logged in.');
      }
    } catch (error) {
      console.error('Error checking admin login status:', error);
      setStatusMessage('Error checking admin login status.');
    }
  };

  const logout = async () => {
    try {
      const response = await fetch('http://localhost:5001/api/Logout', {
        credentials: 'include',
      });
      if (response.ok) {
        setStatusMessage('Logged out successfully.');
      } else {
        setStatusMessage('Error during logout.');
      }
    } catch (error) {
      console.error('Error logging out:', error);
      setStatusMessage('Error logging out.');
    }
  };

  return (

    <div className="calendar-page">
      <header className="calendar-header">
        <button onClick={goToPreviousWeek}>&lt;=</button>
        <h2>
          {weekDates[0].toLocaleDateString('en-US', {
            day: 'numeric',
            month: 'short',
          })}{' '}
          -{' '}
          {weekDates[6].toLocaleDateString('en-US', {
            day: 'numeric',
            month: 'short',
            year: 'numeric',
          })}
        </h2>
        <button onClick={goToNextWeek}>=&gt;</button>
      </header>

      <main className="calendar-grid">
        <div className="calendar-days">
          {weekDates.map((date, index) => {
            const dateStr = date.toISOString().split('T')[0]; 
            const eventsForDay = events.filter((event) => event.date === dateStr);

            return (
              <div key={index} className="calendar-day">
                <h3>
                  {date.toLocaleDateString('en-US', {
                    weekday: 'short',
                    day: 'numeric',
                  })}
                </h3>
                <div className="calendar-events">
                  {eventsForDay.length > 0 ? (
                    eventsForDay.map((event) => (
                      <div
                        key={event.id}
                        className="calendar-event"
                        onClick={() => handleEventSelect(event)}
                      >
                        {event.title} - {event.time}
                      </div>
                    ))
                  ) : (
                    <p>No events</p>
                  )}
                </div>
              </div>
            );
          })}
        </div>

        <aside className="event-info">
          {selectedEvent ? (
            <>
              <h3>{selectedEvent.title}</h3>
              <p>
                Date: {selectedEvent.date} <br />
                Time: {selectedEvent.time}
              </p>
              <button onClick={handleAttendEvent}>Attend Event</button>
            </>
          ) : (
            <p>No event selected</p>
          )}
        </aside>
      </main>

      <footer>
        <button className="create-event-button" onClick={handleCreateEvent}>
          Create Event
        </button>
        <button className="delete-event-button" onClick={handleCreateEvent}>
          Delete Event
        </button>
      </footer>
    </div>
  );
};

const styles: { [key: string]: React.CSSProperties } = {
  container: {
    padding: '20px',
  },
  header: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: '20px',
  },
  buttonContainer: {
    display: 'flex',
    gap: '10px',
  },
  button: {
    padding: '10px 15px',
    color: '#fff',
    backgroundColor: '#007bff',
    border: 'none',
    borderRadius: '5px',
    cursor: 'pointer',
  },
  logoutButton: {
    padding: '10px 15px',
    color: '#fff',
    backgroundColor: '#dc3545',
    border: 'none',
    borderRadius: '5px',
    cursor: 'pointer',
  },
  statusMessage: {
    marginTop: '20px',
    padding: '10px',
    backgroundColor: '#f8f9fa',
    border: '1px solid #ccc',
    borderRadius: '5px',
  },
};

export default CalenderPage;
