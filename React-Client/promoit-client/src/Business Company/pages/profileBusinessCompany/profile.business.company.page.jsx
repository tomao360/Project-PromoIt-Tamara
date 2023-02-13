import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { getBusinessCompanyByEmail } from "../../services/business.company.services";

import "./style.css";

export const ProfileBusinessCompany = ({ Email }) => {
  const navigate = useNavigate();
  const [businessCompany, setBusinessCompany] = useState(undefined);

  //initialize the business company
  const initBusinessCompanyData = async () => {
    let businessCompany = await getBusinessCompanyByEmail(Email);
    setBusinessCompany(businessCompany);
  };

  useEffect(() => {
    initBusinessCompanyData();
  }, []);

  //navigate to /edit-profile route
  const navigateTo = () => {
    navigate("/edit-profile", { state: { businessCompany: businessCompany } });
  };

  return businessCompany && businessCompany !== undefined ? (
    <div className="card card-profile">
      <div className="card-body">
        {businessCompany.BusinessName && (
          <h3 className="card-title">
            Business Name: {businessCompany.BusinessName}
          </h3>
        )}
        {businessCompany.Email && (
          <h5 className="card-title">
            Business Email: {businessCompany.Email}
          </h5>
        )}
        <div className="card-buttons">
          <button className="btn btn-primary card-button" onClick={navigateTo}>
            Edit Business Details
          </button>
          <button
            type="button"
            className="btn btn-danger card-button"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
          >
            Delete Business
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
                  For deleting the business from our system please contact us
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
