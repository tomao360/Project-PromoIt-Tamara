import React, { useState } from "react";
import { Routes, Route } from "react-router-dom";

import {
  HomePageOrganization,
  CreateCampaign,
  ProfileOrganization,
} from "../../pages";
import { NavbarOrganization } from "../navbarOrganization/navbar.organization.component";
import { EditProfile } from "../editProfile/edit.profile.component";
import { About, ContactUs, PageNotFound } from "../../../pages";
import { OroganizationContext } from "../../context/organization.context";
import { EditCampaign } from "../editCampaign/edit.campaign.component";
import { Footer } from "./../../../components/footer/footer.component";

export const MainOrganization = ({ Email }) => {
  const [organizationContext, setOrganizationContext] = useState({});

  return (
    <div className="app">
      <OroganizationContext.Provider
        value={{ organizationContext, setOrganizationContext }}
      >
        <div>
          <NavbarOrganization />
        </div>
        <div className="app">
          <Routes>
            <Route path="/" element={<HomePageOrganization Email={Email} />} />
            <Route path="/about" element={<About />} />
            <Route path="/contact-us" element={<ContactUs Email={Email} />} />
            <Route path="/create-campaign" element={<CreateCampaign />} />
            <Route path="/edit-campaign" element={<EditCampaign />} />
            <Route
              path="/profile"
              element={<ProfileOrganization Email={Email} />}
            />
            <Route path="/edit-profile" element={<EditProfile />} />
            <Route path="*" element={<PageNotFound />} />
          </Routes>
        </div>
        <div>
          <Footer />
        </div>
      </OroganizationContext.Provider>
    </div>
  );
};
