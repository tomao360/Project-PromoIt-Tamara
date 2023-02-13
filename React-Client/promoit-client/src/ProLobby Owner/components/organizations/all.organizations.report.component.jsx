import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { getOrganizationsData } from "../../../Non-Profit Organization/services/organization.services";

export const AllOrganizationsReport = (props) => {
  const [organizationArr, setOrganizationArr] = useState(undefined);

  //initialize organization data
  const initOrganizationsData = async () => {
    let organizations = await getOrganizationsData();
    console.log(organizations);
    let organizationsObject = Object.values(organizations);
    setOrganizationArr(organizationsObject);
  };

  useEffect(() => {
    initOrganizationsData();
  }, []);

  return (
    <div className="organization-container">
      <div>
        <h3>All Non-Profit Organizations Report</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Organization ID</th>
              <th scope="col">Organization Name</th>
              <th scope="col">Email</th>
              <th scope="col">Link To Website</th>
              <th scope="col">Description</th>
            </tr>
          </thead>
          {organizationArr && organizationArr !== undefined ? (
            organizationArr.map((o) => {
              let {
                OrganizationID,
                OrganizationName,
                Email,
                LinkToWebsite,
                Description,
              } = o;
              return (
                <tbody>
                  <tr>
                    <td>{OrganizationID}</td>
                    <td>{OrganizationName}</td>
                    <td>{Email}</td>
                    <td>
                      <Link>{LinkToWebsite}</Link>
                    </td>
                    <td>{Description}</td>
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
