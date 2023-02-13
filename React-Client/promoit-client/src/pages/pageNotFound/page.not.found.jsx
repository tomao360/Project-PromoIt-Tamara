import React from "react";

import pageNotFound from "../../images/404 Error with a cute animal-pana.png";

import "./style.css";

export const PageNotFound = (props) => {
  return (
    <div className="app">
      <img className="page-not-found" src={pageNotFound} alt="Page Not Found" />
    </div>
  );
};
