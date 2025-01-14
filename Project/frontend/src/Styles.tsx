import React, { useState, useEffect } from "react";
import "./Styles.css";


export const ChooseStyle: React.FC = () => {
  const [isDarkMode, setIsDarkMode] = useState(() => {
    return localStorage.getItem("theme") === "dark";
  });

  const toggleDarkMode = () => setIsDarkMode((prev) => !prev);

  useEffect(() => {
    if (isDarkMode) {
      document.body.classList.add("dark-mode");
      localStorage.setItem("theme", "dark");
    } else {
      document.body.classList.remove("dark-mode");
      localStorage.setItem("theme", "light");
    }
  }, [isDarkMode]);
// 
  return (
    <div>
      <h1>Choose a Style</h1>
      <button onClick={toggleDarkMode}>
        Switch to {isDarkMode ? "Light" : "Dark"} Mode
      </button>
    </div>
  );
};
