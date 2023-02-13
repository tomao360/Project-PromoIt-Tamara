import React, { useState, useEffect, useContext } from "react";
import { RingLoader } from "react-spinners";

import { getDonatedProductsByBusinessID } from "../../services/donated.product.services";
import { BusinessContext } from "../../context/business.context";

import "./style.css";

export const Donations = (props) => {
  const { businessContext } = useContext(BusinessContext);
  console.log(businessContext.BusinessID);
  const [productsArr, setProductsArr] = useState(undefined);
  console.log(productsArr, "productsArr");

  //initialize products array (productsArr)
  const initProductsData = async () => {
    let products = await getDonatedProductsByBusinessID(
      businessContext.BusinessID
    );
    console.log(products);
    let productsObject = Object.values(products);
    console.log(products);
    setProductsArr(productsObject);
  };

  useEffect(() => {
    initProductsData();
  }, []);

  return (
    <div className="donations-container">
      <div>
        <h3 className="header">All The Donated Products</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Product Name</th>
              <th scope="col">Price</th>
              <th scope="col">Campaign Name</th>
              <th scope="col">Organization Name</th>
              <th scope="col">Bought</th>
              <th scope="col">Shipped</th>
              <th scope="col"></th>
            </tr>
          </thead>
          {productsArr && productsArr !== undefined ? (
            productsArr.map((p) => {
              let {
                ProductName,
                Price,
                CampaignName,
                OrganizationName,
                Bought,
                Shipped,
              } = p;
              return (
                <tbody>
                  <tr>
                    <td>{ProductName}</td>
                    <td>{Price}</td>
                    <td>{CampaignName}</td>
                    <td>{OrganizationName}</td>
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
