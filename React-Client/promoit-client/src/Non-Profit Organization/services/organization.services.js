import axios from "axios";

import { toast } from "react-toastify";
import { constOrganizationsEndpoint } from "../../constant/constantEndpoints";

//get all non-profit organizations
export const getOrganizationsData = async () => {
  try {
    let endpoint = `${constOrganizationsEndpoint}Get`;
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

//get count of campaigns and products for each organization
export const getCountOfCampaignsProducts = async () => {
  try {
    let endpoint = `${constOrganizationsEndpoint}GetCampaignsProducts`;
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

//get one organization by email
export const getOrganizationByEmail = async (organizationEmail) => {
  try {
    let endpoint = `${constOrganizationsEndpoint}Get/${organizationEmail}`;
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

//add organization to DB
export const addOrganizationToDb = async (organization) => {
  try {
    console.log(organization);
    let endpoint = `${constOrganizationsEndpoint}Add`;
    await axios.post(endpoint, organization);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//update an organization
export const updateOrganizationData = async (organization, organizationID) => {
  try {
    console.log(organization, "111111");
    await axios.put(
      `${constOrganizationsEndpoint}Update/${organizationID}`,
      organization
    );
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//delete an organization
export const deleteOrganizationFromDb = async (organizationID) => {
  try {
    console.log(organizationID);
    let endpoint = `${constOrganizationsEndpoint}Remove/${organizationID}`;
    await axios.delete(endpoint);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
