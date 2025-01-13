import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import "./AlterEvent.css";

export const AlterEvent: React.FC = () => {
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

  const handleAlterEvent = async () => {
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
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(eventData),
      });

      if (response.ok) {
        alert("Event alterd successfully!");
        // Optionally clear the form fields
        setTitle("");
        setDescription("");
        setDate("");
        setStartTime("");
        setEndTime("");
        setLocation("");
      } else {
        const errorData = await response.json();
        console.error('Error altering event:', errorData);
        alert(`Error: ${response.statusText}`);
      }
    } catch (error) {
      console.error('Network error:', error);
      alert("Failed to alter event. Please try again.");
    }
  };

  return (
    <div className="container">
      <button className="back-button" onClick={handleClick}>Back</button>
      <h1>Alter Event</h1>
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
          placeholder="Event Date"
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
        />
        
        <label className="label">Begin Time</label>
        <input
          className="input"
          type="time"
          value={startTime}
          onChange={(e) => setStartTime(e.target.value)}
        />

        <label className="label">End Time</label>
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
        <button className="alter-button" onClick={handleAlterEvent}>
          Alter Event
        </button>
      </div>
    </div>
  );
};