import React, { useState, useEffect } from 'react'; 
import './CalenderPage.css';
import { useNavigate } from 'react-router-dom';


type Event = {
  id: number;
  title: string;
  description: string;
  date: string;
  startTime: string;
  endTime: string;
  EventAttendances: { userId: number; userName: string }[] | null;
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
  const [events, setEvents] = useState<Event[]>([]); 
  const navigate = useNavigate();

  const  getEvents = async () => {
    try {
      const response = await fetch('http://localhost:5001/api/Events', {
        method: 'GET',
      });

      

      if (response.ok) {
        console.log('Get events successful: ', response.statusText);
        const data = await response.json();
        console.log('Data gekregen van API:', data);
        setEvents(data);
      } else {
        alert('Unable to load events')
        console.error(response.statusText)
      }

    } catch (error) {
      alert("Er is iets misgegaan");
      console.error('Error fetching events:', error);
    }

  };

  useEffect(() => {
    getEvents();
  }, [currentWeekStart]);


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

  const handleCreateEvent = () => {//implement this
    console.log('Create Event clicked');
    navigate('/event');
  };

  const handleDeleteEvent = () => {//implement this
    console.log('Create Event clicked');
  };

  const handleAttendEvent = () => {//implement this
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

  const logout = async () => { //implement this
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
            const eventsForDay = events.filter((event) => event.date.split('T')[0] === dateStr);

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
                        {event.title} - {event.startTime.split(":")[0]+":"+event.startTime.split(":")[1]}
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
                Atendees: {selectedEvent.EventAttendances?.length == null? 0:selectedEvent.EventAttendances?.length } <br/>
                Date: {selectedEvent.date.split("T")[0]} <br />
                Time: {selectedEvent.startTime.split(":")[0]+":"+selectedEvent.startTime.split(":")[1]} - {selectedEvent.endTime.split(":")[0]+":"+selectedEvent.endTime.split(":")[1]}<br/>
                Description: {selectedEvent.description}
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
        <button className="delete-event-button" onClick={handleDeleteEvent}>
          Delete Event
        </button>
      </footer>
    </div>
  );
};

