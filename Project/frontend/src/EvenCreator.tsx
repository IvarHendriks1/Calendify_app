import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import "./EventCreator.css";

export const EventCreator: React.FC = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [date, setDate] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const [location, setLocation] = useState("");

  const navigate = useNavigate();

  const handleClick = () => {
    // Navigate to '/menu' when back is clicked
    navigate('/menu');
  };

  const handleCreateEvent = async () => {
    if (new Date(date) < new Date()) {
      alert("Event date cannot be in the past.");
      return;
    }
  
    if (startTime >= endTime) {
      alert("End time must be after start time.");
      return;
    }
    
    const eventData = {
      title,
      description,
      date,
      startTime,
      endTime,
      location,
      adminApproval: true, // Hardcoded for simplicity
    };

    try {
      const response = await fetch('http://localhost:5001/api/Events', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(eventData),
      });

      if (response.ok) {
        alert("Event created successfully!");
        // Optionally clear the form fields
        setTitle("");
        setDescription("");
        setDate("");
        setStartTime("");
        setEndTime("");
        setLocation("");
      } else {
        const errorData = await response.json();
        console.error('Error creating event:', errorData);
        alert(`Error: ${response.statusText}`);
      }
    } catch (error) {
      console.error('Network error:', error);
      alert("Failed to create event. Please try again.");
    }
  };

  return (
    <div className="container">
      <button className="back-button" onClick={handleClick}>Back</button>
      <div className="form-container">
        <input
          className="input"
          type="text"
          placeholder="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <textarea
          className="textarea"
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <input
          className="input"
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
        />
        <input
          className="input"
          type="time"
          value={startTime}
          onChange={(e) => setStartTime(e.target.value)}
        />
        <input
          className="input"
          type="time"
          value={endTime}
          onChange={(e) => setEndTime(e.target.value)}
        />
        <input
          className="input"
          type="text"
          placeholder="Location"
          value={location}
          onChange={(e) => setLocation(e.target.value)}
        />
        <button className="create-button" onClick={handleCreateEvent}>
          Create Event
        </button>
      </div>
    </div>
  );
};
