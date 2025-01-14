import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './Header'; // Import the Header component
import { UserInput } from './LoginPage';
import { EventCreator } from './EvenCreator';
import { CalendarPage } from './CalenderPage';
import { AlterEvent } from './AlterEvent';
import { DeletePopUp } from './DeletePopup';
import SearchPage from './SearchPage';

const App: React.FC = () => {
  return (
    <Router>
      <Header /> {/* Ensure Header is inside the Router */}
      <Routes>
        <Route path="/" element={<UserInput />} />
        <Route path="/calender" element={<CalendarPage />} />
        <Route path="/event" element={<EventCreator />} />
        <Route path="/search" element={<SearchPage />} />
        <Route path="/alter" element={<AlterEvent />} />
        <Route path="/DeletePopup" element={<DeletePopUp />} />
      </Routes>
    </Router>
  );
};

// Initialize the app only once
const container = document.getElementById('root') as HTMLElement;
const root = ReactDOM.createRoot(container);

// Use root.render() to render the app
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

export default App;
