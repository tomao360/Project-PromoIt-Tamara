import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";
import { ToastContainer } from "react-toastify";

import { toastSuccess, toastError } from "../../../constant/toastify";
import { getCampaignsAndOrganizationsData } from "../../../Non-Profit Organization/services/campaign.services";
import { getActivistByEmail } from "../../services/activist.services";
import { ActivistContext } from "../../context/activist.context";
import { addActiveCampaignToDb } from "../../services/active.campaigns.services";

import "./style.css";

export const HomePageSocialActivist = ({ Email }) => {
  const { activistContext, setActivistContext } = useContext(ActivistContext);
  const [campaignsArr, setCampaignsArr] = useState(undefined);
  const [userName, setUserName] = useState("");

  //initialize the social activist context
  const initActivistData = async () => {
    let activist = await getActivistByEmail(Email);
    console.log(activist);
    setActivistContext(activist);
  };

  //initialize all the campaigns (campaignsArr)
  const initCampaignsData = async () => {
    let campaigns = await getCampaignsAndOrganizationsData();
    console.log(campaigns);
    let campaignsObject = Object.values(campaigns);
    setCampaignsArr(campaignsObject);
  };

  useEffect(() => {
    initActivistData();
    initCampaignsData();
  }, []);

  //add the campaign that the activist chose to the database
  const handleChooseCampaign = async (
    ActivistID,
    CampaignID,
    Hashtag,
    CampaignName
  ) => {
    let activeCampaign = {
      ActivistID: ActivistID,
      CampaignID: CampaignID,
      TwitterUserName: userName,
      Hashtag: Hashtag,
      MoneyEarned: 0,
      CampaignName: CampaignName,
    };
    console.log(userName, "userName");
    //if the userName is empty or includes @ returns error
    if (!userName || userName.includes("@")) {
      toastError("The user name field is not a valid user name");
      return;
    } else {
      await addActiveCampaignToDb(activeCampaign);
      toastSuccess(
        "Your selection has been successfully entered into the system"
      );
      setUserName({});
      document.querySelectorAll("input").forEach((input) => (input.value = ""));
    }
  };

  return (
    <div className="home-container">
      <ToastContainer />
      {activistContext && activistContext !== undefined && (
        <div>
          {activistContext.FullName && (
            <h3>Welcome {activistContext.FullName}</h3>
          )}
          <p className="p-info">
            In the table below there are all the campaigns registered in our
            system
            <br /> Please enter your Twitter user name (without @) in the input
            below and then select a campaign to promote it on Twitter
          </p>
          <div className="mb-3 twitter-input">
            <label htmlFor="basic-url" className="form-label twitter-label">
              Twitter User Name
            </label>
            <input
              type="text"
              className="form-control"
              id="basic-url"
              aria-describedby="basic-addon3"
              placeholder="user name without @"
              onChange={(o) => {
                setUserName(o.target.value);
              }}
            />
          </div>
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
                        className="btn btn-primary"
                        onClick={() => {
                          handleChooseCampaign(
                            activistContext.ActivistID,
                            CampaignID,
                            Hashtag,
                            CampaignName
                          );
                        }}
                      >
                        Choose
                      </button>
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
