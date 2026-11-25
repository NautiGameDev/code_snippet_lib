import { useNavigate, Outlet } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { checkAdmin } from '../services/accountService';
import { useAuth } from '../contexts/authContext';

function Dashboard() {

    const navigate = useNavigate();
    const { isLoggedIn } = useAuth();

    const [currentState, setCurrentState] = useState("Home");

    const handleAuthentication = async () => {
        if (!isLoggedIn) {
            navigate("/unauthenticated");
        }

        const response = await checkAdmin();

        if (response.status !== 200) {
            navigate('/unauthenticated');
        }
        
    }

    const handlePageChange = (page) => {
        setCurrentState(page);

        switch (page) {
            case "Home":
                navigate("");
                break;
            case "New Post":
                navigate("new-post");
                break;
            case "View Posts":
                navigate("view-posts");
                break;
            case "Logs":
                navigate("activity-logs");
                break;
            case "Return":
                navigate("/");
                break;
        }
    }

    useEffect(() => {
        handleAuthentication();
    }, []);


  return (
      <div className="page row">
          <div className="db-nav">
              <ul>
                  <li className={currentState === "Home" ? "active" : ""} onClick={() => handlePageChange("Home")}>
                      <span className="glitch-in glitch-hover no-select" data-text="Home">
                          Home
                      </span>
                  </li>
                  <li className={currentState === "New Post" ? "active" : ""} onClick={() => handlePageChange("New Post")}>
                      <span className="glitch-in glitch-hover no-select" data-text="New Post">
                          New Post
                      </span>
                  </li>
                  <li className={currentState === "View Posts" ? "active" : ""} onClick={() => handlePageChange("View Posts")}>
                      <span className="glitch-in glitch-hover no-select" data-text="View Posts">
                          View Posts
                      </span>
                  </li>
                  <li className={currentState === "Logs" ? "active" : ""} onClick={() => handlePageChange("Logs")}>
                      <span className="glitch-in glitch-hover no-select" data-text="Logs">
                          Logs
                      </span>
                  </li>
                  <li className={currentState === "Return" ? "active" : ""} onClick={() => handlePageChange("Return")}>
                      <span className="glitch-in glitch-hover no-select" data-text="Return">
                          Return
                      </span>
                  </li>
              </ul>
          </div>
          <div className="db-content">
              <div className="db-gradient">
                <Outlet/>
              </div>
          </div>
      </div>
  );
}

export default Dashboard;