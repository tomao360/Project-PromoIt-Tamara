import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { getCampaignsAndOrganizationsData } from "../../../Non-Profit Organization/services/campaign.services";

export const AllCampaignsReport = (props) => {
  const [campaignsArr, setCampaignsArr] = useState(undefined);

  //initialize the campaignsArr
  const initCampaignsData = async () => {
    let campaigns = await getCampaignsAndOrganizationsData();
    console.log(campaigns);
    let campaignsObject = Object.values(campaigns);
    setCampaignsArr(campaignsObject);
  };

  useEffect(() => {
    initCampaignsData();
  }, []);

  return (
    <div className="campaign-container">
      <div>
        <h3>All Campaigns Report</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Organization Name</th>
              <th scope="col">Campaign ID</th>
              <th scope="col">Campaign Name</th>
              <th scope="col">Link To Landing Page</th>
              <th scope="col">Hashtag</th>
              <th scope="col"></th>
            </tr>
          </thead>
          {campaignsArr && campaignsArr !== undefined ? (
            campaignsArr.map((c, index) => {
              let {
                CampaignID,
                CampaignName,
                LinkToLandingPage,
                Hashtag,
                OrganizationName,
                Description,
              } = c;
              return (
                <tbody>
                  <tr>
                    <td>
                      <p>
                        <a
                          className="btn btn-light"
                          data-bs-toggle="collapse"
                          href={`#collapseExample${index}`}
                          role="button"
                          aria-expanded="false"
                          aria-controls={`collapseExample${index}`}
                        >
                          {OrganizationName}
                        </a>
                      </p>
                    </td>
                    <td>{CampaignID}</td>
                    <td>{CampaignName}</td>
                    <td>
                      <Link>{LinkToLandingPage}</Link>
                    </td>
                    <td>{Hashtag}</td>
                  </tr>
                  <tr>
                    <div
                      className="collapse card-description"
                      id={`collapseExample${index}`}
                    >
                      <td className="card card-body">
                        Organization's Description: {Description}
                      </td>
                    </div>
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
