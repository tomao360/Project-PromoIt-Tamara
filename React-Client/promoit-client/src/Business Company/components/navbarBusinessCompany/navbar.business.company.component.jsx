import React from "react";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import CallIcon from "@mui/icons-material/Call";
import VolunteerActivismRoundedIcon from "@mui/icons-material/VolunteerActivismRounded";
import LocalShippingRoundedIcon from "@mui/icons-material/LocalShippingRounded";
import LogoutRoundedIcon from "@mui/icons-material/LogoutRounded";

import "./style.css";

export const NavbarBusinessCompany = (props) => {
  const { user, logout } = useAuth0();
  return (
    <div>
      <nav className="navbar navbar-expand-lg nav-all">
        <div className="container-fluid">
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNavDropdown"
            aria-controls="navbarNavDropdown"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNavDropdown">
            <ul className="navbar-nav">
              <li className="nav-item">
                <Link className="nav-link" to="/">
                  <HomeIcon className="icon" />
                  Home
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/about">
                  <InfoIcon className="icon" />
                  About PromoIt
                </Link>
              </li>
              <li className="nav-item user-staff">
                <Link className="nav-link" to="/donations">
                  <VolunteerActivismRoundedIcon className="icon" />
                  My Donations
                </Link>
              </li>
              <li className="nav-item user-staff">
                <Link className="nav-link" to="/shipment-details">
                  <LocalShippingRoundedIcon className="icon" />
                  Shipment Details
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/contact-us">
                  <CallIcon className="icon" />
                  Contact Us
                </Link>
              </li>
              <li className="nav-item dropdown">
                <Link
                  className="nav-link dropdown-toggle show"
                  to="#"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="true"
                >
                  {user.name}
                </Link>
                <ul className="dropdown-menu show" data-bs-popper="static">
                  <li>
                    <Link className="dropdown-item" to="/profile">
                      Profile
                    </Link>
                  </li>
                  <li>
                    <hr className="dropdown-divider" />
                  </li>
                  <li>
                    <button
                      className="logout"
                      onClick={() =>
                        logout({ returnTo: window.location.origin })
                      }
                    >
                      <LogoutRoundedIcon className="icon" />
                      Log Out
                    </button>
                  </li>
                </ul>
              </li>
            </ul>
          </div>
        </div>
        <h6 className="user-role">Business Company</h6>
      </nav>
    </div>
  );
};
