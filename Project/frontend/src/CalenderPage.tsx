import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';

export const CalenderPage: React.FC = () => {
  const location = useLocation();
  const { username } = location.state || { username: 'Guest' }; // Fallback to 'Guest' if no username is provided

  const [statusMessage, setStatusMessage] = useState<string | null>(null);

  const checkUserLoggedIn = async () => {
    try {
      const response = await fetch('http://localhost:5001/api/IsUserLoggedIn', {
        credentials: 'include',
      });
      if (response.ok) {
        setStatusMessage('User is logged in.');
      } else {
        setStatusMessage('User is not logged in.');
      }
    } catch (error) {
      console.error('Error checking user login status:', error);
      setStatusMessage('Error checking user login status.');
    }
  };

  const checkAdminLoggedIn = async () => {
    try {
      const response = await fetch('http://localhost:5001/api/IsAdminLoggedIn', {
        credentials: 'include',
      });
      if (response.ok) {
        setStatusMessage('Admin is logged in.');
      } else {
        setStatusMessage('Admin is not logged in.');
      }
    } catch (error) {
      console.error('Error checking admin login status:', error);
      setStatusMessage('Error checking admin login status.');
    }
  };

  const logout = async () => {
    try {
      const response = await fetch('http://localhost:5001/api/Logout', {
        credentials: 'include',
      });
      if (response.ok) {
        setStatusMessage('Logged out successfully.');
      } else {
        setStatusMessage('Error during logout.');
      }
    } catch (error) {
      console.error('Error logging out:', error);
      setStatusMessage('Error logging out.');
    }
  };

  return (
    <div style={styles.container}>
      <div style={styles.header}>
        <h1>Welcome, {username}!</h1>
        <div style={styles.buttonContainer}>
          <button style={styles.button} onClick={checkUserLoggedIn}>
            Check User Login
          </button>
          <button style={styles.button} onClick={checkAdminLoggedIn}>
            Check Admin Login
          </button>
          <button style={styles.logoutButton} onClick={logout}>
            Logout
          </button>
        </div>
      </div>
      {statusMessage && <div style={styles.statusMessage}>{statusMessage}</div>}
    </div>
  );
};

const styles: { [key: string]: React.CSSProperties } = {
  container: {
    padding: '20px',
  },
  header: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: '20px',
  },
  buttonContainer: {
    display: 'flex',
    gap: '10px',
  },
  button: {
    padding: '10px 15px',
    color: '#fff',
    backgroundColor: '#007bff',
    border: 'none',
    borderRadius: '5px',
    cursor: 'pointer',
  },
  logoutButton: {
    padding: '10px 15px',
    color: '#fff',
    backgroundColor: '#dc3545',
    border: 'none',
    borderRadius: '5px',
    cursor: 'pointer',
  },
  statusMessage: {
    marginTop: '20px',
    padding: '10px',
    backgroundColor: '#f8f9fa',
    border: '1px solid #ccc',
    borderRadius: '5px',
  },
};

export default CalenderPage;
