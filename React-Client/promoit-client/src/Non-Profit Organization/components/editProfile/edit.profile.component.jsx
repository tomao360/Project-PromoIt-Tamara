import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";

import { toastSuccess, toastError } from "../../../constant/toastify";
import { updateOrganizationData } from "../../services/organization.services";

import "react-toastify/dist/ReactToastify.css";
import "./style.css";

export const EditProfile = (props) => {
  const location = useLocation();
  console.log(location, "check");
  const { organization } = location.state;
  const navigate = useNavigate();
  const [editorganization, setEditOrganization] = useState({
    OrganizationName: "",
    LinkToWebsite: "",
    Description: "",
  });
  const [charactersLeft, setCharactersLeft] = useState(500);
  console.log(organization);

  //update organization data
  const handleUpdateOrganization = async () => {
    let updateMade = false;
    //update organization data just if editorganization is not empty and he is different from organization
    if (
      editorganization.OrganizationName !== "" &&
      editorganization.OrganizationName !== organization.OrganizationName
    ) {
      organization.OrganizationName = editorganization.OrganizationName;
      updateMade = true;
    }
    if (
      editorganization.LinkToWebsite !== "" &&
      editorganization.LinkToWebsite !== organization.LinkToWebsite
    ) {
      organization.LinkToWebsite = editorganization.LinkToWebsite;
      updateMade = true;
    }
    if (
      editorganization.Description !== "" &&
      editorganization.Description !== organization.Description
    ) {
      organization.Description = editorganization.Description;
      updateMade = true;
    }
    //if updateMade is true then update campaign data
    if (updateMade) {
      await updateOrganizationData(organization, organization.OrganizationID);
      toastSuccess("The organization data has been updated successfully");
      setEditOrganization({});
      setTimeout(() => {
        navigateTo();
      }, 1000);
    } else {
      toastError("No changes are made to the organization");
    }
  };

  //navigate to /profile reoute
  const navigateTo = () => {
    navigate("/profile");
  };

  return (
    <div className="container-editOrganization">
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
          Organization's Name
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={organization.OrganizationName}
          onChange={(o) => {
            console.log(o.target.value);
            setEditOrganization({
              ...editorganization,
              OrganizationName: o.target.value,
            });
          }}
        />
      </div>
      <div className="mb-3">
        <label htmlFor="Email1" className="form-label">
          Email address
        </label>
        <input
          readOnly
          type="email"
          className="form-control email-input"
          id="Email1"
          aria-describedby="emailHelp"
          value={organization.Email}
        />
      </div>
      <div className="mb-3">
        <label htmlFor="basic-url" className="form-label">
          Organization's Website URL
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={organization.LinkToWebsite}
          onChange={(o) => {
            setEditOrganization({
              ...editorganization,
              LinkToWebsite: o.target.value,
            });
          }}
        />
      </div>
      <div className="mb-3">
        <label htmlFor="Textarea1" className="form-label">
          Description About The Organization
        </label>
        <textarea
          className="form-control area-text"
          id="Textarea1"
          rows="3"
          defaultValue={organization.Description}
          onChange={(o) => {
            setEditOrganization({
              ...editorganization,
              Description: o.target.value,
            });
            //setCharactersLeft within the textarea to 500 - o.target.value.length
            setCharactersLeft(500 - o.target.value.length);
          }}
          //textarea can have 500 characters if over 500 the preventDefault function activates
          onKeyDown={(o) => {
            if (o.target.value.length >= 500) {
              o.preventDefault();
            }
          }}
          //the max length of the textarea is 500 characters
          maxLength={500}
        ></textarea>
        <div>Characters left:{charactersLeft}</div>
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
          onClick={handleUpdateOrganization}
        >
          Save Changes
        </button>
      </div>
    </div>
  );
};
