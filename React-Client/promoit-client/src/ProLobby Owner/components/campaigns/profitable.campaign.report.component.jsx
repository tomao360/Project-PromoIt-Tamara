import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { getProfitableCampaign } from "../../../Non-Profit Organization/services/campaign.services";

export const ProfitableCampaignReport = (props) => {
  const [campaignsArr, setCampaignsArr] = useState(undefined);

  //initialize the campaignsArr
  const initProfitableCampaignsData = async () => {
    let campaigns = await getProfitableCampaign();
    console.log(campaigns);
    let campaignsObject = Object.values(campaigns);
    setCampaignsArr(campaignsObject);
  };

  useEffect(() => {
    initProfitableCampaignsData();
  }, []);

  return (
    <div className="campaign-container">
      <div>
        <h3>Campaigns Profitability Report From High To Low</h3>
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
              <th scope="col">Total Money Earned</th>
            </tr>
          </thead>
          {campaignsArr && campaignsArr !== undefined ? (
            campaignsArr.map((c) => {
              let {
                OrganizationName,
                CampaignID,
                CampaignName,
                LinkToLandingPage,
                Hashtag,
                TotalMoney,
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
                    <td>{TotalMoney}</td>
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
