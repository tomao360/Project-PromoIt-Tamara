import React, { useState } from "react";
import { Routes, Route } from "react-router-dom";

import {
  Donations,
  ShipmentDetails,
  ProfileBusinessCompany,
  HomePageBusinessCompany,
} from "../../pages";
import { About, ContactUs, PageNotFound } from "../../../pages";
import { NavbarBusinessCompany, EditProfile } from "../index";
import { BusinessContext } from "../../context/business.context";
import { DonateProduct } from "./../donateProduct/donate.product.component";
import { Footer } from "./../../../components/footer/footer.component";

export const MainBusinessCompany = ({ Email }) => {
  const [businessContext, setBusinessContext] = useState({});
  console.log(businessContext, "BusinessContext");

  return (
    <div className="app">
      <BusinessContext.Provider value={{ businessContext, setBusinessContext }}>
        <div>
          <NavbarBusinessCompany />
        </div>
        <div className="app">
          <Routes>
            <Route
              path="/"
              element={<HomePageBusinessCompany Email={Email} />}
            />
            <Route path="/about" element={<About />} />
            <Route path="/contact-us" element={<ContactUs Email={Email} />} />
            <Route path="/donations" element={<Donations />} />
            <Route path="/donate" element={<DonateProduct />} />
            <Route path="/shipment-details" element={<ShipmentDetails />} />
            <Route
              path="/profile"
              element={<ProfileBusinessCompany Email={Email} />}
            />
            <Route path="/edit-profile" element={<EditProfile />} />
            <Route path="*" element={<PageNotFound />} />
          </Routes>
        </div>
        <div>
          <Footer />
        </div>
      </BusinessContext.Provider>
    </div>
  );
};
