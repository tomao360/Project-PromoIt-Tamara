import React, { useState } from "react";
import { ToastContainer } from "react-toastify";

import { toastError, toastSuccess } from "../../../constant/toastify";
import { MainBusinessCompany } from "../index";
import { addBusinessCompanyToDb } from "../../services/business.company.services";

import "./style.css";

export const RegistrationBusinessCompany = ({ Email }) => {
  const [businessCompany, setBusinessCompany] = useState({
    BusinessName: "",
    Email: Email,
  });
  // state to store the success status of adding business company
  const [addSuccess, setAddsuccess] = useState(false);

  //add business company to the database
  const handleAddBusinessCompany = async () => {
    let json = businessCompany;
    if (!businessCompany.BusinessName) {
      toastError("You must enter business company name");
    } else {
      //setAddsuccess(true) when addBusinessCompanyToDb function is finished
      await addBusinessCompanyToDb(json).then(() => {
        setAddsuccess(true);
      });
      toastSuccess(
        "The business company has been added successfully to our system"
      );
      setBusinessCompany({});
      document.querySelectorAll("input").forEach((input) => (input.value = ""));
    }
  };

  //render the MainBusinessCompany component if adding business company is successful
  if (addSuccess === true) return <MainBusinessCompany Email={Email} />;

  return (
    <div>
      <h1 className="register-h1">Business Company Registration</h1>
      <div className="register-container">
        <ToastContainer />
        <div className="mb-3 ">
          <label htmlFor="basic-url" className="form-label">
            Business Name
          </label>
          <input
            type="text"
            className="form-control"
            id="basic-url"
            aria-describedby="basic-addon3"
            placeholder="Organization's Name"
            onChange={(o) => {
              setBusinessCompany({
                ...businessCompany,
                BusinessName: o.target.value,
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
        <div className="register-button">
          <button
            className="btn btn-success"
            onClick={handleAddBusinessCompany}
          >
            Register
          </button>
        </div>
      </div>
    </div>
  );
};
