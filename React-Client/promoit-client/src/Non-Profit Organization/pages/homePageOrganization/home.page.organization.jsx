import React, { useState, useEffect, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { RingLoader } from "react-spinners";

import {
  deleteCampaignFromDb,
  getCampaignByORGEmail,
} from "../../services/campaign.services";
import { getOrganizationByEmail } from "../../services/organization.services";
import { OroganizationContext } from "../../context/organization.context";

import "./style.css";

export const HomePageOrganization = ({ Email }) => {
  const navigate = useNavigate();
  const [campaignsArr, setCampaignsArr] = useState(undefined);
  const { organizationContext, setOrganizationContext } =
    useContext(OroganizationContext);

  //initialize the organization context
  const initorganizationData = async () => {
    let organization = await getOrganizationByEmail(Email);
    console.log(organization);
    setOrganizationContext(organization);
  };

  //initialize all the campaigns for this organization (campaignsArr)
  const initCampaignsData = async () => {
    let campaigns = await getCampaignByORGEmail(Email);
    console.log(campaigns);
    let campaignsObject = Object.values(campaigns);
    setCampaignsArr(campaignsObject);
  };

  useEffect(() => {
    initorganizationData();
    initCampaignsData();
  }, []);

  //delete a campaign from DB by CampaignID
  const handleDeleteCampaign = async (CampaignID) => {
    await deleteCampaignFromDb(CampaignID);
    let newCampaignsArr = campaignsArr.filter(
      (s) => s.CampaignID !== CampaignID
    );
    setCampaignsArr(newCampaignsArr);
  };

  return (
    <div>
      {organizationContext && organizationContext !== undefined && (
        <div>
          {organizationContext.OrganizationName && (
            <h3>Welcome "{organizationContext.OrganizationName}"</h3>
          )}
          {organizationContext.Description && (
            <h5 className="description">
              Organization's description: "{organizationContext.Description}"
            </h5>
          )}
          <p className="p-info">
            In the table below are all the campaigns you have registered in the
            system
          </p>
        </div>
      )}
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Campaign Name</th>
              <th scope="col">Link To Landing Page</th>
              <th scope="col">Hashtag</th>
              <th scope="col"></th>
              <th scope="col"></th>
            </tr>
          </thead>
          {campaignsArr && campaignsArr !== undefined ? (
            campaignsArr.map((c) => {
              let {
                CampaignID,
                CampaignName,
                LinkToLandingPage,
                Hashtag,
                DeleteAnswer,
              } = c;
              return (
                <tbody>
                  <tr>
                    <td>{CampaignName}</td>
                    <td>
                      <Link>{LinkToLandingPage}</Link>
                    </td>
                    <td>{Hashtag}</td>

                    <td>
                      <button
                        className="btn btn-primary"
                        onClick={() => {
                          navigate("/edit-campaign", {
                            state: { campaign: c },
                          });
                        }}
                      >
                        Edit Campaign
                      </button>
                    </td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-danger"
                        data-bs-toggle="modal"
                        data-bs-target={`#staticBackdrop${CampaignID}`}
                      >
                        Delete Campaign
                      </button>
                      <div
                        className="modal fade"
                        id={`staticBackdrop${CampaignID}`}
                        data-bs-backdrop="static"
                        data-bs-keyboard="false"
                        tabIndex="-1"
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
                                Delete Campaign
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
                                Are you sure you want to delete "{CampaignName}"{" "}
                                campaign?
                              </div>
                            ) : (
                              <div className="modal-body">
                                The campaign: "{CampaignName}" is linked to
                                other records in the system. If you delete the
                                campaign, the following information may be
                                deleted, and you will not be able to restore it.
                                Are you sure you want to delete the campaign?
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
                                  handleDeleteCampaign(CampaignID);
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
