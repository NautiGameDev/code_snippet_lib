import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

function SessionExpired() {
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
          <span className="material-icons xl-font">
              error
          </span>
          <span className="error med-font mt-16 heading">Session expired. </span>
          <span className="error sm-font mt-16 heading">Returning to login page...</span>
      </div>
  );
}

export default SessionExpired;