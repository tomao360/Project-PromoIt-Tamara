import React from "react";
import { Link } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

import HomeIcon from "@mui/icons-material/Home";
import LogoutRoundedIcon from "@mui/icons-material/LogoutRounded";
import InfoIcon from "@mui/icons-material/Info";
import CallIcon from "@mui/icons-material/Call";
import CampaignRoundedIcon from "@mui/icons-material/CampaignRounded";

import "./style.css";

export const NavbarOrganization = (props) => {
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
                <Link className="nav-link" to="/create-campaign">
                  <CampaignRoundedIcon className="icon" />
                  Create A Campaign
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
        <h6 className="user-role">Non-Profit Organization</h6>
      </nav>
    </div>
  );
};
