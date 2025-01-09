import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './LoginPage.css';
import logo from './calender.png';

export const UserInput: React.FC = () => {
  const [username, setUsername] = useState<string>(''); // State for username
  const [password, setPassword] = useState<string>(''); // State for password
  const [showPassword, setShowPassword] = useState<boolean>(false); // State for toggling password visibility
  const navigate = useNavigate();

  const handleLogin = () => {
    // Navigate to the greeting page and pass the username as a state
    navigate('/greeting', { state: { username } });
  };

  const togglePasswordVisibility = () => {
    setShowPassword((curState) => !curState);
  }

  return (
    <div>
       <header className = 'LoginPage-header'>
       <img src={logo} />
       <div style={{marginTop: '7vmin' }}>
        <label htmlFor="user">Username: </label>
        <input
          id="user"
          type="text"
          value={username}
          onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
            setUsername(event.target.value);
          }}
          placeholder="Enter your username"
        />
      </div>

      <div style={{ marginTop: '10px' }}>
        <label htmlFor="pass">Password: </label>
        <input
          id="pass"
          style={{ marginRight: '10px' }}
          type={showPassword? 'text' : 'password'}
          value = {password}
          onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
            setPassword(event.target.value);
          }}
          placeholder="Enter your password"
        />
        
        <button onClick = {togglePasswordVisibility}>show password</button>
      </div>

      <button
        onClick={handleLogin}
        style={{ marginTop: '10px', padding: '5px 10px' }}
      >
        Log In
      </button>
      <p></p>
      <p></p>
      <p></p>
      <p></p>
      <p></p>
      </header>
    </div>
  );
};
