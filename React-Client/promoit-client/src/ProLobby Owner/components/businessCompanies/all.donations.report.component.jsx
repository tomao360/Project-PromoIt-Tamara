import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import { getDonatedProductsData } from "../../../Business Company/services/donated.product.services";

export const AllDonationsReport = (props) => {
  const [productsArr, setProductsArr] = useState(undefined);

  //initialize donated products
  const initDonatedProductsData = async () => {
    let products = await getDonatedProductsData();
    console.log(products);
    let productsObject = Object.values(products);
    setProductsArr(productsObject);
  };

  useEffect(() => {
    initDonatedProductsData();
  }, []);

  return (
    <div className="business-container">
      <div>
        <h3>All Donated Products Report</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Business ID</th>
              <th scope="col">Product ID</th>
              <th scope="col">Product Name</th>
              <th scope="col">Price</th>
              <th scope="col">Bought</th>
              <th scope="col">Shipped</th>
            </tr>
          </thead>
          {productsArr && productsArr !== undefined ? (
            productsArr.map((p) => {
              let {
                ProductID,
                ProductName,
                Price,
                BusinessID,
                Bought,
                Shipped,
              } = p;
              return (
                <tbody>
                  <tr>
                    <td>{BusinessID}</td>
                    <td>{ProductID}</td>
                    <td>{ProductName}</td>
                    <td>{Price}</td>
                    <td>{Bought}</td>
                    <td>{Shipped}</td>
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
