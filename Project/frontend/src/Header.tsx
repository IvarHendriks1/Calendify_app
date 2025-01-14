import React from 'react';
import { Link } from 'react-router-dom';

const Header: React.FC = () => {
  return (
    <header
      style={{
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        padding: '10px',
        backgroundColor: '#f0f0f0',
        borderBottom: '1px solid #ddd',
      }}
    >
      {/* Left Section */}
      <nav style={{ display: 'flex', gap: '15px' }}>
        <Link to="/" style={{ textDecoration: 'none', color: 'black' }}>
          Home
        </Link>
        <Link to="/lorem1" style={{ textDecoration: 'none', color: 'black' }}>
          Lorem Ipsum
        </Link>
        <Link to="/lorem2" style={{ textDecoration: 'none', color: 'black' }}>
          Lorem Ipsum
        </Link>
      </nav>

      {/* Middle Section */}
      <div style={{ fontSize: '24px', fontWeight: 'bold' }}>Logo</div>

      {/* Right Section */}
      <button style={{ padding: '5px 10px', fontSize: '16px' }}>Search</button>
    </header>
  );
};

export default Header;
