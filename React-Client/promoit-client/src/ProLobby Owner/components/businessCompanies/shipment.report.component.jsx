import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import { getShipmentsData } from "../../../Business Company/services/business.company.services";

export const ShipmentReport = (props) => {
  const [shipmentArr, setShipmentArr] = useState(undefined);

  //initialize shipmentArr
  const initShipmentData = async () => {
    let shipments = await getShipmentsData();
    console.log(shipments);
    let shipmentsObject = Object.values(shipments);
    setShipmentArr(shipmentsObject);
  };

  useEffect(() => {
    initShipmentData();
  }, []);

  return (
    <div className="business-container">
      <div>
        <h3>Shipment Report</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Product ID</th>
              <th scope="col">Full Name</th>
              <th scope="col">Email</th>
              <th scope="col">Address</th>
              <th scope="col">Phone Number</th>
            </tr>
          </thead>
          {shipmentArr && shipmentArr !== undefined ? (
            shipmentArr.map((s) => {
              return (
                <tbody>
                  <tr>
                    <td>{s.ProductID}</td>
                    <td>{s.FullName}</td>
                    <td>{s.Email}</td>
                    <td>{s.Address}</td>
                    <td>{s.PhoneNumber}</td>
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
