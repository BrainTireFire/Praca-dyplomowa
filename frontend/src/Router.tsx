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
import CampaignInstance from "./features/campaigns/CampaignInstance";
import Shops from "./pages/campaign/shop/Shops";
import Profile from "./pages/account/Profile";
import NotFound from "./pages/errors/NotFound";
import ForgotPassword from "./pages/account/ForgotPassword";
import PasswordChanged from "./pages/account/PasswordChanged";
import HomebrewCreatePower from "./pages/homebrew/HomebrewCreatePower";
import CustomizeShop from "./pages/campaign/shop/CustomizeShop";
import ProtectedRoute from "./features/account/ProtectedRoute";
import Forbidden from "./pages/errors/Forbidden";
import ServiceDown from "./pages/errors/ServiceDown";
import MainBoard from "./pages/campaign/session/MainBoard";
import BoardCreateForm from "./features/homebrew/maps/BoardCreateForm";
import HomebrewMap from "./pages/homebrew/maps/HomebrewMap";
import MapInstance from "./features/homebrew/maps/MapInstance";
import MapUpdateBoardForm from "./features/homebrew/maps/MapUpdateBoardForm";
import CampaignJoin from "./pages/campaign/CampaignJoin";
import CreateEncounter from "./pages/campaign/encounter/CreateEncounter";
import Items from "./pages/items/Items";
import Powers from "./pages/powers/Powers";
import NpcCharacter from "./pages/NpcCharacter";
import MainSession from "./pages/campaign/session/MainSession";
import Encounter from "./pages/campaign/encounter/Encounter";
import EncounterEditForm from "./features/campaigns/encounter/EncounterEditForm";
import ItemFamilies from "./pages/items/ItemFamilies";
import CampaginsAttend from "./pages/campaign/CampaginsAttend";

export default function Router() {
  return (
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
        <Route path="join/:campaignId" element={<CampaignJoin />} />
        <Route path="campaigns" element={<Campagins />} />
        <Route path="campaignAttend" element={<CampaginsAttend />} />
        <Route path="campaigns/:campaignId" element={<CampaignInstance />} />
        <Route path="campaigns/:campaignId/shops" element={<Shops />} />
        <Route
          path="campaigns/:campaignId/shops/:shopId"
          element={<CustomizeShop />}
        />
        <Route
          path="campaigns/:campaignId/createSession"
          element={<MainBoard />}
        />
        <Route
          path="campaigns/:campaignId/session/:groupName"
          element={<MainSession />}
        />
        <Route
          path="campaigns/:campaignId/createEncounter"
          element={<CreateEncounter />}
        />
        <Route
          path="campaigns/:campaignId/encounters/:encounterId/editEncounter"
          element={<EncounterEditForm />}
        />
        <Route
          path="campaigns/:campaignId/encounters"
          element={<Encounter />}
        />
        <Route path="characters" element={<Characters />} />
        <Route path="npc" element={<NpcCharacter />} />
        <Route path="items" element={<Items />} />
        <Route path="itemFamilies" element={<ItemFamilies />} />
        <Route path="powers" element={<Powers />} />
        <Route path="homebrew" element={<Homebrew />} />
        <Route path="homebrew/createPower" element={<HomebrewCreatePower />} />
        <Route path="homebrew/createMap" element={<BoardCreateForm />} />
        <Route
          path="homebrew/updateMap/:boardId"
          element={<MapUpdateBoardForm />}
        />
        <Route path="homebrew/map" element={<HomebrewMap />} />
        <Route path="homebrew/map/:boardId" element={<MapInstance />} />
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
  );
}
