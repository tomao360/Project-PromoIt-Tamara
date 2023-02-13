import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";
import { useNavigate } from "react-router-dom";

import { getActivistByEmail } from "../../services/activist.services";

import "./style.css";

export const ProfileSocialActivist = ({ Email }) => {
  const navigate = useNavigate();
  const [activist, setActivist] = useState(undefined);

  //initialize the social activist object
  const initActivistData = async () => {
    let activist = await getActivistByEmail(Email);
    setActivist(activist);
  };

  useEffect(() => {
    initActivistData();
  }, []);

  //navigate to /edit-profile route
  const navigateTo = () => {
    navigate("/edit-profile", { state: { activist: activist } });
  };

  return activist && activist !== undefined ? (
    <div className="card card-profile">
      <div className="card-body">
        {activist.FullName && (
          <h3 className="card-title">Full Name: {activist.FullName}</h3>
        )}
        {activist.Email && (
          <h5 className="card-title">Email: {activist.Email}</h5>
        )}
        {activist.Address && (
          <h5 className="card-title">Address: {activist.Address}</h5>
        )}
        {activist.PhoneNumber && (
          <h5 className="card-title">Phone Number: {activist.PhoneNumber}</h5>
        )}
        <div className="card-buttons">
          <button className="btn btn-primary card-button" onClick={navigateTo}>
            Edit Account Details
          </button>
          <button
            type="button"
            className="btn btn-danger card-button"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
          >
            Delete Account
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
                  For deleting your account from our system please contact us
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
