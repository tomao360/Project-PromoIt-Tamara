import React, { useState } from "react";
import { Routes, Route } from "react-router-dom";

import {
  HomePageSocialActivist,
  BuyDonateProduct,
  ProfileSocialActivist,
} from "../../pages";
import { NavbarSocialActivist, EditProfile } from "../index";
import { About, ContactUs, PageNotFound } from "../../../pages";
import { Footer } from "./../../../components/footer/footer.component";
import { ActivistContext } from "../../context/activist.context";

export const MainSocialActivist = ({ Email }) => {
  const [activistContext, setActivistContext] = useState({});
  return (
    <div className="app">
      <ActivistContext.Provider value={{ activistContext, setActivistContext }}>
        <div>
          <NavbarSocialActivist />
        </div>
        <div className="app">
          <Routes>
            <Route
              path="/"
              element={<HomePageSocialActivist Email={Email} />}
            />
            <Route path="/about" element={<About />} />
            <Route path="/contact-us" element={<ContactUs Email={Email} />} />
            <Route path="/buy-donate" element={<BuyDonateProduct />} />
            <Route
              path="/profile"
              element={<ProfileSocialActivist Email={Email} />}
            />
            <Route path="/edit-profile" element={<EditProfile />} />
            <Route path="*" element={<PageNotFound />} />
          </Routes>
        </div>
        <div>
          <Footer />
        </div>
      </ActivistContext.Provider>
    </div>
  );
};
