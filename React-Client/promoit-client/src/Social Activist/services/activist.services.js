import axios from "axios";

import { toast } from "react-toastify";
import { constActivistsEndpoint } from "../../constant/constantEndpoints";

//get all social activists
export const getActivistsData = async () => {
  try {
    let endpoint = `${constActivistsEndpoint}Get`;
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

//get social activists by money earned
export const getMostMoneyEarned = async () => {
  try {
    let endpoint = `${constActivistsEndpoint}GetMostMoneyEarned`;
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

//get social activists by promted campaigns
export const getMostPromotedCampaigns = async () => {
  try {
    let endpoint = `${constActivistsEndpoint}GetMostPromotedCampaigns`;
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

//get one social activist by email
export const getActivistByEmail = async (activistEmail) => {
  try {
    let endpoint = `${constActivistsEndpoint}Get/${activistEmail}`;
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

//add social activist to DB
export const addActivistToDb = async (activist) => {
  try {
    console.log(activist);
    let endpoint = `${constActivistsEndpoint}Add`;
    await axios.post(endpoint, activist);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//update a social activist
export const updateActivistData = async (activist, activistID) => {
  try {
    console.log(activist, "111111");
    await axios.put(`${constActivistsEndpoint}Update/${activistID}`, activist);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//delete a social activist
export const deleteActivistFromDb = async (activistID) => {
  try {
    console.log(activistID);
    let endpoint = `${constActivistsEndpoint}Remove/${activistID}`;
    await axios.delete(endpoint);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
