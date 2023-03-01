import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import {
  getBusinessCompaniesData,
  deleteBusinessCompanyFromDb,
} from "../../../Business Company/services/business.company.services";

export const BusinessCompaniesDelete = (props) => {
  const [businessCompaniesArr, setBusinessCompaniesArr] = useState(undefined);
  const [filteredBusinessCompaniesArr, setFilteredBusinessCompaniesArr] =
    useState(undefined);

  //initialize business companies => businessCompaniesArr - will stay the same, filteredBusinessCompaniesArr - will change
  const initBusinessCompaniesData = async () => {
    let businessCompanies = await getBusinessCompaniesData();
    console.log(businessCompanies);
    let businessCompaniesObject = Object.values(businessCompanies);
    setBusinessCompaniesArr(businessCompaniesObject);
    setFilteredBusinessCompaniesArr(businessCompaniesObject);
  };

  useEffect(() => {
    initBusinessCompaniesData();
  }, []);

  //delete business company by BusinessID
  const handleDeleteBusinessCompanies = async (BusinessID) => {
    await deleteBusinessCompanyFromDb(BusinessID);
    let newBusinessCompaniesArr = businessCompaniesArr.filter(
      (b) => b.BusinessID !== BusinessID
    );
    setFilteredBusinessCompaniesArr(newBusinessCompaniesArr);
  };

  //search the search input value and show the result in the table
  const handleSearch = (searchValue) => {
    //filter businessCompaniesArr by the Email.includes the searchValue
    let result = businessCompaniesArr.filter((business) =>
      business.Email.toLowerCase().includes(searchValue.toLowerCase())
    );
    //set the setFilteredBusinessCompaniesArr with the result
    setFilteredBusinessCompaniesArr(result);
  };

  return (
    <div className="business-container">
      <div>
        <h3>Business Companies</h3>
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
              <th scope="col">Business Name</th>
              <th scope="col">Email</th>
              <th scope="col"></th>
            </tr>
          </thead>
          {filteredBusinessCompaniesArr &&
          filteredBusinessCompaniesArr !== undefined ? (
            filteredBusinessCompaniesArr.map((b) => {
              let { BusinessID, BusinessName, Email, DeleteAnswer } = b;
              return (
                <tbody>
                  <tr>
                    <td>{BusinessName}</td>
                    <td>{Email}</td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-danger"
                        data-bs-toggle="modal"
                        data-bs-target={`#staticBackdrop${BusinessID}`}
                      >
                        Delete Business Company
                      </button>
                      <div
                        className="modal fade"
                        id={`staticBackdrop${BusinessID}`}
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
                                Delete Business Company
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
                                Are you sure you want to delete "{BusinessName}"{" "}
                                business company?
                              </div>
                            ) : (
                              <div className="modal-body">
                                The business company: "{BusinessName}" is linked
                                to other records in the system. If you delete
                                the business, the following information may be
                                deleted, and you will not be able to restore it.
                                Are you sure you want to delete the business?
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
                                  handleDeleteBusinessCompanies(BusinessID);
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
