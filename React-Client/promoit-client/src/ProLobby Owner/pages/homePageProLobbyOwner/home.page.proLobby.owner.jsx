import React from "react";
import { useAuth0 } from "@auth0/auth0-react";

import "./style.css";

export const HomePageProLobbyOwner = (props) => {
  const { user } = useAuth0();
  return (
    <div className="homeOwner-container">
      <h1>Welcome {user.name}</h1>
      <p className="p-homeOwner">
        On this website you can find reports about every user who registered on
        your site and was assigned to a role <br />
        To get the reports, go to the "Generate Reports" page <br />
        Also, please pay attention to the "User Messages" page if you received a
        requests from the users to delete their account <br />
        If so, please delete accordingly
      </p>
    </div>
  );
};
