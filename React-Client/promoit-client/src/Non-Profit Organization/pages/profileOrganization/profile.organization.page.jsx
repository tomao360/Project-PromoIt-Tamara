import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { getOrganizationByEmail } from "../../services/organization.services";

import "./style.css";

export const ProfileOrganization = ({ Email }) => {
  const navigate = useNavigate();
  const [organization, setOrganization] = useState(undefined);
  console.log(organization, "state");

  //initialize the organization object
  const initorganizationData = async () => {
    let organization = await getOrganizationByEmail(Email);
    console.log(organization, "tt");
    setOrganization(organization);
  };

  useEffect(() => {
    initorganizationData();
  }, []);

  //navigate to the /edit-profile route
  const navigateTo = () => {
    navigate("/edit-profile", { state: { organization: organization } });
  };

  return organization && organization !== undefined ? (
    <div className="card card-profileORG">
      <div className="card-body">
        {organization.OrganizationName && (
          <h3 className="card-title">
            Organization's Name: {organization.OrganizationName}
          </h3>
        )}
        {organization.Email && (
          <h5 className="card-title">
            Organization's Email: {organization.Email}
          </h5>
        )}
        {organization.Description && (
          <p className="card-text">
            Organization's Description: {organization.Description}
          </p>
        )}
        {organization.LinkToWebsite && (
          <Link className="card-title" to="#">
            {organization.LinkToWebsite}
          </Link>
        )}

        <div className="card-buttons">
          <button className="btn btn-primary card-button" onClick={navigateTo}>
            Edit Organization's Details
          </button>
          <button
            type="button"
            className="btn btn-danger card-button"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
          >
            Delete Organization
          </button>
        </div>
        <div
          className="modal fade"
          id="exampleModal"
          tabIndex="-1"
          aria-labelledby="exampleModalLabel"
          aria-hidden="true"
        >
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <button
                  type="button"
                  className="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>
              <div className="modal-body">
                <h4>
                  For deleting the organization from our system please contact
                  us
                </h4>
              </div>
              <div className="modal-footer">
                <button
                  type="button"
                  className="btn btn-secondary"
                  data-bs-dismiss="modal"
                >
                  Close
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  ) : (
    <div className="spinner-app">
      <RingLoader color="#414141" size={200} />
    </div>
  );
};
