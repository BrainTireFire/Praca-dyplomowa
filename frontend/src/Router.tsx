import React from "react";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Login from "./pages/account/Login";
import Register from "./pages/account/Register";
import Home from "./pages/Home";
import AppLayout from "./ui/containers/AppLayout";
import MainDashboard from "./pages/MainDashboard";
import Concact from "./pages/Concact";
import Homebrew from "./pages/homebrew/Homebrew";
import Characters from "./pages/Characters";
import Campagins from "./pages/campaign/Campagins";
import CampaginInstance from "./pages/campaign/CampaignInstance";
import Profile from "./pages/account/Profile";
import NotFound from "./pages/errors/NotFound";
import ForgotPassword from "./pages/account/ForgotPassword";
import PasswordChanged from "./pages/account/PasswordChanged";
import HomebrewCreatePower from "./pages/homebrew/HomebrewCreatePower";
import ProtectedRoute from "./features/account/ProtectedRoute";
import Forbidden from "./pages/errors/Forbidden";
import ServiceDown from "./pages/errors/ServiceDown";
import MainBoard from "./pages/campaign/session/MainBoard";

export default function Router() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          element={
            <ProtectedRoute>
              <AppLayout />
            </ProtectedRoute>
          }
        >
          <Route index element={<Navigate replace to="main" />} />
          <Route path="main" element={<MainDashboard />} />
          <Route path="campaigns" element={<Campagins />} />
          <Route path="campaigns/:campaignId" element={<CampaginInstance />} />
          <Route path="campaigns/session/:groupName" element={<MainBoard />} />
          <Route path="characters" element={<Characters />} />
          <Route path="homebrew" element={<Homebrew />} />
          <Route
            path="homebrew/createPower"
            element={<HomebrewCreatePower />}
          />
          <Route path="contact" element={<Concact />} />
          <Route path="profile" element={<Profile />} />
        </Route>
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="forgotPassword" element={<ForgotPassword />} />
        <Route path="changedPassword" element={<PasswordChanged />} />
        <Route path="home" element={<Home />} />
        <Route path="forbidden" element={<Forbidden />} />
        <Route path="serviceDown" element={<ServiceDown />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  );
}
