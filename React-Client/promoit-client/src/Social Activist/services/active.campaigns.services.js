import axios from "axios";

import { toast } from "react-toastify";
import { constActiveCampaignsEndpoint } from "../../constant/constantEndpoints";

//get all active campaigns
export const getActiveCampaignsData = async () => {
  try {
    let endpoint = `${constActiveCampaignsEndpoint}Get`;
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

//get donated products by business activist ID
export const getDonatedProductsByActivistID = async (activistID) => {
  try {
    let endpoint = `${constActiveCampaignsEndpoint}Get/${activistID}`;
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

//get active campaigns by social activist ID
export const getActivieCampaignsByActivistID = async (activistID) => {
  try {
    let endpoint = `${constActiveCampaignsEndpoint}GetCampaigns/${activistID}`;
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

//add active campaign to DB
export const addActiveCampaignToDb = async (activeCampaign) => {
  try {
    console.log(activeCampaign);
    let endpoint = `${constActiveCampaignsEndpoint}Add`;
    await axios.post(endpoint, activeCampaign);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//update an active campaign
export const updateActiveCampaignAddMoney = async (
  activeCampaign,
  activeCampID
) => {
  try {
    console.log(activeCampaign, "111111");
    await axios.put(
      `${constActiveCampaignsEndpoint}UpdateAddMoney/${activeCampID}`,
      activeCampaign
    );
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//update an active campaign
export const updateActiveCampaignSubtractMoney = async (
  activeCampaign,
  activeCampID
) => {
  try {
    console.log(activeCampaign, "111111");
    await axios.put(
      `${constActiveCampaignsEndpoint}UpdateSubtractMoney/${activeCampID}`,
      activeCampaign
    );
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
