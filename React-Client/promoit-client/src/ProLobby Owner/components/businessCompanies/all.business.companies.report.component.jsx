import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import { getBusinessCompaniesData } from "../../../Business Company/services/business.company.services";

export const AllBusinessCompaniesReport = (props) => {
  const [businessCompaniesArr, setBusinessCompaniesArr] = useState(undefined);

  //initialize business companies
  const initBusinessCompaniesData = async () => {
    let businessCompanies = await getBusinessCompaniesData();
    console.log(businessCompanies);
    let businessCompaniesObject = Object.values(businessCompanies);
    setBusinessCompaniesArr(businessCompaniesObject);
  };

  useEffect(() => {
    initBusinessCompaniesData();
  }, []);

  return (
    <div className="business-container">
      <div>
        <h3 className="header">All Business Companies Report</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Business ID</th>
              <th scope="col">Business Name</th>
              <th scope="col">Email</th>
            </tr>
          </thead>
          {businessCompaniesArr && businessCompaniesArr !== undefined ? (
            businessCompaniesArr.map((b) => {
              let { BusinessID, BusinessName, Email } = b;
              return (
                <tbody>
                  <tr>
                    <td>{BusinessID}</td>
                    <td>{BusinessName}</td>
                    <td>{Email}</td>
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
