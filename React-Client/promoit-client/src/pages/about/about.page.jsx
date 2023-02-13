import React from "react";

import "./style.css";

export const About = (props) => {
  return (
    <div className="about-container">
      <h1>About PromoIt</h1>
      <p className="p-about">
        PromoIt is a system that handles the social promotion of the non-profit
        organization and other related campaigns. <br />
        The system's goal is to promote social campaigns.
        <br />
        The means to do so involve onboarding business organizations that donate
        products, onboarding non-profit organizations that want to promote
        campaigns, and onboarding social activists - users of Twitter that can
        promote those campaigns.
      </p>
    </div>
  );
};
