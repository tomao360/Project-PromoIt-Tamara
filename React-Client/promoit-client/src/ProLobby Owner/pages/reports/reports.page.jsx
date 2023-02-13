import React, { useState } from "react";
import { ToastContainer } from "react-toastify";

import { toastError } from "../../../constant/toastify";
import {
  AllOrganizationsReport,
  AllCampaignsReport,
  AllBusinessCompaniesReport,
  AllSocialActivistsReport,
  CountCampaignsProductsOrgReport,
  AllActiveCampaignsReport,
  PopularCampaignReport,
  ProfitableCampaignReport,
  AllDonationsReport,
  CountDonationsReport,
  ShipmentReport,
  CountCampaignsActivistsReport,
  MoneyEarnedActivistsReport,
} from "../../components";

import "./style.css";

export const Reports = (props) => {
  const [selectValueUser, setSelecteValueUser] = useState("");
  const [selectValueReport, setSelecteValueReport] = useState("");
  const [generateReport, setGenerateReport] = useState(undefined);

  //handle generating the appropriate component based on the selectValueUser and selectValueReport
  const handleGenerateReport = () => {
    if (selectValueUser === "1" && selectValueReport === "1") {
      setGenerateReport(<AllOrganizationsReport />);
    } else if (selectValueUser === "1" && selectValueReport === "2") {
      setGenerateReport(<AllCampaignsReport />);
    } else if (selectValueUser === "1" && selectValueReport === "3") {
      setGenerateReport(<AllBusinessCompaniesReport />);
    } else if (selectValueUser === "1" && selectValueReport === "4") {
      setGenerateReport(<AllSocialActivistsReport />);
    } else if (selectValueUser === "2" && selectValueReport === "1") {
      setGenerateReport(<CountCampaignsProductsOrgReport />);
    } else if (selectValueUser === "2" && selectValueReport === "2") {
      setGenerateReport(<AllActiveCampaignsReport />);
    } else if (selectValueUser === "2" && selectValueReport === "3") {
      setGenerateReport(<PopularCampaignReport />);
    } else if (selectValueUser === "2" && selectValueReport === "4") {
      setGenerateReport(<ProfitableCampaignReport />);
    } else if (selectValueUser === "3" && selectValueReport === "1") {
      setGenerateReport(<AllDonationsReport />);
    } else if (selectValueUser === "3" && selectValueReport === "2") {
      setGenerateReport(<CountDonationsReport />);
    } else if (selectValueUser === "3" && selectValueReport === "3") {
      setGenerateReport(<ShipmentReport />);
    } else if (selectValueUser === "4" && selectValueReport === "1") {
      setGenerateReport(<CountCampaignsActivistsReport />);
    } else if (selectValueUser === "4" && selectValueReport === "2") {
      setGenerateReport(<MoneyEarnedActivistsReport />);
    } else {
      toastError("Please select a value to generate a report for");
    }
  };

  //show the relevant options based on the selectValueUser
  const handleOption = () => {
    if (selectValueUser === "1") {
      return (
        <>
          <option value="1">all non-profit organizations</option>
          <option value="2">all campaigns</option>
          <option value="3">all business companies</option>
          <option value="4">all social activists</option>
        </>
      );
    } else if (selectValueUser === "2") {
      return (
        <>
          <option value="1">amount of campaigns and donated products</option>
          <option value="2">active campaigns</option>
          <option value="3">campaign's popularity</option>
          <option value="4">campaign's profitability</option>
        </>
      );
    } else if (selectValueUser === "3") {
      return (
        <>
          <option value="1">all donated products</option>
          <option value="2">amount of donations</option>
          <option value="3">shipping status</option>
        </>
      );
    } else if (selectValueUser === "4") {
      return (
        <>
          <option value="1">amount of campaigns promoted</option>
          <option value="2">earned money and tweets</option>
        </>
      );
    }
  };

  return (
    <div className="report-container">
      <ToastContainer />
      <div className="select-container">
        <div className="select-header">
          <div className="title first">Create a report to</div>
          <select
            className="form-select"
            aria-label="Default select example"
            onChange={(e) => {
              setSelecteValueUser(e.target.value);
            }}
          >
            <option selected>select a user role</option>
            <option value="1">proLobby owner</option>
            <option value="2">non-profit organization</option>
            <option value="3">business company</option>
            <option value="4">social activist</option>
          </select>
        </div>
        <div className="select-header">
          <div className="title">Report Type</div>
          <select
            className="form-select"
            aria-label="Default select example"
            onChange={(e) => {
              setSelecteValueReport(e.target.value);
            }}
          >
            <option selected>choose the report you need</option>
            {handleOption()}
          </select>
        </div>
        <div>
          <button
            className="btn btn-primary select-button"
            onClick={handleGenerateReport}
            disabled={!selectValueUser || !selectValueReport}
          >
            Generate Report
          </button>
        </div>
      </div>
      <div className="report">{generateReport}</div>
    </div>
  );
};
