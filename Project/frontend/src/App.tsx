import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { UserInput } from './LoginPage';
import { EventCreator } from './EvenCreator';
import { GreetingPage } from './CalenderPage';
import { AlterEvent } from './AlterEvent';
import SearchPage from './SearchPage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<UserInput />} />
        <Route path="/greeting" element={<GreetingPage />} />
        <Route path="/event" element={<EventCreator />} />
        <Route path="/search" element={<SearchPage />} />
        <Route path="/alter" element={<AlterEvent />} />
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
