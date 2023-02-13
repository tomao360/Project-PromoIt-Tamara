import React from "react";
import { useAuth0 } from "@auth0/auth0-react";

import "./style.css";

export const Confirmation = (props) => {
  const { logout } = useAuth0();
  return (
    <div className="app confirmation-container">
      <h1>Thank You For Signing Up!</h1>
      <p className="p-confirmation">
        You will be assigned to the website shortly <br />
        Please logout and login again in a few minutes
      </p>
      <button
        className="btn btn-light logout-button"
        onClick={() => logout({ returnTo: window.location.origin })}
      >
        Log Out
      </button>
    </div>
  );
};
