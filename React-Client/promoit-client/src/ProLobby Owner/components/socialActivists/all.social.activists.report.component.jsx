import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import { getActivistsData } from "../../../Social Activist/services/activist.services";

export const AllSocialActivistsReport = (props) => {
  const [activistArr, setActivistArr] = useState(undefined);

  //initialize the social activists data
  const initActivistsData = async () => {
    let activists = await getActivistsData();
    console.log(activists);
    let activistsObject = Object.values(activists);
    setActivistArr(activistsObject);
  };

  useEffect(() => {
    initActivistsData();
  }, []);

  return (
    <div className="organization-container">
      <div>
        <h3>All Social Activists Report</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Activist ID</th>
              <th scope="col">Full Name</th>
              <th scope="col">Email</th>
              <th scope="col">Address</th>
              <th scope="col">PhoneNumber</th>
            </tr>
          </thead>
          {activistArr && activistArr !== undefined ? (
            activistArr.map((a) => {
              let { FullName, Email, Address, PhoneNumber, ActivistID } = a;
              return (
                <tbody>
                  <tr>
                    <td>{ActivistID}</td>
                    <td>{FullName}</td>
                    <td>{Email}</td>
                    <td>{Address}</td>
                    <td>{PhoneNumber}</td>
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
