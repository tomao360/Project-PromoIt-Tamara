import React from "react";
import { Routes, Route } from "react-router-dom";

import {
  HomePageProLobbyOwner,
  DeleteUser,
  Reports,
  UserMessages,
} from "../../pages";
import { PageNotFound } from "../../../pages";
import { NavbarProLobbyOwner } from "../index";
import { Footer } from "../../../components/footer/footer.component";

export const MainProLobbyOwner = (props) => {
  return (
    <div className="app">
      <div>
        <NavbarProLobbyOwner />
      </div>
      <div className="app">
        <Routes>
          <Route path="/" element={<HomePageProLobbyOwner />} />
          <Route path="/delete-user" element={<DeleteUser />} />
          <Route path="/reports" element={<Reports />} />
          <Route path="/user-messages" element={<UserMessages />} />
          <Route path="*" element={<PageNotFound />} />
        </Routes>
      </div>
      <div>
        <Footer />
      </div>
    </div>
  );
};
