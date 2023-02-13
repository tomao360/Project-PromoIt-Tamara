import axios from "axios";

import { toast } from "react-toastify";
import { constProductsEndpoint } from "../../constant/constantEndpoints";

//get all donated products
export const getDonatedProductsData = async () => {
  try {
    let endpoint = `${constProductsEndpoint}Get`;
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

//get donated products by business company ID
export const getDonatedProductsByBusinessID = async (businessCompanyID) => {
  try {
    let endpoint = `${constProductsEndpoint}Get/${businessCompanyID}`;
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

//add donated product to DB
export const addDonatedProductToDb = async (donatedProduct) => {
  try {
    console.log(donatedProduct);
    let endpoint = `${constProductsEndpoint}Add`;
    await axios.post(endpoint, donatedProduct);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//update a donated product
export const updateDonatedProductBought = async (donatedProduct, productID) => {
  try {
    console.log(donatedProduct, "111111");
    await axios.put(
      `${constProductsEndpoint}UpdateBought/${productID}`,
      donatedProduct
    );
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};

//update a donated product
export const updateDonatedProductShipped = async (
  donatedProduct,
  productID
) => {
  try {
    console.log(donatedProduct, "111111");
    await axios.put(
      `${constProductsEndpoint}UpdateShipped/${productID}`,
      donatedProduct
    );
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
