import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";

import { Confirmation } from "../confirmation/confirmation.component";
import { MainProLobbyOwner } from "../../ProLobby Owner/components";
import {
  MainOrganization,
  RegistrationOrganization,
} from "../../Non-Profit Organization/components";
import {
  MainBusinessCompany,
  RegistrationBusinessCompany,
} from "../../Business Company/components";
import {
  MainSocialActivist,
  RegistrationSocialActivist,
} from "../../Social Activist/components";
import { getRoles } from "../../services/getRoles.services";
import { addUserToDb, getUsersData } from "../../services/users.services";

export const Main = (props) => {
  const { user } = useAuth0();
  const [usersArr, setUsersArr] = useState(undefined);
  const [roles, setRoles] = useState([]);
  const [finishedFetch, setFinishedFetch] = useState(false);

  //the handleRoles function is called to get the roles for the current user, and set them in the state
  const handleRoles = async () => {
    let userID = user.sub;
    let roles = await getRoles(userID);
    console.log(roles);
    setRoles(roles);
  };

  //the initUsersData function is called to get all users data and set it in the state
  const initUsersData = async () => {
    let users = await getUsersData();
    console.log(users);
    let usersObject = Object.values(users);
    setUsersArr(usersObject);
  };

  useEffect(() => {
    //set the finishedFetch to true when the handleRolesfinished fetching
    handleRoles().then(() => setFinishedFetch(true));
    initUsersData();
  }, []);

  //the handleAddUsertoDB function is called to add the current user's email to the database
  const handleAddUsertoDB = async () => {
    let json = user.email;
    await addUserToDb(json);
  };

  return (
    <div>
      {roles && roles.length > 0
        ? roles.map((role) => {
            console.log(role.name, 1212);
            if (role.name === "ProLobby Owner ") {
              handleAddUsertoDB();
              return <MainProLobbyOwner />;
            } else if (role.name === "Non-Profit Organization") {
              if (usersArr.find((u) => u.Email === user.email) !== undefined) {
                return <MainOrganization Email={user.email} />;
              } else {
                handleAddUsertoDB();
                return <RegistrationOrganization Email={user.email} />;
              }
            } else if (role.name === "Business Company ") {
              if (usersArr.find((u) => u.Email === user.email) !== undefined) {
                return <MainBusinessCompany Email={user.email} />;
              } else {
                handleAddUsertoDB();
                return <RegistrationBusinessCompany Email={user.email} />;
              }
            } else if (role.name === "Social Activist") {
              if (usersArr.find((u) => u.Email === user.email) !== undefined) {
                return <MainSocialActivist Email={user.email} />;
              } else {
                handleAddUsertoDB();
                return <RegistrationSocialActivist Email={user.email} />;
              }
            }
            return null;
          })
        : //if finishedFetch is true and there are no roles, it renders the Confirmation component
          finishedFetch === true && <Confirmation />}
    </div>
  );
};
