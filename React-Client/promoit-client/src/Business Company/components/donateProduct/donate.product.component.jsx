import React, { useState } from "react";
import { ToastContainer } from "react-toastify";

import { toastSuccess, toastError } from "../../../constant/toastify";
import { addDonatedProductToDb } from "../../services/donated.product.services";

export const DonateProduct = ({ CampaignID, BusinessID }) => {
  console.log("Donate Product", CampaignID);
  const [donatProduct, setDonateProduct] = useState({
    ProductName: "",
    Price: "",
    BusinessID: 0,
    CampaignID: 0,
  });
  const [amount, setAmount] = useState(0);

  //add donated product to database
  const handleAddDonatedProduct = async () => {
    donatProduct.Price = parseFloat(donatProduct.Price);
    donatProduct.BusinessID = parseInt(BusinessID);
    donatProduct.CampaignID = parseInt(CampaignID);
    let json = donatProduct;
    //validation to check if the required fields are filled
    if (!donatProduct.ProductName) {
      toastError("You Must enter product name");
    } else if (!donatProduct.Price) {
      toastError("You Must enter price");
    } else if (amount < 1) {
      toastError("You Must enter enter amount");
    } else {
      console.log(json);
      //loop to add the product to the database the number of times specified in the amount
      for (let i = 0; i < amount; i++) {
        await addDonatedProductToDb(json);
      }
      toastSuccess("We have successfully donated the product to the campaign");
      setDonateProduct({});
      document.querySelectorAll("input").forEach((input) => (input.value = ""));
    }
  };

  return (
    <div>
      <ToastContainer />
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
              <h1 className="modal-title fs-5" id="staticBackdropLabel">
                Donate A Product
              </h1>
              <button
                type="button"
                className="btn-close"
                data-bs-dismiss="modal"
                aria-label="Close"
              ></button>
            </div>
            <div className="modal-body">
              <div className="mb-3 ">
                <label htmlFor="basic-url" className="form-label donate-label">
                  Product Name
                </label>
                <input
                  type="text"
                  className="form-control"
                  id="basic-url"
                  aria-describedby="basic-addon3"
                  placeholder="Product Name"
                  onChange={(o) => {
                    setDonateProduct({
                      ...donatProduct,
                      ProductName: o.target.value,
                    });
                  }}
                />
              </div>
              <div className="mb-3">
                <label htmlFor="basic-url" className="form-label">
                  Price
                </label>
                <input
                  type="text"
                  className="form-control"
                  id="basic-url"
                  aria-describedby="basic-addon3"
                  placeholder="Price in dollars"
                  onChange={(o) => {
                    setDonateProduct({
                      ...donatProduct,
                      Price: o.target.value,
                    });
                  }}
                />
              </div>
              <div className="mb-3">
                <label htmlFor="basic-url" className="form-label">
                  Amount
                </label>
                <input
                  type="number"
                  className="form-control"
                  id="basic-url"
                  aria-describedby="basic-addon3"
                  placeholder="Amount"
                  onChange={(o) => {
                    setAmount(o.target.value);
                  }}
                />
              </div>
            </div>
            <div className="modal-footer">
              <button
                type="button"
                className="btn btn-secondary"
                data-bs-dismiss="modal"
              >
                Close
              </button>
              <button
                type="button"
                className="btn btn-primary"
                onClick={handleAddDonatedProduct}
              >
                Donate
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
