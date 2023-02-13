import React, { useState } from "react";
import { ToastContainer } from "react-toastify";

import { toastError, toastSuccess } from "../../../constant/toastify";
import { addActivistToDb } from "../../services/activist.services";
import { MainSocialActivist } from "./../mainSocialActivist/main.social.activist.component";

import "react-toastify/dist/ReactToastify.css";
import "./style.css";

export const RegistrationSocialActivist = ({ Email }) => {
  const [activist, setActivist] = useState({
    FullName: "",
    Email: Email,
    Address: "",
    PhoneNumber: "",
  });
  // state to store the success status of adding social activist
  const [addSuccess, setAddsuccess] = useState(false);

  //add social activist to the database
  const handleAddActivist = async () => {
    let json = activist;
    if (!activist.FullName) {
      toastError("You must enter full name");
    } else if (!activist.Address) {
      toastError("You must enter address");
    } else if (!activist.PhoneNumber) {
      toastError("You must enter phone number");
    } else {
      //setAddsuccess(true) when addActivistToDb function is finished
      await addActivistToDb(json).then(() => {
        setAddsuccess(true);
      });
      toastSuccess(
        "The social activist has been added successfully to our system"
      );
      setActivist({});
      document.querySelectorAll("input").forEach((input) => (input.value = ""));
    }
  };

  //render the MainSocialActivist component if adding social activist is successful
  if (addSuccess === true) return <MainSocialActivist Email={Email} />;

  return (
    <div>
      <h1 className="register-h1">Social Activist Registration</h1>
      <div className="register-container">
        <ToastContainer />
        <div className="mb-3 ">
          <label htmlFor="basic-url" className="form-label">
            Full Name
          </label>
          <input
            type="text"
            className="form-control"
            id="basic-url"
            aria-describedby="basic-addon3"
            placeholder="Activist Name"
            onChange={(o) => {
              setActivist({
                ...activist,
                FullName: o.target.value,
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
        <div className="mb-3 ">
          <label htmlFor="basic-url" className="form-label">
            Address
          </label>
          <input
            type="text"
            className="form-control"
            id="basic-url"
            aria-describedby="basic-addon3"
            placeholder="Address"
            onChange={(o) => {
              setActivist({
                ...activist,
                Address: o.target.value,
              });
            }}
          />
        </div>
        <div className="mb-3 ">
          <label htmlFor="basic-url" className="form-label">
            Phone Number
          </label>
          <input
            type="text"
            className="form-control"
            id="basic-url"
            aria-describedby="basic-addon3"
            placeholder="Phone Number"
            onChange={(o) => {
              setActivist({
                ...activist,
                PhoneNumber: o.target.value,
              });
            }}
          />
        </div>
        <div className="register-button">
          <button className="btn btn-success" onClick={handleAddActivist}>
            Register
          </button>
        </div>
      </div>
    </div>
  );
};
