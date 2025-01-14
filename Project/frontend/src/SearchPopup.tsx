import React, { useState, useEffect } from 'react';

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
}

const SearchPopup: React.FC<SearchPopupProps> = ({ isOpen, onClose }) => {
  const [events, setEvents] = useState<Event[]>([]);
  const [filteredEvents, setFilteredEvents] = useState<Event[]>([]);
  const [searchQuery, setSearchQuery] = useState<string>('');

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

    if (isOpen) {
      fetchEvents(); // Fetch events only when the popup is open
    }
  }, [isOpen]);

  useEffect(() => {
    const filtered = events.filter((event) =>
      event.title.toLowerCase().includes(searchQuery.toLowerCase())
    );
    setFilteredEvents(filtered);
  }, [searchQuery, events]);

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(e.target.value);
  };

  if (!isOpen) return null; // Render nothing if the popup is not open

  return (
    <div style={styles.overlay}>
      <div style={styles.popup}>
        <h1 style={styles.header}>Search Events</h1>
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
              </div>
            ))
          ) : (
            <p>No events found.</p>
          )}
        </div>
        <button style={styles.closeButton} onClick={onClose}>
          Close
        </button>
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
  closeButton: {
    marginTop: '16px',
    padding: '10px 20px',
    backgroundColor: '#007BFF',
    color: '#fff',
    border: 'none',
    borderRadius: '4px',
    cursor: 'pointer',
  },
};

export default SearchPopup;
