import React, { useState, useEffect } from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { UserInput } from './LoginPage';
import { EventCreator } from './EvenCreator';
import { CalenderPage } from './CalenderPage';
import { AlterEvent } from './AlterEvent';
import { DeletePopUp } from './DeletePopup';
import { ChooseStyle } from './Styles';
import SearchPage from './SearchPage';
import './Styles.css'; // Import your global CSS styles here

const App: React.FC = () => {
  // State to manage dark mode
  const [isDarkMode, setIsDarkMode] = useState(() => {
    // Retrieve the saved theme preference from localStorage
    return localStorage.getItem('theme') === 'dark';
  });

  // Function to toggle dark mode
  const toggleDarkMode = () => {
    setIsDarkMode((prevMode) => !prevMode);
  };

  // Apply the dark mode class to the body
  useEffect(() => {
    if (isDarkMode) {
      document.body.classList.add('dark-mode');
      localStorage.setItem('theme', 'dark');
    } else {
      document.body.classList.remove('dark-mode');
      localStorage.setItem('theme', 'light');
    }
  }, [isDarkMode]);

  return (
    <Router>
      <div>
        {/* Add a global dark mode toggle button */}
        <header style={{ padding: '10px', background: isDarkMode ? '#333' : '#f5f5f5', color: isDarkMode ? '#fff' : '#000' }}>
          <button onClick={toggleDarkMode}>
            Switch to {isDarkMode ? 'Light' : 'Dark'} Mode
          </button>
        </header>

        <Routes>
          <Route path="/" element={<UserInput />} />
          <Route path="/calender" element={<CalenderPage />} />
          <Route path="/event" element={<EventCreator />} />
          <Route path="/search" element={<SearchPage />} />
          <Route path="/alter" element={<AlterEvent />} />
          <Route path="/DeletePopup" element={<DeletePopUp />} />
          <Route path="/choose" element={<ChooseStyle />} />
        </Routes>
      </div>
    </Router>
  );
};

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

export default App;
