import axios from "axios";

import { toast } from "react-toastify";
import { constCampaignsEndpoint } from "../../constant/constantEndpoints";

//get all campaigns
export const getCampaignsData = async () => {
  try {
    let endpoint = `${constCampaignsEndpoint}Get`;
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

//get one campaign by organization email
export const getCampaignByORGEmail = async (organizationEmail) => {
  try {
    let endpoint = `${constCampaignsEndpoint}Get/${organizationEmail}`;
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

//add campaign to DB
export const addCampaignToDb = async (campaign) => {
  try {
    console.log(campaign);
    let endpoint = `${constCampaignsEndpoint}Add`;
    await axios.post(endpoint, campaign);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//update a campaign
export const updateCampaignData = async (campaign, campaignID) => {
  try {
    console.log(campaign, "111111");
    await axios.put(`${constCampaignsEndpoint}Update/${campaignID}`, campaign);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//delete a campaign
export const deleteCampaignFromDb = async (campaignID) => {
  try {
    console.log(campaignID);
    let endpoint = `${constCampaignsEndpoint}Remove/${campaignID}`;
    await axios.delete(endpoint);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//get all campaigns & organizations
export const getCampaignsAndOrganizationsData = async () => {
  try {
    let endpoint = `${constCampaignsEndpoint}GetCampaignsAndORG`;
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

//get campaigns by popularity
export const getPopularCampaign = async () => {
  try {
    let endpoint = `${constCampaignsEndpoint}GetPopularCampaign`;
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

//get campaigns by profitability
export const getProfitableCampaign = async () => {
  try {
    let endpoint = `${constCampaignsEndpoint}GetProfitableCampaign`;
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
