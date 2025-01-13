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
    // Navigate to '/menu' wanneer back is geklikt maar weet nog hoe die link heet dus gaat nu gwn zo zijn
    // ik ga dit maken wanneer voorpagina is gemaakt
    navigate('/menu');
  };
  
  const handleAlterEvent = () => {
    // Handle event creation logic
    console.log({ title, description, date, startTime, endTime, location});
    alert("Event alterd!");
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