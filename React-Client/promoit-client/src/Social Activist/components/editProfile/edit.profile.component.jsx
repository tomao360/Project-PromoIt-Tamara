import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";

import { toastSuccess, toastError } from "../../../constant/toastify";
import { updateActivistData } from "../../services/activist.services";

import "./style.css";

export const EditProfile = (props) => {
  const location = useLocation();
  const { activist } = location.state;
  const navigate = useNavigate();
  const [editActivist, setEditActivis] = useState({
    FullName: "",
    Address: "",
    PhoneNumber: "",
  });

  //update the social activist information
  const handleUpdateActivist = async () => {
    let updateMade = false;
    //update social activist properties only when editActivist is not empty and editActivist is different from activist
    if (
      editActivist.FullName !== "" &&
      editActivist.FullName !== activist.FullName
    ) {
      activist.FullName = editActivist.FullName;
      updateMade = true;
    }
    if (
      editActivist.Address !== "" &&
      editActivist.Address !== activist.Address
    ) {
      activist.Address = editActivist.Address;
      updateMade = true;
    }
    if (
      editActivist.PhoneNumber !== "" &&
      editActivist.PhoneNumber !== activist.PhoneNumber
    ) {
      activist.PhoneNumber = editActivist.PhoneNumber;
      updateMade = true;
    }
    if (updateMade) {
      await updateActivistData(activist, activist.ActivistID);
      toastSuccess("The social activist account has been updated successfully");
      setEditActivis({});
      setTimeout(() => {
        navigateTo();
      }, 1000);
    } else {
      toastError("No changes are made to the social activist account");
    }
  };

  //navigate to /profile reoute
  const navigateTo = () => {
    navigate("/profile");
  };

  return (
    <div className="container-editActivist">
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
          Activist Name
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={activist.FullName}
          onChange={(o) => {
            console.log(o.target.value);
            setEditActivis({
              ...editActivist,
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
          value={activist.Email}
        />
      </div>
      <div className="mb-3">
        <label htmlFor="basic-url" className="form-label">
          Address
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={activist.Address}
          onChange={(o) => {
            setEditActivis({
              ...editActivist,
              Address: o.target.value,
            });
          }}
        />
      </div>
      <div className="mb-3">
        <label htmlFor="basic-url" className="form-label">
          Phone Number
        </label>
        <input
          type="text"
          className="form-control"
          id="basic-url"
          aria-describedby="basic-addon3"
          defaultValue={activist.PhoneNumber}
          onChange={(o) => {
            setEditActivis({
              ...editActivist,
              PhoneNumber: o.target.value,
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
          onClick={handleUpdateActivist}
        >
          Save Changes
        </button>
      </div>
    </div>
  );
};
