import axios from "axios";

import { toast } from "react-toastify";
import { constRolesEndpoint } from "../constant/constantEndpoints";

//get the roles from database
export const getRoles = async (userID) => {
  try {
    let result = await axios.get(`${constRolesEndpoint}${userID}`);
    return result.data;
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
