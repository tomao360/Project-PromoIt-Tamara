import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import { Auth0Provider } from "@auth0/auth0-react";

import "./index.css";
import ".././node_modules/bootstrap/dist/css/bootstrap.css";
import ".././node_modules/bootstrap/dist/css/bootstrap-grid.min.css";
import ".././node_modules/bootstrap/dist/js/bootstrap.bundle.min.js";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Auth0Provider
        domain="dev-ti3u1n80psnj3o5q.us.auth0.com"
        clientId="QZbfLxTJF6xN4XDaGHgpJIufxmFtO9Ci"
        redirectUri={window.location.origin}
      >
        <App />
      </Auth0Provider>
    </BrowserRouter>
  </React.StrictMode>
);
