import React, { useState, useRef } from "react";
import { Outlet } from "react-router-dom";
import styled, { keyframes } from "styled-components";
import Navbar from "./bars/Navbar";
import Sidebar from "./bars/Sidebar";
import Button from "../interactive/Button";
import { HiXMark } from "react-icons/hi2";
import { SidebarOverlay } from "../../features/campaigns/sidebarOverlayComponents/SidebarOverlay";
import { ControlledCharacterContext } from "../../features/campaigns/session/context/ControlledCharacterContext";
import useClickOutside from "../../hooks/useClickOutside";

const StyledAppLayout = styled.div`
  display: grid;
  height: 100vh;
  grid-template-columns: 7rem 1fr;
  grid-template-rows: auto 1fr;
`;

const Main = styled.main`
  padding-top: 0rem;
  /* padding: 4rem 4.8rem 6.4rem; */
  overflow: auto;

  /* Hide scrollbar for WebKit browsers (Chrome, Safari) */
  /* &::-webkit-scrollbar {
    display: none;
  } */

  /* Hide scrollbar for Firefox */
  //scrollbar-width: none; /* Firefox */

  /* Hide scrollbar for Internet Explorer, Edge */
  //-ms-overflow-style: none; /* Internet Explorer and Edge */
`;

const Container = styled.div`
  height: 100%;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  gap: 3.2rem;
`;

export default function AppLayout() {
  const sidebarOverlayRef = useRef(null);
  const [activeComponent, setActiveComponent] = useState<string | null>(null);
  const [isClosing, setIsClosing] = useState(false);
  const [controlledCharacterId, setControlledCharacterId] = useState(null);

  const handleOpen = (component: string | null) => {
    setIsClosing(false);
    setActiveComponent(component);
  };

  const handleClose = () => {
    setIsClosing(true);
    setTimeout(() => setActiveComponent(null), 300); // Matches the animation duration
  };

  useClickOutside(sidebarOverlayRef, handleClose);

  return (
    <StyledAppLayout>
      <ControlledCharacterContext.Provider
        value={[controlledCharacterId, setControlledCharacterId]}
      >
        <Navbar />
        <Sidebar
          setActiveComponent={handleOpen}
          handleClose={handleClose}
          activeComponent={activeComponent}
        />
        <Main>
          <Container>
            <Outlet />
          </Container>
        </Main>
        {/* Overlay Component */}
        {activeComponent && (
          <SidebarOverlay
            ref={sidebarOverlayRef}
            isClosing={isClosing}
            handleClose={handleClose}
            activeComponent={activeComponent}
          ></SidebarOverlay>
        )}
      </ControlledCharacterContext.Provider>
    </StyledAppLayout>
  );
}
