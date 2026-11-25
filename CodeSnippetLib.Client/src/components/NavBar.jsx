import { useState } from 'react';

function NavBar() {
    const [currentState, setCurrentState] = useState("Home");

    const handlePageChange = (page) => {
        setCurrentState(page);
    }

  return (
      <div className="nav-bar">
          <div className="nav-header">
          </div>
          <div className="nav-links">
              <ul>
                  <li className={currentState === "Home" ? "active" : ""} onClick={() => handlePageChange("Home")}>
                      <span className="glitch-in glitch-hover no-select" data-text="Home">
                          Home
                      </span>
                  </li>
                  <li className={currentState === "About" ? "active" : ""} onClick={() => handlePageChange("About")}>
                      <span className="glitch-in glitch-hover no-select" data-text="About">
                          About
                      </span>
                  </li>
                  <li className={currentState === "Projects" ? "active" : ""} onClick={() => handlePageChange("Projects")}>
                      <span className="glitch-in glitch-hover no-select" data-text="Projects">
                          Project
                      </span>
                  </li>
                  <li className={currentState === "Blog" ? "active" : ""} onClick={() => handlePageChange("Blog")}>
                      <span className="glitch-in glitch-hover no-select" data-text="Blog">
                          Blog
                      </span>
                  </li>
                  <li className={currentState === "Contact" ? "active" : ""} onClick={() => handlePageChange("Contact")}>
                      <span className="glitch-in glitch-hover no-select" data-text="Contact">
                          Contact
                      </span>
                  </li>
              </ul>
          </div>
          <div className="nav-account">
              <span className="material-icons glitch-in glitch-hover no-select" data-text="account_circle">
                  account_circle
              </span>
          </div>
      </div>
  );
}

export default NavBar;