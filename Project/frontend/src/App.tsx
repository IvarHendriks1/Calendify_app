import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { UserInput } from './LoginPage';
import { EventCreator } from './EvenCreator';
import { CalendarPage } from './CalenderPage';
import { AlterEvent } from './AlterEvent';
import { DeletePopUp } from './DeletePopup';
import { RegisterPopup } from './ResgisterPopup';
import SearchPage from './SearchPage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<UserInput />} />
        <Route path="/calender" element={<CalendarPage />} />
        <Route path="/event" element={<EventCreator />} />
        <Route path="/search" element={<SearchPage />} />
        <Route path="/alter" element={<AlterEvent />} />
        <Route path="/DeletePopup" element={<DeletePopUp />} />
        <Route path="/RegisterPopup" element={<RegisterPopup />} />
      </Routes>
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
