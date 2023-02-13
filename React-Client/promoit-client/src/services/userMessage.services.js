import axios from "axios";

import { toast } from "react-toastify";
import { constMessagesEndpoint } from "../constant/constantEndpoints";

//get all user messages
export const getMessagesData = async () => {
  try {
    let endpoint = `${constMessagesEndpoint}Get`;
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

//add user message to DB
export const addMessageToDb = async (message) => {
  try {
    console.log(message);
    let endpoint = `${constMessagesEndpoint}Add`;
    await axios.post(endpoint, message);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//delete user message
export const deleteMessageFromDb = async (messageID) => {
  try {
    console.log(messageID);
    let endpoint = `${constMessagesEndpoint}Remove/${messageID}`;
    await axios.delete(endpoint);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
