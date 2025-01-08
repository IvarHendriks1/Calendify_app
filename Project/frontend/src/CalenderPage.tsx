import React from 'react';
import { useLocation } from 'react-router-dom';

export const GreetingPage: React.FC = () => {
  const location = useLocation();
  const { username } = location.state || { username: 'Guest' }; // Fallback to 'Guest' if no username is provided

  return (
    <div>
      <h1>Welcome, {username}!</h1>
    </div>
  );
};
