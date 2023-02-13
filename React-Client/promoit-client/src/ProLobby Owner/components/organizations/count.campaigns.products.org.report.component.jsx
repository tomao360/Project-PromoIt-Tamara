import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { getCountOfCampaignsProducts } from "../../../Non-Profit Organization/services/organization.services";

export const CountCampaignsProductsOrgReport = (props) => {
  const [organizationArr, setOrganizationArr] = useState(undefined);

  //initialize the count of campaigns and products => organizationArr
  const initOrganizationsData = async () => {
    let organizations = await getCountOfCampaignsProducts();
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
        <h3>
          Amount Of Campaigns And Donated Products To Non-Profit Organizations
          Report
        </h3>
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
              <th scope="col">Total Campaigns</th>
              <th scope="col">Total Products</th>
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
                TotalCampaigns,
                TotalProducts,
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
                    <td>{TotalCampaigns}</td>
                    <td>{TotalProducts}</td>
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
