import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import { getActiveCampaignsData } from "../../../Social Activist/services/active.campaigns.services";

export const AllActiveCampaignsReport = (props) => {
  const [activeCampaignsArr, setActiveCampaignsArr] = useState(undefined);

  //initialize the active campaigns
  const initActiveCampaignsData = async () => {
    let activeCampaigns = await getActiveCampaignsData();
    console.log(activeCampaigns);
    let activeCampaignsObject = Object.values(activeCampaigns);
    setActiveCampaignsArr(activeCampaignsObject);
  };

  useEffect(() => {
    initActiveCampaignsData();
  }, []);

  return (
    <div className="campaign-container">
      <div>
        <h3>All Active Campaigns Report</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Active Campaign ID</th>
              <th scope="col">Activist ID</th>
              <th scope="col">Campaign ID</th>
              <th scope="col">Campaign Name</th>
              <th scope="col">Hashtag</th>
              <th scope="col">MoneyEarned</th>
              <th scope="col">Twitter User Name</th>
              <th scope="col">Number Of Tweets</th>
            </tr>
          </thead>
          {activeCampaignsArr && activeCampaignsArr !== undefined ? (
            activeCampaignsArr.map((a) => {
              let {
                ActiveCampID,
                ActivistID,
                CampaignID,
                TwitterUserName,
                Hashtag,
                MoneyEarned,
                CampaignName,
                TweetsNumber,
              } = a;
              return (
                <tbody>
                  <tr>
                    <td>{ActiveCampID}</td>
                    <td>{ActivistID}</td>
                    <td>{CampaignID}</td>
                    <td>{CampaignName}</td>
                    <td>{Hashtag}</td>
                    <td>{MoneyEarned}</td>
                    <td>{TwitterUserName}</td>
                    <td>{TweetsNumber}</td>
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
