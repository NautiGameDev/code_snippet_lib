import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
function Unauthenticated() {
    const navigate = useNavigate();

    const delayTransition = () => {
        setTimeout(() => {
            navigate("/login");
        }, 5000);
    };

    useEffect(() => {
        delayTransition();
    }, []);

  return (
      <div className="view v-col-center glitch-in error">
                <span class="material-icons xl-font">
                    warning
                </span>
              <span className="error med-font mt-16 heading">You don't have permission to access this page</span>
              <span className="error sm-font mt-16 heading">Returning to login page...</span>          
      </div>
  );
}

export default Unauthenticated;