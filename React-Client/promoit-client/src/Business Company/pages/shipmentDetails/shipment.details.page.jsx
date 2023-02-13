import React, { useState, useEffect, useContext } from "react";
import { RingLoader } from "react-spinners";

import { getShipmentsDataByBusinessID } from "../../services/business.company.services";
import { updateDonatedProductShipped } from "../../services/donated.product.services";
import { BusinessContext } from "../../context/business.context";

export const ShipmentDetails = (props) => {
  const { businessContext } = useContext(BusinessContext);
  const [shipmentArr, setShipmentArr] = useState(undefined);
  const [isDataFetched, setIsDataFetched] = useState(false);

  //initialize shipmentArr
  const initShipmentData = async () => {
    let shipments = await getShipmentsDataByBusinessID(
      businessContext.BusinessID
    );
    console.log(shipments);
    let shipmentsObject = Object.values(shipments);
    setShipmentArr(shipmentsObject);
  };

  useEffect(() => {
    initShipmentData();

    //if isDataFetched is set to false then render the component
    if (!isDataFetched) {
      initShipmentData();
      setIsDataFetched(true);
    }
  }, [isDataFetched]);

  //update donated product shipment status
  const handleShipProduct = async (product) => {
    let donatedProduct = {
      ProductID: product.ProductID,
      ProductName: product.ProductName,
      Price: product.Price,
      BusinessID: product.BusinessID,
      CampaignID: product.CampaignID,
      Bought: product.Bought,
      Shipped: product.Shipped,
    };
    await updateDonatedProductShipped(donatedProduct, donatedProduct.ProductID);
    //after sending to the database set the isDataFetched to false
    setIsDataFetched(false);
  };

  return (
    <div className="home-container">
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Product ID</th>
              <th scope="col">Full Name</th>
              <th scope="col">Email</th>
              <th scope="col">Address</th>
              <th scope="col">Phone Number</th>
              <th scope="col"></th>
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
                    <td>
                      <button
                        type="button"
                        className="btn btn-primary card-button"
                        data-bs-toggle="modal"
                        data-bs-target="#exampleModal"
                        onClick={() => {
                          handleShipProduct(s);
                        }}
                      >
                        Ship Product
                      </button>

                      <div
                        className="modal fade"
                        id="exampleModal"
                        tabIndex="-1"
                        aria-labelledby="exampleModalLabel"
                        aria-hidden="true"
                      >
                        <div className="modal-dialog">
                          <div className="modal-content">
                            <div className="modal-header">
                              <button
                                type="button"
                                className="btn-close"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                              ></button>
                            </div>
                            <div className="modal-body">
                              <h4>
                                The product is shipped to the customer! <br />
                              </h4>
                            </div>
                            <div className="modal-footer">
                              <button
                                type="button"
                                className="btn btn-secondary"
                                data-bs-dismiss="modal"
                              >
                                Close
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
