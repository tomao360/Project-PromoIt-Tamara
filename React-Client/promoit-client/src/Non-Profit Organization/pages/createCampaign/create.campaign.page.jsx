import React, { useState, useContext } from "react";
import { ToastContainer } from "react-toastify";

import { toastError, toastSuccess } from "../../../constant/toastify";
import { OroganizationContext } from "../../context/organization.context";
import { addCampaignToDb } from "../../services/campaign.services";

import "./style.css";

export const CreateCampaign = (props) => {
  const { organizationContext } = useContext(OroganizationContext);
  const [newCampaign, setNewCampaign] = useState({
    CampaignName: "",
    LinkToLandingPage: "",
    Hashtag: "",
    OrganizationID: 0,
  });

  //add campaign to database
  const handleAddCampaign = async () => {
    let json = newCampaign;
    if (!newCampaign.CampaignName) {
      toastError("You must enter campaign name");
    } else if (!newCampaign.LinkToLandingPage) {
      toastError("You must enter link to landing page");
    } else if (!newCampaign.Hashtag) {
      toastError("You must enter hashtag");
    } else {
      newCampaign.OrganizationID = organizationContext.OrganizationID;
      console.log(json);
      await addCampaignToDb(json);
      toastSuccess(
        "The campaign has been added to the organization successfully"
      );
      setNewCampaign({});
      document.querySelectorAll("input").forEach((input) => (input.value = ""));
    }
  };

  return (
    <div className="container-createCampaign">
      <ToastContainer />
      <div className="mb-3">
        <label htmlFor="basic-url" className="form-label">
          Campaign Name
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          placeholder="Campaign Name"
          onChange={(o) => {
            setNewCampaign({
              ...newCampaign,
              CampaignName: o.target.value,
            });
          }}
        />
      </div>
      <label htmlFor="basic-url" className="form-label">
        Campaign's Landing Page URL
      </label>
      <div className="input-group mb-3">
        <span className="input-group-text" id="basic-addon3">
          https://example.com/
        </span>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          onChange={(o) => {
            setNewCampaign({
              ...newCampaign,
              LinkToLandingPage: o.target.value,
            });
          }}
        />
      </div>
      <div className="mb-3">
        <label htmlFor="basic-url" className="form-label">
          Campaign's Hashtag
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          placeholder="Campaign's Hashtag #"
          onChange={(o) => {
            setNewCampaign({
              ...newCampaign,
              Hashtag: o.target.value,
            });
          }}
        />
      </div>
      <div className="create-button">
        <button
          type="button"
          className="btn btn-primary"
          onClick={handleAddCampaign}
        >
          Create Campaign
        </button>
      </div>
    </div>
  );
};
