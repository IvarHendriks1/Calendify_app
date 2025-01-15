import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./DeletePopup.css";

export const DeletePopup: React.FC<{ onDelete: () => void; onCancel: () => void }> = ({ onDelete, onCancel }) => {
  return (
    <div className="popup-overlay">
      <div className="popup-container">
        <div className="popup-message">Are you sure you want to delete this event?</div>
        <button className="popup-cancel" onClick={onCancel}>
          Cancel
        </button>
        <button className="popup-delete" onClick={onDelete}>
          Delete
        </button>
      </div>
    </div>
  );
};

export const DeleteEvent: React.FC = () => {
  const [showPopup, setShowPopup] = useState(false);
  const [eventId, setEventId] = useState(""); // State to store the event ID
  const [isAdmin, setIsAdmin] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const checkAdminLoggedIn = async () => {
      try {
        const response = await fetch("http://localhost:5001/api/IsAdminLoggedIn", {
          credentials: "include",
        });

        if (response.ok) {
          const data = await response.json();
          if (data.isAdmin) {
            setIsAdmin(true);
          } else {
            alert("You must be logged in as an admin to access this page.");
            navigate("/");
          }
        } else {
          alert("Failed to verify admin status. Redirecting to home page.");
          navigate("/");
        }
      } catch (error) {
        console.error("Error checking admin login status:", error);
        alert("An error occurred. Redirecting to home page.");
        navigate("/");
      }
    };

    checkAdminLoggedIn();
  }, [navigate]);

  // Function to handle the deletion action
  const handleDeleteEvent = async () => {
    try {
      const response = await fetch(`http://localhost:5001/api/Events/${eventId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (response.ok) {
        alert("Event has been deleted.");
        setShowPopup(false); // Hide the popup
        setEventId(""); // Clear the event ID
        navigate("/"); // Navigate to the menu page
      } else {
        const errorData = await response.json();
        console.error("Error deleting event:", errorData);
        alert("Failed to delete the event.");
      }
    } catch (error) {
      console.error("Network error:", error);
      alert("Failed to delete the event.");
    }
  };

  // Function to handle cancel action
  const handleCancelDelete = () => {
    setShowPopup(false); // Hide the popup
  };

  // Function to show the popup
  const handleShowDeletePopup = () => {
    if (!eventId.trim()) {
      // trim ensures that it's not just whitespace
      alert("Please enter an event ID before attempting to delete.");
      return;
    }
    setShowPopup(true);
  };

  return (
    <div className="container">
      <h1>Delete Event</h1>
      <input
        className="input"
        type="text"
        placeholder="Enter Event ID"
        value={eventId}
        onChange={(e) => setEventId(e.target.value)}
      />
      <button className="trigger-button" onClick={handleShowDeletePopup}>
        Delete Event
      </button>

      {/* Popup will be shown if showPopup is true */}
      {showPopup && (
        <DeletePopup
          onDelete={handleDeleteEvent} // Delete item when confirmed
          onCancel={handleCancelDelete} // Cancel deletion
        />
      )}
    </div>
  );
};
