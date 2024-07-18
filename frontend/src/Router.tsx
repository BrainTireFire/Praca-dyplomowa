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
import CampaignInstance from "./pages/campaign/CampaignInstance";
import Shops from "./pages/campaign/shop/Shops";
import Profile from "./pages/account/Profile";
import NotFound from "./pages/NotFound";
import ForgotPassword from "./pages/account/ForgotPassword";
import PasswordChanged from "./pages/account/PasswordChanged";
import HomebrewCreatePower from "./pages/homebrew/HomebrewCreatePower";

export default function Router() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<AppLayout />}>
          <Route index element={<Navigate replace to="main" />} />
          <Route path="main" element={<MainDashboard />} />
          <Route path="campaigns" element={<Campagins />} />
          <Route path="campaigns/:campaignId" element={<CampaignInstance />} />
          <Route path="campaigns/:campaignId/shops" element={<Shops />} />
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
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  );
}
