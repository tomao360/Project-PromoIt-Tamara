import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import { getCountOfDonations } from "../../../Business Company/services/business.company.services";

export const CountDonationsReport = (props) => {
  const [donationsCountArr, setDonationsCountArr] = useState(undefined);

  //initialize the donationsCountArr
  const initDonationsCountData = async () => {
    let donationsCount = await getCountOfDonations();
    console.log(donationsCount);
    let donationsCountObject = Object.values(donationsCount);
    setDonationsCountArr(donationsCountObject);
  };

  useEffect(() => {
    initDonationsCountData();
  }, []);

  return (
    <div className="business-container">
      <div>
        <h3>Amount Of Donated Products Report</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Business ID</th>
              <th scope="col">Business Name</th>
              <th scope="col">Email</th>
              <th scope="col">Total Donated Products</th>
            </tr>
          </thead>
          {donationsCountArr && donationsCountArr !== undefined ? (
            donationsCountArr.map((b) => {
              let { BusinessID, BusinessName, Email, TotalProducts } = b;
              return (
                <tbody>
                  <tr>
                    <td>{BusinessID}</td>
                    <td>{BusinessName}</td>
                    <td>{Email}</td>
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
