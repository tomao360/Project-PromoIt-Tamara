import React from "react";
import { useAuth0 } from "@auth0/auth0-react";

import logo from "../../images/PromoIt-1.jpg";

import "./style.css";

export const UnauthorizedUsers = (props) => {
  const { loginWithRedirect } = useAuth0();
  return (
    <div className="app unauthorized-container">
      <img className="logo" src={logo} alt="Logo" />
      <button
        className="btn btn-light enter-button"
        onClick={() => loginWithRedirect("http://localhost:3000")}
      >
        Login
      </button>
    </div>
  );
};
