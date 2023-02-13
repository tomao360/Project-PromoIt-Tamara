import React, { useState } from "react";
import { ToastContainer } from "react-toastify";

import { toastError, toastSuccess } from "../../../constant/toastify";
import { addOrganizationToDb } from "../../services/organization.services";
import { MainOrganization } from "../mainOrganization/main.organization.component";

import "./style.css";

export const RegistrationOrganization = ({ Email }) => {
  const [organization, setOrganization] = useState({
    OrganizationName: "",
    Email: Email,
    LinkToWebsite: "",
    Description: "",
  });
  const [charactersLeft, setCharactersLeft] = useState(500);
  //state to store the success status of adding organization
  const [addSuccess, setAddsuccess] = useState(false);

  //add organization to database
  const handleAddOrganization = async () => {
    let json = organization;
    if (!organization.OrganizationName) {
      toastError("You must enter organization name");
    } else if (!organization.LinkToWebsite) {
      toastError("You must enter link to organization's website");
    } else if (!organization.Description) {
      toastError("You must enter description about the organization");
    } else {
      //setAddsuccess(true) when addOrganizationToDb function is finished
      await addOrganizationToDb(json).then(() => {
        setAddsuccess(true);
      });
      toastSuccess("The organization was added successfully to the database");
      setOrganization({});
      document.querySelectorAll("input").forEach((input) => (input.value = ""));
      document
        .querySelectorAll("textarea")
        .forEach((textarea) => (textarea.value = ""));
    }
  };

  //render the MainOrganization component if adding organization is successful
  if (addSuccess === true) return <MainOrganization Email={Email} />;

  return (
    <div>
      <h1 className="register-h1">Non-Profit Organization Registration</h1>
      <div className="register-container">
        <ToastContainer />
        <div className="mb-3 ">
          <label htmlFor="basic-url" className="form-label">
            Organization's Name
          </label>
          <input
            type="text"
            className="form-control"
            id="basic-url"
            aria-describedby="basic-addon3"
            placeholder="Organization's Name"
            onChange={(o) => {
              setOrganization({
                ...organization,
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
            value={Email}
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
            placeholder="https://example.com"
            onChange={(o) => {
              setOrganization({
                ...organization,
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
            onChange={(o) => {
              setOrganization({ ...organization, Description: o.target.value });
              //setCharactersLeft within the textarea to 500 - o.target.value.length
              setCharactersLeft(500 - o.target.value.length);
            }}
            onKeyDown={(o) => {
              //textarea can have 500 characters if over 500 the preventDefault function activates
              if (o.target.value.length >= 500) {
                o.preventDefault();
              }
            }}
            //the max length of the textarea is 500 characters
            maxLength={500}
          ></textarea>
          <div>Characters left:{charactersLeft}</div>
        </div>
        <div className="register-button">
          <button className="btn btn-success" onClick={handleAddOrganization}>
            Register
          </button>
        </div>
      </div>
    </div>
  );
};
