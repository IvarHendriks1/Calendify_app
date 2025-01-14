import React, { useState, useEffect, useRef } from 'react';

interface Event {
  id: number;
  title: string;
  description: string;
  date: string;
  startTime: string;
  endTime: string;
  location: string;
}

interface SearchPopupProps {
  isOpen: boolean;
  onClose: () => void;
  loggedInUserId: number;
}

interface Attendee {
  userId: number;
  userName: string;
}

const SearchPopup: React.FC<SearchPopupProps> = ({ isOpen, onClose, loggedInUserId }) => {
  const [events, setEvents] = useState<Event[]>([]);
  const [filteredEvents, setFilteredEvents] = useState<Event[]>([]);
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [attendees, setAttendees] = useState<Attendee[]>([]);
  const [popupEvent, setPopupEvent] = useState<Event | null>(null);
  const [noAttendeesPopup, setNoAttendeesPopup] = useState<string | null>(null);
  const [confirmationMessage, setConfirmationMessage] = useState<string | null>(null);
  const [attendedEvents, setAttendedEvents] = useState<number[]>([]);
  const popupRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        const response = await fetch('http://localhost:5001/api/Events');
        if (!response.ok) {
          throw new Error(`Error: ${response.status}`);
        }
        const data: Event[] = await response.json();
        setEvents(data);
        setFilteredEvents(data);
      } catch (error) {
        console.error('Error fetching events:', error);
      }
    };

    const fetchAttendedEvents = async () => {
      try {
        const response = await fetch(
          `http://localhost:5001/api/EventAttendance/user/${loggedInUserId}/attended-events`
        );
        if (!response.ok) {
          throw new Error(`Error: ${response.status}`);
        }
        const data: Event[] = await response.json();
        const attendedEventIds = data.map((event) => event.id);
        setAttendedEvents(attendedEventIds);
      } catch (error) {
        console.error('Error fetching attended events:', error);
      }
    };

    fetchEvents();
    fetchAttendedEvents();
  }, [loggedInUserId]);

  useEffect(() => {
    const filtered = events.filter((event) =>
      event.title.toLowerCase().includes(searchQuery.toLowerCase())
    );
    setFilteredEvents(filtered);
  }, [searchQuery, events]);

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(e.target.value);
  };

  const handleClickOutside = (event: MouseEvent) => {
    if (popupRef.current && !popupRef.current.contains(event.target as Node)) {
      onClose();
    }
  };

  useEffect(() => {
    if (isOpen) {
      document.addEventListener('mousedown', handleClickOutside);
    } else {
      document.removeEventListener('mousedown', handleClickOutside);
    }

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [isOpen]);

  const handleViewAttendees = async (event: Event) => {
    try {
      const response = await fetch(
        `http://localhost:5001/api/EventAttendance/attendees/${event.id}`
      );
      if (!response.ok) {
        throw new Error(`Error: ${response.status}`);
      }
      const data: Attendee[] = await response.json();
      if (data.length === 0) {
        setNoAttendeesPopup(`No attendees for "${event.title}" yet.`);
      } else {
        setAttendees(data);
        setPopupEvent(event);
      }
    } catch (error) {
      console.error('Error fetching attendees:', error);
    }
  };

  const handleAttendEvent = async (event: Event) => {
    try {
      const response = await fetch('http://localhost:5001/api/EventAttendance/attend', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ userId: loggedInUserId, eventId: event.id }),
      });
      if (!response.ok) {
        throw new Error(`Error: ${response.status}`);
      }
      setAttendedEvents([...attendedEvents, event.id]);
      setConfirmationMessage(`You have successfully attended "${event.title}".`);
    } catch (error) {
      console.error('Error attending event:', error);
    }
  };

  const handleLeaveEvent = async (event: Event) => {
    try {
      const response = await fetch('http://localhost:5001/api/EventAttendance/remove', {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ userId: loggedInUserId, eventId: event.id }),
      });
      if (!response.ok) {
        throw new Error(`Error: ${response.status}`);
      }
      setAttendedEvents(attendedEvents.filter((id) => id !== event.id));
      setConfirmationMessage(`You have successfully left "${event.title}".`);
    } catch (error) {
      console.error('Error leaving event:', error);
    }
  };

  if (!isOpen) return null;

  return (
    <div style={styles.overlay}>
      <div style={styles.popup} ref={popupRef}>
        <h1 style={styles.header}>Search Events</h1>
        <button style={styles.closeButton} onClick={onClose}>
          Close
        </button>
        <input
          type="text"
          placeholder="Search for events"
          value={searchQuery}
          onChange={handleSearch}
          style={styles.searchInput}
        />
        <div style={styles.eventList}>
          {filteredEvents.length > 0 ? (
            filteredEvents.map((event) => (
              <div key={event.id} style={styles.eventItem}>
                <h3>{event.title}</h3>
                <p>{event.description}</p>
                <p>
                  Date: {new Date(event.date).toLocaleDateString()} | Time:{' '}
                  {event.startTime} - {event.endTime}
                </p>
                <p>Location: {event.location}</p>
                <button
                  style={styles.button}
                  onClick={() => handleViewAttendees(event)}
                >
                  View Attendees
                </button>
                {attendedEvents.includes(event.id) ? (
                  <button
                    style={{ ...styles.button, backgroundColor: 'red' }}
                    onClick={() => handleLeaveEvent(event)}
                  >
                    Leave Event
                  </button>
                ) : (
                  <button
                    style={{ ...styles.button, backgroundColor: 'green' }}
                    onClick={() => handleAttendEvent(event)}
                  >
                    Attend Event
                  </button>
                )}
              </div>
            ))
          ) : (
            <p>No events found.</p>
          )}
        </div>
      </div>
    </div>
  );
};

const styles: { [key: string]: React.CSSProperties } = {
  overlay: {
    position: 'fixed',
    top: 0,
    left: 0,
    width: '100%',
    height: '100%',
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: 1000,
  },
  popup: {
    backgroundColor: '#fff',
    padding: '20px',
    borderRadius: '8px',
    width: '80%',
    maxWidth: '600px',
    maxHeight: '80%',
    overflowY: 'auto',
    position: 'relative',
  },
  header: {
    margin: '0 0 16px 0',
    fontSize: '24px',
    textAlign: 'center',
  },
  searchInput: {
    width: '100%',
    padding: '10px',
    fontSize: '16px',
    marginBottom: '16px',
    border: '1px solid #ccc',
    borderRadius: '4px',
  },
  eventList: {
    maxHeight: '400px',
    overflowY: 'auto',
  },
  eventItem: {
    borderBottom: '1px solid #ddd',
    padding: '10px 0',
  },
  button: {
    margin: '5px',
    padding: '10px 15px',
    color: '#fff',
    border: 'none',
    borderRadius: '4px',
    cursor: 'pointer',
  },
  closeButton: {
    position: 'absolute',
    top: '10px',
    right: '10px',
    padding: '5px 10px',
    backgroundColor: 'red',
    color: '#fff',
    border: 'none',
    borderRadius: '4px',
    cursor: 'pointer',
  },
};

export default SearchPopup;
