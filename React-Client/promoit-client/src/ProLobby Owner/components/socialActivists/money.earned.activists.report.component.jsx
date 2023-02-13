import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import { getMostMoneyEarned } from "../../../Social Activist/services/activist.services";

export const MoneyEarnedActivistsReport = (props) => {
  const [activistArr, setActivistArr] = useState(undefined);

  //initialize the activistArr
  const initMoneyEarnesActivistsData = async () => {
    let activists = await getMostMoneyEarned();
    console.log(activists);
    let activistsObject = Object.values(activists);
    setActivistArr(activistsObject);
  };

  useEffect(() => {
    initMoneyEarnesActivistsData();
  }, []);

  return (
    <div className="organization-container">
      <div>
        <h3>
          A Report On The Money That Social Activists Earned From High To Low
        </h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Activist ID</th>
              <th scope="col">Full Name</th>
              <th scope="col">Email</th>
              <th scope="col">Total Of Earned Money and Tweets</th>
            </tr>
          </thead>
          {activistArr && activistArr !== undefined ? (
            activistArr.map((a) => {
              let { ActivistID, FullName, Email, TotalMoney } = a;
              return (
                <tbody>
                  <tr>
                    <td>{ActivistID}</td>
                    <td>{FullName}</td>
                    <td>{Email}</td>
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
