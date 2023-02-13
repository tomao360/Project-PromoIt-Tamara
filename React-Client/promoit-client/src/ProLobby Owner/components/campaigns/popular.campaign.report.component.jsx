import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { getPopularCampaign } from "../../../Non-Profit Organization/services/campaign.services";

export const PopularCampaignReport = (props) => {
  const [campaignsArr, setCampaignsArr] = useState(undefined);

  //initialize the campaignsArr
  const initPopularCampaignsData = async () => {
    let campaigns = await getPopularCampaign();
    console.log(campaigns);
    let campaignsObject = Object.values(campaigns);
    setCampaignsArr(campaignsObject);
  };

  useEffect(() => {
    initPopularCampaignsData();
  }, []);

  return (
    <div className="campaign-container">
      <div>
        <h3>Campaigns Popularity Report From High To Low</h3>
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
              <th scope="col">Total Activists Promotes The Campaign</th>
              <th scope="col">Total Tweets</th>
              <th scope="col">Total Products Donated To The Campaign</th>
            </tr>
          </thead>
          {campaignsArr && campaignsArr !== undefined ? (
            campaignsArr.map((c, index) => {
              let {
                OrganizationName,
                CampaignID,
                CampaignName,
                LinkToLandingPage,
                Hashtag,
                TotalActivists,
                TotalTweets,
                TotalProducts,
              } = c;
              return (
                <tbody>
                  <tr>
                    <td>{OrganizationName}</td>
                    <td>{CampaignID}</td>
                    <td>{CampaignName}</td>
                    <td>
                      <Link>{LinkToLandingPage}</Link>
                    </td>
                    <td>{Hashtag}</td>
                    <td>{TotalActivists}</td>
                    <td>{TotalTweets}</td>
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
