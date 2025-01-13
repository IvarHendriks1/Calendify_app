import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './DeletePopup.css';

const DeletePopup: React.FC<{ onDelete: () => void; onCancel: () => void }> = ({ onDelete, onCancel }) => {
  return (
    <div className="popup-overlay">
      <div className="popup-container">
        <div className="popup-message">Are you sure you want to delete this event?</div>
        <button className="popup-cancel" onClick={onCancel}>Cancel</button>
        <button className="popup-delete" onClick={onDelete}>Delete</button>
      </div>
    </div>
  );
};

// Export the updated component with the new name
export const DeletePopUp: React.FC = () => {
  const [showPopup, setShowPopup] = useState(false); // State to control popup visibility
  const navigate = useNavigate();

  // Function to handle the deletion action
  const handleDeleteEvent = async () => {
    try {
      
      
      // Perform the delete operation (make an API call or modify your state)
      console.log("Event has been deleted.");

      // After deletion, close the popup and redirect to the menu
      setShowPopup(false); // Hide the popup
      navigate('/menu'); // Navigate to the menu page
    } catch (error) {
      console.error('Error deleting event:', error);
      alert("Failed to delete the event.");
    }
  };

  // Function to handle cancel action (redirect to menu without deletion)
  const handleCancelDelete = () => {
    setShowPopup(false); // Hide the popup
    navigate('/menu'); // Redirect to the menu page
  };

  // Function to show the popup
  const handleShowDeletePopup = () => {
    setShowPopup(true); // Show the popup
  };

  return (
    <div className="container">
      {/* Trigger button for showing the delete confirmation popup */}
      <button className="trigger-button" onClick={handleShowDeletePopup}>
        Delete Event
      </button>

      {/* Popup will be shown if showPopup is true */}
      {showPopup && (
        <DeletePopup
          onDelete={handleDeleteEvent} // Delete item when confirmed
          onCancel={handleCancelDelete} // Cancel deletion and go back to the menu
        />
      )}
    </div>
  );
};
