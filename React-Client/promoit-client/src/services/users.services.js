import axios from "axios";

import { toast } from "react-toastify";
import { constUsersEndpoint } from "../constant/constantEndpoints";

//get all users
export const getUsersData = async () => {
  try {
    let endpoint = `${constUsersEndpoint}Get`;
    let response = await axios.get(endpoint);
    console.log(response);
    return response.data;
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//add user to DB
export const addUserToDb = async (user) => {
  try {
    console.log(user);
    let endpoint = `${constUsersEndpoint}Add`;
    await axios.post(endpoint, user);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
