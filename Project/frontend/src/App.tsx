import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { UserInput } from './LoginPage';
import { EventCreator } from './EvenCreator';
import { CalenderPage } from './CalenderPage';
import SearchPage from './SearchPage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<UserInput />} />
        <Route path="/calender" element={<CalenderPage />} />
        <Route path="/event" element={<EventCreator />} />
        <Route path="/search" element={<SearchPage />} />
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
