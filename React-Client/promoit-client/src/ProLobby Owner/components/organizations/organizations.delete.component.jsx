import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";

import {
  getOrganizationsData,
  deleteOrganizationFromDb,
} from "../../../Non-Profit Organization/services/organization.services";

export const OrganizationsDelete = (props) => {
  const [organizationArr, setOrganizationArr] = useState(undefined);
  const [filteredOrganizationArr, setFilteredOrganizationArr] =
    useState(undefined);

  //initialize organizations data => organizationArr - will stay the same, filteredOrganizationArr - will change
  const initOrganizationsData = async () => {
    let organizations = await getOrganizationsData();
    console.log(organizations);
    let organizationsObject = Object.values(organizations);
    setOrganizationArr(organizationsObject);
    setFilteredOrganizationArr(organizationsObject);
  };

  useEffect(() => {
    initOrganizationsData();
  }, []);

  //delete organization by OrganizationID
  const handleDeleteOrganization = async (OrganizationID) => {
    await deleteOrganizationFromDb(OrganizationID);
    let newOrganizationsArr = organizationArr.filter(
      (s) => s.OrganizationID !== OrganizationID
    );
    setFilteredOrganizationArr(newOrganizationsArr);
  };

  //search the search input value and show the result in the table
  const handleSearch = (searchValue) => {
    //filter organizationArr by the Email.includes the searchValue
    let result = organizationArr.filter((organization) =>
      organization.Email.toLowerCase().includes(searchValue.toLowerCase())
    );
    //set the setFilteredOrganizationArr with the result
    setFilteredOrganizationArr(result);
  };

  return (
    <div className="organization-container">
      <div>
        <h3>Non-Profit Organizations</h3>
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text" id="basic-addon1">
          Search User By Email
        </span>
        <input
          type="text"
          className="form-control"
          placeholder="Search By Email"
          aria-label="Username"
          aria-describedby="basic-addon1"
          onChange={(e) => {
            handleSearch(e.target.value);
          }}
        />
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Organization Name</th>
              <th scope="col">Email</th>
              <th scope="col">Link To Website</th>
              <th scope="col">Description</th>
              <th scope="col"></th>
            </tr>
          </thead>
          {filteredOrganizationArr && filteredOrganizationArr !== undefined ? (
            filteredOrganizationArr.map((o) => {
              let {
                OrganizationID,
                OrganizationName,
                Email,
                LinkToWebsite,
                Description,
                DeleteAnswer,
              } = o;
              return (
                <tbody>
                  <tr>
                    <td>{OrganizationName}</td>
                    <td>{Email}</td>
                    <td>
                      <Link>{LinkToWebsite}</Link>
                    </td>
                    <td>{Description}</td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-danger"
                        data-bs-toggle="modal"
                        data-bs-target={`#staticBackdrop${OrganizationID}`}
                      >
                        Delete Organization
                      </button>
                      <div
                        className="modal fade"
                        id={`staticBackdrop${OrganizationID}`}
                        data-bs-backdrop="static"
                        data-bs-keyboard="false"
                        tabindex="-1"
                        aria-labelledby="staticBackdropLabel"
                        aria-hidden="true"
                      >
                        <div className="modal-dialog">
                          <div className="modal-content">
                            <div className="modal-header">
                              <h1
                                className="modal-title fs-5"
                                id="staticBackdropLabel"
                              >
                                Delete Organization
                              </h1>
                              <button
                                type="button"
                                className="btn-close"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                              ></button>
                            </div>
                            {DeleteAnswer === "1" ? (
                              <div className="modal-body">
                                Are you sure you want to delete "
                                {OrganizationName}" non-profit organization?
                              </div>
                            ) : (
                              <div className="modal-body">
                                The non-profit organization: "{OrganizationName}
                                " is linked to other records in the system. If
                                you delete the non-profit organization, the
                                following information may be deleted, and you
                                will not be able to restore it. Are you sure you
                                want to delete the non-profit organization?
                              </div>
                            )}
                            <div className="modal-footer">
                              <button
                                type="button"
                                className="btn btn-secondary"
                                data-bs-dismiss="modal"
                              >
                                No
                              </button>
                              <button
                                type="button"
                                className="btn btn-danger"
                                onClick={() => {
                                  handleDeleteOrganization(OrganizationID);
                                }}
                                data-bs-dismiss="modal"
                              >
                                Delete
                              </button>
                            </div>
                          </div>
                        </div>
                      </div>
                    </td>
                  </tr>
                </tbody>
              );
            })
          ) : (
            <tbody>
              <tr>
                <td colSpan={9}>
                  <RingLoader className="spinner" color="#414141" />
                </td>
              </tr>
            </tbody>
          )}
        </table>
      </div>
    </div>
  );
};
