import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import {
  getActivistsData,
  deleteActivistFromDb,
} from "../../../Social Activist/services/activist.services";

export const SocialActivistsDelete = (props) => {
  const [activistArr, setActivistArr] = useState(undefined);
  const [filteredActivistArr, setFilteredActivistArr] = useState(undefined);

  //initialize the social activists data
  const initActivistsData = async () => {
    let activists = await getActivistsData();
    console.log(activists);
    let activistsObject = Object.values(activists);
    setActivistArr(activistsObject);
    setFilteredActivistArr(activistsObject);
  };

  useEffect(() => {
    initActivistsData();
  }, []);

  //delete social activist by ActivistID
  const handleDeleteActivist = async (ActivistID) => {
    await deleteActivistFromDb(ActivistID);
    let newActivistsArr = activistArr.filter(
      (a) => a.ActivistID !== ActivistID
    );
    setFilteredActivistArr(newActivistsArr);
  };

  //search the search input value and show the result in the table
  const handleSearch = (searchValue) => {
    //filter activistArr by the Email.includes the searchValue
    let result = activistArr.filter((activist) =>
      activist.Email.toLowerCase().includes(searchValue.toLowerCase())
    );
    //set the setFilteredActivistArr with the result
    setFilteredActivistArr(result);
  };

  return (
    <div className="organization-container">
      <div>
        <h3>Social Activists</h3>
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
              <th scope="col">Full Name</th>
              <th scope="col">Email</th>
              <th scope="col">Address</th>
              <th scope="col">PhoneNumber</th>
              <th scope="col"></th>
            </tr>
          </thead>
          {filteredActivistArr && filteredActivistArr !== undefined ? (
            filteredActivistArr.map((a) => {
              let {
                ActivistID,
                FullName,
                Email,
                Address,
                PhoneNumber,
                DeleteAnswer,
              } = a;
              return (
                <tbody>
                  <tr>
                    <td>{FullName}</td>
                    <td>{Email}</td>
                    <td>{Address}</td>
                    <td>{PhoneNumber}</td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-danger"
                        data-bs-toggle="modal"
                        data-bs-target={`#staticBackdrop${ActivistID}`}
                      >
                        Delete Activist
                      </button>
                      <div
                        className="modal fade"
                        id={`staticBackdrop${ActivistID}`}
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
                                Delete Activist
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
                                Are you sure you want to delete "{FullName}"{" "}
                                social activist?
                              </div>
                            ) : (
                              <div className="modal-body">
                                The social activist: "{FullName}" is linked to
                                other records in the system. If you delete his
                                account, the following information may be
                                deleted, and you will not be able to restore it.
                                Are you sure you want to delete his account?
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
                                  handleDeleteActivist(ActivistID);
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
