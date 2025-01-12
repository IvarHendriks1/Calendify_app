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

const SearchPage: React.FC = () => {
  const [events, setEvents] = useState<Event[]>([]); // All events from the backend
  const [filteredEvents, setFilteredEvents] = useState<Event[]>([]); // Filtered events
  const [searchQuery, setSearchQuery] = useState<string>(''); // User input for search

  // Fetch events from the backend when the component loads
  useEffect(() => {
    const fetchEvents = async () => {
      try {
        const response = await fetch('http://localhost:5001/api/Events'); // Replace with your backend endpoint
        if (!response.ok) {
          throw new Error(`Error: ${response.status}`);
        }
        const data: Event[] = await response.json();
        setEvents(data);
        setFilteredEvents(data); // Initialize filtered events
      } catch (error) {
        console.error('Error fetching events:', error);
      }
    };

    fetchEvents();
  }, []);

  // Filter events based on the search query
  useEffect(() => {
    const filtered = events.filter((event) =>
      event.title.toLowerCase().includes(searchQuery.toLowerCase())
    );
    setFilteredEvents(filtered);
  }, [searchQuery, events]);

  const handleSearch = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(e.target.value);
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial, sans-serif' }}>
      <h1>Search Events</h1>
      <input
        type="text"
        placeholder="Search for events"
        value={searchQuery}
        onChange={handleSearch}
        style={{
          width: '100%',
          padding: '10px',
          fontSize: '16px',
          marginBottom: '20px',
          border: '1px solid #ccc',
          borderRadius: '4px',
        }}
      />
      <div>
        {filteredEvents.length > 0 ? (
          filteredEvents.map((event) => (
            <div
              key={event.id}
              style={{
                border: '1px solid #ddd',
                padding: '10px',
                marginBottom: '10px',
                borderRadius: '4px',
              }}
            >
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
    </div>
  );
};

export default SearchPage;
