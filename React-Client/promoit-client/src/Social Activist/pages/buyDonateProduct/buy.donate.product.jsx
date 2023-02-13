import React, { useState, useEffect, useContext } from "react";
import { RingLoader } from "react-spinners";

import { ActivistContext } from "../../context/activist.context";
import {
  getActivieCampaignsByActivistID,
  getDonatedProductsByActivistID,
  updateActiveCampaignSubtractMoney,
} from "../../services/active.campaigns.services";
import { updateDonatedProductBought } from "../../../Business Company/services/donated.product.services";
import { postATweet } from "../../../services/twitter.services";

import "./style.css";

export const BuyDonateProduct = (props) => {
  const { activistContext } = useContext(ActivistContext);
  const [activeCampaignsArr, setActiveCampaignsArr] = useState(undefined);
  const [productsArr, setProductsArr] = useState(undefined);
  const [isDataFetched, setIsDataFetched] = useState(false);
  console.log(activeCampaignsArr);

  //initialize active campaigns date
  const initActiveCampaignsData = async () => {
    let activeCampaigns = await getActivieCampaignsByActivistID(
      activistContext.ActivistID
    );
    let activeCampaignsObject = Object.values(activeCampaigns);
    setActiveCampaignsArr(activeCampaignsObject);
  };

  //initialize donated products data (wiil initialize only the products that relates to the campaigns that the activist promotes)
  const initDonatedProductsByActivistID = async () => {
    let donatedProducts = await getDonatedProductsByActivistID(
      activistContext.ActivistID
    );
    let donatedProductsObject = Object.values(donatedProducts);
    setProductsArr(donatedProductsObject);
  };

  useEffect(() => {
    initActiveCampaignsData();
    initDonatedProductsByActivistID();

    //if isDataFetched is set to false then render the component
    if (!isDataFetched) {
      initActiveCampaignsData();
      initDonatedProductsByActivistID();
      setIsDataFetched(true);
    }
  }, [isDataFetched]);

  //buy product => update the product's Bought status, update the money earned column in the databas, and post a tweet
  const handleBuyProduct = async (product) => {
    if (product.MoneyEarned < product.Price) {
      console.log(
        "Error: You don't have enough funds to purchase this product"
      );
      return;
    } else {
      let donatedProduct = {
        ProductID: product.ProductID,
        ProductName: product.ProductName,
        Price: product.Price,
        BusinessID: product.BusinessID,
        CampaignID: product.CampaignID,
        Bought: product.Bought,
        Shipped: product.Shipped,
      };
      await updateDonatedProductBought(
        donatedProduct,
        donatedProduct.ProductID
      );

      let activeCampaign = {
        ActiveCampID: product.ActiveCampID,
        ActivistID: product.ActivistID,
        CampaignID: product.CampaignID,
        TwitterUserName: product.TwitterUserName,
        Hashtag: product.Hashtag,
        MoneyEarned: product.Price,
        CampaignName: product.CampaignName,
        TweetsNumber: product.TweetsNumber,
      };
      await updateActiveCampaignSubtractMoney(
        activeCampaign,
        product.ActiveCampID
      );

      await postATweet(product.TwitterUserName, product.CampaignName);

      //after sending to the database set the isDataFetched to false
      setIsDataFetched(false);
    }
  };

  //donate product => update the the money earned column in the databas and post a tweet
  const handleDonateProduct = async (product) => {
    if (product.MoneyEarned < product.Price) {
      console.log("Error: You don't have enough funds to donate this product");
      return;
    } else {
      let activeCampaign = {
        ActiveCampID: product.ActiveCampID,
        ActivistID: product.ActivistID,
        CampaignID: product.CampaignID,
        TwitterUserName: product.TwitterUserName,
        Hashtag: product.Hashtag,
        MoneyEarned: product.Price,
        CampaignName: product.CampaignName,
        TweetsNumber: product.TweetsNumber,
      };
      await updateActiveCampaignSubtractMoney(
        activeCampaign,
        product.ActiveCampID
      );
      await postATweet(product.TwitterUserName, product.CampaignName);

      //after sending to the database set the isDataFetched to false
      setIsDataFetched(false);
    }
  };

  return (
    <div className="buy-container">
      <div className="wallet">
        <h5>My Wallet</h5>
        <div>
          {activeCampaignsArr &&
            activeCampaignsArr !== undefined &&
            activeCampaignsArr.map((activeCampaign) => {
              return (
                <div className="money-earned">
                  {activeCampaign.CampaignName && (
                    <h6 className="wallet-title">
                      Campaign Name: "{activeCampaign.CampaignName}", Money
                      Earned: {activeCampaign.MoneyEarned}$
                    </h6>
                  )}
                </div>
              );
            })}
        </div>
      </div>
      <div>
        {productsArr && productsArr !== undefined ? (
          productsArr.map((p) => {
            return (
              <div className="card card-profile card-product">
                <div className="card-body">
                  {p.ProductName && (
                    <h3 className="card-title">{p.ProductName}</h3>
                  )}
                  {p.Price && <h5 className="card-title">Price: {p.Price}</h5>}
                  {p.CampaignName && (
                    <h5 className="card-title">Campaign: {p.CampaignName}</h5>
                  )}
                  {p.Hashtag && (
                    <h5 className="card-title">Hashtag: {p.Hashtag}</h5>
                  )}
                </div>
                <div className="card-buttons">
                  <button
                    type="button"
                    className="btn btn-primary card-button"
                    data-bs-toggle="modal"
                    data-bs-target={`#exampleModalBuy${p.ProductID}`}
                    onClick={() => {
                      handleBuyProduct(p);
                    }}
                  >
                    Buy
                  </button>
                  <div
                    className="modal fade"
                    id={`exampleModalBuy${p.ProductID}`}
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
                          {p.MoneyEarned < p.Price ? (
                            <h4>
                              Sorry! You dont have enough money to buy the
                              product 🙁
                            </h4>
                          ) : (
                            <h4>
                              Thank you for your order 😀 <br />
                              The purchase of the product has been accepted in
                              the system.
                              <br />
                              The delivery company will contact you shortly.
                            </h4>
                          )}
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
                  <button
                    type="button"
                    className="btn btn-primary card-button"
                    data-bs-toggle="modal"
                    data-bs-target={`#exampleModalDonate${p.ProductID}`}
                    onClick={() => {
                      handleDonateProduct(p);
                    }}
                  >
                    Donate
                  </button>
                  <div
                    className="modal fade"
                    id={`exampleModalDonate${p.ProductID}`}
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
                          {p.MoneyEarned < p.Price ? (
                            <h4>
                              Sorry! You dont have enough money to donate the
                              product 🙁
                            </h4>
                          ) : (
                            <h4>
                              Thank you for donating the product back to the
                              campaign 😀
                            </h4>
                          )}
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
                </div>
              </div>
            );
          })
        ) : (
          <div className="spinner-app">
            <RingLoader color="#414141" size={200} />
          </div>
        )}
      </div>
    </div>
  );
};
