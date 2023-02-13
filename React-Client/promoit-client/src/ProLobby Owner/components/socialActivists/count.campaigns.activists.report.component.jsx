import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import { getMostPromotedCampaigns } from "../../../Social Activist/services/activist.services";

export const CountCampaignsActivistsReport = (props) => {
  const [activistArr, setActivistArr] = useState(undefined);

  //initialize the activistArr
  const initPromotedCampaignsActivistsData = async () => {
    let activists = await getMostPromotedCampaigns();
    console.log(activists);
    let activistsObject = Object.values(activists);
    setActivistArr(activistsObject);
  };

  useEffect(() => {
    initPromotedCampaignsActivistsData();
  }, []);

  return (
    <div className="organization-container">
      <div>
        <h3>
          A Report On The Amount Of Campaigns That A Social Activist Promotes
          From High To Low
        </h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Activist ID</th>
              <th scope="col">Full Name</th>
              <th scope="col">Email</th>
              <th scope="col">Total Of Promoted Campaigns</th>
            </tr>
          </thead>
          {activistArr && activistArr !== undefined ? (
            activistArr.map((a) => {
              let { ActivistID, FullName, Email, TotalCampaigns } = a;
              return (
                <tbody>
                  <tr>
                    <td>{ActivistID}</td>
                    <td>{FullName}</td>
                    <td>{Email}</td>
                    <td>{TotalCampaigns}</td>
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
