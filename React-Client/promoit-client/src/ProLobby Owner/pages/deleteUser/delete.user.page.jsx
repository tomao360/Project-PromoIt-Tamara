import React, { useState } from "react";

import {
  OrganizationsDelete,
  BusinessCompaniesDelete,
  SocialActivistsDelete,
} from "../../components";

import "./style.css";

export const DeleteUser = (props) => {
  const [selectValueUser, setSelecteValueUser] = useState("");
  const [generateUsers, setGenerateUsers] = useState(undefined);

  //handle generating the appropriate user component based on the selected value
  const handleGenerateUsers = () => {
    if (selectValueUser && selectValueUser === "1") {
      setGenerateUsers(<OrganizationsDelete />);
    } else if (selectValueUser && selectValueUser === "2") {
      setGenerateUsers(<BusinessCompaniesDelete />);
    } else if (selectValueUser && selectValueUser === "3") {
      setGenerateUsers(<SocialActivistsDelete />);
    }
  };

  return (
    <div>
      <div className="select-container-user">
        <div className="title-user">Choose User Role</div>
        <select
          className="form-select user"
          aria-label="Default select example"
          onChange={(e) => {
            setSelecteValueUser(e.target.value);
          }}
        >
          <option selected>select a user role</option>
          <option value="1">non-profit organization</option>
          <option value="2">business company</option>
          <option value="3">social activist</option>
        </select>
        <button
          className="btn btn-primary button-data"
          onClick={handleGenerateUsers}
        >
          Generate Data
        </button>
      </div>
      <div className="report">{generateUsers}</div>
    </div>
  );
};
