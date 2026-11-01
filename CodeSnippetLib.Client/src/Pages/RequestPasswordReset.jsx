import { useState } from 'react';
import { requestPasswordReset } from '../services/accountService';

function RequestPasswordReset() {
    const [email, setEmail] = useState("");
    const [sysMessage, setSysMessage] = useState("");
    const [errorMessage, setErrorMessage] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        setSysMessage("");
        setErrorMessage("");

        const isEmail = /^\S+@\S+\.[a-zA-Z]{2,6}$/i;

        if (!isEmail.test(email)) {
            setErrorMessage("Must enter a valid email address");
            return;
        }

        setSysMessage("Sending request to reset password");

        const response = await requestPasswordReset(email);

        if (response.status === 200) {
            setSysMessage(response.message);
        }
        else {
            setSysMessage(`Error ${response.status}: ${response.message}`)
        }
    }

  return (
      <div className="view v-col-center">

          { sysMessage.trim() === "" ? (
              <form className="glitch-in" onSubmit={(e) => handleSubmit(e)}>
                  <div className="form-header">
                      <h1>Request password reset</h1>                  
                  </div>
                  <div className="form-row error">
                      {errorMessage}
                  </div>
                  <div className="form-row">
                      <p>Enter your email address to receive confirmation link:</p>
                  </div>
                  <div className="form-row">
                      <label htmlFor="email">Email Address:</label>
                      <input className="w-50" id="email" type="email" value={email} placeholder="Enter email associated with account" onChange={(e) => setEmail(e.target.value)}></input>
                  </div>
                  <div className="form-btn-container">
                    <button type="submit">Reset password</button>
                  </div>
              </form>
          ) : (
            <span className="sm-font tertiary heading w-75 glitch-in">{sysMessage}</span>
          )}

      </div>
  );
}

export default RequestPasswordReset;