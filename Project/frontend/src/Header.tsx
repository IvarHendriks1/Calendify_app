import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

const Header: React.FC = () => {
  const [role, setRole] = useState<string | null>(null);

  useEffect(() => {
    // Check if admin is logged in
    fetch('/api/IsAdminLoggedIn')
      .then((response) => {
        if (response.ok) {
          setRole('admin');
        }
      })
      .catch(() => {
        // Check if user is logged in if admin check fails
        fetch('/api/IsUserLoggedIn')
          .then((response) => {
            if (response.ok) {
              setRole('user');
            }
          })
          .catch(() => setRole(null)); // Not logged in
      });
  }, []);

  return (
    <header
      style={{
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        padding: '10px 20px',
        backgroundColor: '#f0f0f0',
        borderBottom: '1px solid #ddd',
      }}
    >
      {/* Left Section */}
      <div style={{ display: 'flex', gap: '15px', flex: '1' }}>
        {role === 'admin' ? (
          <>
            <Link to="/" style={{ textDecoration: 'none', color: 'black' }}>Calendar</Link>
            <Link to="/add-event" style={{ textDecoration: 'none', color: 'black' }}>Add Event</Link>
            <Link to="/all-users" style={{ textDecoration: 'none', color: 'black' }}>All Users</Link>
          </>
        ) : role === 'user' ? (
          <>
            <Link to="/" style={{ textDecoration: 'none', color: 'black' }}>My Schedule</Link>
            <Link to="/tasks" style={{ textDecoration: 'none', color: 'black' }}>Tasks</Link>
            <Link to="/notifications" style={{ textDecoration: 'none', color: 'black' }}>Notifications</Link>
          </>
        ) : (
          <span style={{ color: 'red' }}>Not logged in</span>
        )}
      </div>

      {/* Middle Section */}
      <div style={{ fontSize: '24px', fontWeight: 'bold', textAlign: 'center', flex: '1' }}>
        Calendify
      </div>

      {/* Right Section */}
      <div style={{ display: 'flex', justifyContent: 'flex-end', flex: '1' }}>
        <button
          style={{
            padding: '10px 30px',
            fontSize: '16px',
            border: '0',
            backgroundColor: '#236BE0',
            color: '#fff',
            borderRadius: '4px',
            cursor: 'pointer',
          }}
        >
          Search
        </button>
      </div>
    </header>
  );
};

export default Header;
