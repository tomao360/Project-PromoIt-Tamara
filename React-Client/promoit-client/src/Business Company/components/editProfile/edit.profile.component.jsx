import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";

import { toastSuccess, toastError } from "../../../constant/toastify";
import { updateBusinessCompanyData } from "../../services/business.company.services";

import "./style.css";

export const EditProfile = (props) => {
  const location = useLocation();
  const { businessCompany } = location.state;
  const navigate = useNavigate();
  const [editBusinessCompanyName, setEditBusinessCompanyName] = useState("");

  //update business company properties
  const handleUpdateBusinessCompany = async () => {
    let updateMade = false;
    //update business company properties only when editBusinessCompanyName is not empty and editBusinessCompanyName is different from businessCompany
    if (
      editBusinessCompanyName !== "" &&
      editBusinessCompanyName !== businessCompany.BusinessName
    ) {
      businessCompany.BusinessName = editBusinessCompanyName;
      updateMade = true;
    }
    if (updateMade) {
      await updateBusinessCompanyData(
        businessCompany,
        businessCompany.BusinessID
      );
      toastSuccess(
        "The business company account has been updated successfully"
      );
      setEditBusinessCompanyName("");
      setTimeout(() => {
        navigateTo();
      }, 1000);
    } else {
      toastError("No changes are made to the business company");
    }
  };

  //navigate to /profile route
  const navigateTo = () => {
    navigate("/profile");
  };

  return (
    <div className="container-editBusiness">
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
          Business Company Name
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={businessCompany.BusinessName}
          onChange={(o) => {
            setEditBusinessCompanyName(o.target.value);
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
          value={businessCompany.Email}
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
          onClick={handleUpdateBusinessCompany}
        >
          Save Changes
        </button>
      </div>
    </div>
  );
};
