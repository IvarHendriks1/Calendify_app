import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { UserInput } from './LoginPage';
//import { EventCreator } from './EvenCreator';
import { GreetingPage } from './CalenderPage';
//<Route path="/event" element={<EventCreator />} />

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<UserInput />} />
        <Route path="/greeting" element={<GreetingPage />} />
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
