import NavBar from "../components/NavBar";
import { Outlet } from "react-router-dom";
function Home() {
  return (
      <div className="page">
          <NavBar />
          <div className="page-container">
              <div className="page-gradient">
                    <Outlet />
              </div>
          </div>
      </div>
  );
}

export default Home;