import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";

import { toastSuccess, toastError } from "../../../constant/toastify";
import { updateCampaignData } from "../../services/campaign.services";

import "./style.css";

export const EditCampaign = (props) => {
  const location = useLocation();
  const { campaign } = location.state;
  console.log(campaign);
  const navigate = useNavigate();
  const [editCampaign, setEditCampaign] = useState({
    CampaignName: "",
    LinkToLandingPage: "",
    Hashtag: "",
  });

  //update campaign data
  const handleUpdateCampaign = async () => {
    let updateMade = false;
    //update campaign data just if editCampaign is not empty and he is different from campaign
    if (
      editCampaign.CampaignName !== "" &&
      editCampaign.CampaignName !== campaign.CampaignName
    ) {
      campaign.CampaignName = editCampaign.CampaignName;
      updateMade = true;
    }
    if (
      editCampaign.LinkToLandingPage !== "" &&
      editCampaign.LinkToLandingPage !== campaign.LinkToLandingPage
    ) {
      campaign.LinkToLandingPage = editCampaign.LinkToLandingPage;
      updateMade = true;
    }
    if (
      editCampaign.Hashtag !== "" &&
      editCampaign.Hashtag !== campaign.Hashtag
    ) {
      campaign.Hashtag = editCampaign.Hashtag;
      updateMade = true;
    }

    //if updateMade is true then update campaign data
    if (updateMade) {
      await updateCampaignData(campaign, campaign.CampaignID);
      toastSuccess("The campaign has been updated successfully");
      setEditCampaign({});
      setTimeout(() => {
        navigateTo();
      }, 1000);
    } else {
      toastError("No changes are made to the campaign");
    }
  };

  //navigate to / => home page
  const navigateTo = () => {
    navigate("/");
  };

  return (
    <div className="container-editCampaign">
      <ToastContainer />
      <div className="close-button">
        <button
          type="button"
          className="btn-close"
          aria-label="Close"
          onClick={navigateTo}
        ></button>
      </div>
      <div className="mb-3 ">
        <label htmlFor="basic-url" className="form-label">
          Campaign Name
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={campaign.CampaignName}
          onChange={(o) => {
            console.log(o.target.value);
            setEditCampaign({
              ...editCampaign,
              CampaignName: o.target.value,
            });
          }}
        />
      </div>
      <div className="mb-3">
        <label htmlFor="basic-url" className="form-label">
          Link To Landing Page
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={campaign.LinkToLandingPage}
          onChange={(o) => {
            setEditCampaign({
              ...editCampaign,
              LinkToLandingPage: o.target.value,
            });
          }}
        />
      </div>
      <div className="mb-3">
        <label htmlFor="basic-url" className="form-label">
          Hashtag
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={campaign.Hashtag}
          onChange={(o) => {
            setEditCampaign({
              ...editCampaign,
              Hashtag: o.target.value,
            });
          }}
        />
      </div>
      <div className="edit-buttonsDiv">
        <button
          type="button"
          className="btn btn-secondary button-edit"
          onClick={navigateTo}
        >
          Close
        </button>
        <button
          type="button"
          className="btn btn-success button-edit"
          onClick={handleUpdateCampaign}
        >
          Save Changes
        </button>
      </div>
    </div>
  );
};
