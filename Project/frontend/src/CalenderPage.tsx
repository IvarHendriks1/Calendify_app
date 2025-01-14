import React, { useState, useEffect } from 'react'; 
import './CalenderPage.css';

type Event = {
  Id: number;
  Title: string;
  Description: string;
  Date: string;
  StartTime: string;
  EndTime: string;
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


  const  getEvents = async () => {
    try {
      const response = await fetch('http://localhost:5001/api/Events', {
        method: 'GET',
      });

      

      if (response.ok) {
        console.log('Get events successful: ', response.statusText);
        const data = await response.json();
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

  const handleCreateEvent = () => {
    console.log('Create Event clicked');
  };

  const handleAttendEvent = () => {
    if (selectedEvent) {
      console.log(`Attending event: ${selectedEvent.Title}`);
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
            const eventsForDay = events.filter((event) => event.Date === dateStr);

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
                        key={event.Id}
                        className="calendar-event"
                        onClick={() => handleEventSelect(event)}
                      >
                        {event.Title} - {event.EndTime}
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
              <h3>{selectedEvent.Title}</h3>
              <p>
                Date: {selectedEvent.Date} <br />
                Time: {selectedEvent.StartTime} - {selectedEvent.EndTime}
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
