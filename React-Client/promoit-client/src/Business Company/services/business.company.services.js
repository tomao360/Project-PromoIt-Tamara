import axios from "axios";

import { toast } from "react-toastify";
import { constBusinessCompanyEndpoint } from "../../constant/constantEndpoints";

//get all business Companies
export const getBusinessCompaniesData = async () => {
  try {
    let endpoint = `${constBusinessCompanyEndpoint}Get`;
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

//get all count of donations
export const getCountOfDonations = async () => {
  try {
    let endpoint = `${constBusinessCompanyEndpoint}GetCountOfDonations`;
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

//get one business company by email
export const getBusinessCompanyByEmail = async (businessCompanyEmail) => {
  try {
    let endpoint = `${constBusinessCompanyEndpoint}Get/${businessCompanyEmail}`;
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

//get all shipments
export const getShipmentsData = async () => {
  try {
    let endpoint = `${constBusinessCompanyEndpoint}GetShipment`;
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

//get all shipments by businessID
export const getShipmentsDataByBusinessID = async (businessCompanyID) => {
  try {
    let endpoint = `${constBusinessCompanyEndpoint}GetShipmentByID/${businessCompanyID}`;
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

//add business company to DB
export const addBusinessCompanyToDb = async (businessCompany) => {
  try {
    console.log(businessCompany);
    let endpoint = `${constBusinessCompanyEndpoint}Add`;
    await axios.post(endpoint, businessCompany);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//update a business company
export const updateBusinessCompanyData = async (
  businessCompany,
  businessCompanyID
) => {
  try {
    console.log(businessCompany, "111111");
    await axios.put(
      `${constBusinessCompanyEndpoint}Update/${businessCompanyID}`,
      businessCompany
    );
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//delete a business company
export const deleteBusinessCompanyFromDb = async (businessCompanyID) => {
  try {
    console.log(businessCompanyID);
    let endpoint = `${constBusinessCompanyEndpoint}Remove/${businessCompanyID}`;
    await axios.delete(endpoint);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
