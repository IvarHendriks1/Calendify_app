import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import "./EventCreator.css";

export const EventCreator: React.FC = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [date, setDate] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const navigate = useNavigate();

  const handleClick = () => {
    // Navigate to '/menu' wanneer back is geklikt maar weet nog hoe die link heet dus gaat nu gwn zo zijn
    navigate('/menu');
  };
  
  const handleCreateEvent = () => {
    // Handle event creation logic
    console.log({ title, description, date, startTime, endTime });
    alert("Event created!");
  };

  return (
    <div className="container">
      <button className="back-button">Back</button>
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
        <button className="create-button" onClick={handleCreateEvent}>
          Create Event
        </button>
      </div>
    </div>
  );
};