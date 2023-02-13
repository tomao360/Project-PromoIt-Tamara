import axios from "axios";

import { toast } from "react-toastify";
import { constTwitterEndpoint } from "../constant/constantEndpoints";

//post tweet on twitter
export const postATweet = async (userName, campaignName) => {
  try {
    let endpoint = `${constTwitterEndpoint}Add/${userName}/${campaignName}`;
    await axios.post(endpoint);
  } catch (error) {
    toast(
      "An error occurred while executing the request. Please try again in a few minutes, if the problem continues contact ProLobby."
    );
    console.error(error);
  }
};
