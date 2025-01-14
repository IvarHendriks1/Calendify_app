import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./EventCreator.css";

export const EventCreator: React.FC = () => {
  const [Title, setTitle] = useState("");
  const [Description, setDescription] = useState("");
  const [EventDate, setEventDate] = useState(""); // Renamed from Date
  const [StartTime, setStartTime] = useState("");
  const [EndTime, setEndTime] = useState("");
  const [Location, setLocation] = useState("");

  const navigate = useNavigate();

  const handleClick = () => {
    navigate("/menu");
  };

  const handleCreateEvent = async () => {
    if (new Date(EventDate) < new Date()) {
      alert("Event date cannot be in the past.");
      return;
    }

    if (StartTime >= EndTime) {
      alert("End time must be after start time.");
      return;
    }

    const eventData = {
      Title,
      Description,
      Date: EventDate, // Use EventDate here
      StartTime,
      EndTime,
      Location,
      AdminApproval: true, // Hardcoded for simplicity
    };

    try {
      const response = await fetch("http://localhost:5001/api/Events", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(eventData),
      });

      if (response.ok) {
        alert("Event created successfully!");
        setTitle("");
        setDescription("");
        setEventDate(""); // Clear EventDate
        setStartTime("");
        setEndTime("");
        setLocation("");
      } else {
        const errorData = await response.json();
        console.error("Error creating event:", errorData);
        alert(`Error: ${response.statusText}`);
      }
    } catch (error) {
      console.error("Network error:", error);
      alert("Failed to create event. Please try again.");
    }
  };

  return (
    <div className="container">
      <button className="back-button" onClick={handleClick}>
        Back
      </button>
      <h1>Create Event</h1>
      <div className="form-container">
        <input
          className="input"
          type="text"
          placeholder="Title"
          value={Title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <textarea
          className="textarea"
          placeholder="Description"
          value={Description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <input
          className="input"
          placeholder="Event Date"
          type="date"
          value={EventDate} // Updated to EventDate
          onChange={(e) => setEventDate(e.target.value)} // Updated to setEventDate
        />

        <label className="label">Begin Time</label>
        <input
          className="input"
          type="time"
          value={StartTime}
          onChange={(e) => setStartTime(e.target.value)}
        />

        <label className="label">End Time</label>
        <input
          className="input"
          type="time"
          value={EndTime}
          onChange={(e) => setEndTime(e.target.value)}
        />

        <input
          className="input"
          type="text"
          placeholder="Location"
          value={Location}
          onChange={(e) => setLocation(e.target.value)}
        />
        <button className="create-button" onClick={handleCreateEvent}>
          Create Event
        </button>
      </div>
    </div>
  );
};
