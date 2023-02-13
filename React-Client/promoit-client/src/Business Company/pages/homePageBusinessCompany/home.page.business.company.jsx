import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { getBusinessCompanyByEmail } from "../../services/business.company.services";
import { getCampaignsAndOrganizationsData } from "../../../Non-Profit Organization/services/campaign.services";
import { DonateProduct } from "./../../components/donateProduct/donate.product.component";
import { BusinessContext } from "./../../context/business.context";

import "./style.css";

export const HomePageBusinessCompany = ({ Email }) => {
  const { businessContext, setBusinessContext } = useContext(BusinessContext);
  const [campaignsArr, setCampaignsArr] = useState(undefined);

  //initialize the business context
  const initBusinessCompanyData = async () => {
    let company = await getBusinessCompanyByEmail(Email);
    console.log(company);
    setBusinessContext(company);
  };

  //initialize campaignsArr
  const initCampaignsData = async () => {
    let campaigns = await getCampaignsAndOrganizationsData();
    console.log(campaigns);
    let campaignsObject = Object.values(campaigns);
    setCampaignsArr(campaignsObject);
  };

  useEffect(() => {
    initBusinessCompanyData();
    initCampaignsData();
  }, []);

  return (
    <div className="home-container">
      {businessContext && businessContext !== undefined && (
        <div>
          {businessContext.BusinessName && (
            <h3>Welcome "{businessContext.BusinessName}"</h3>
          )}
          <p className="p-info">
            In the table below there are all the campaigns registered in our
            system
            <br /> Please select a campaign and donate a product to it
          </p>
        </div>
      )}
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Organization Name</th>
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
                    <td>{CampaignName}</td>
                    <td>
                      <Link>{LinkToLandingPage}</Link>
                    </td>
                    <td>{Hashtag}</td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-primary"
                        data-bs-toggle="modal"
                        data-bs-target={`#staticBackdrop${CampaignID}`}
                      >
                        Make A Donation
                      </button>
                      <DonateProduct
                        CampaignID={CampaignID}
                        BusinessID={businessContext.BusinessID}
                      />
                    </td>
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
