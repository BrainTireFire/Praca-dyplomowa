import React from "react";
import { Outlet } from "react-router-dom";
import styled from "styled-components";
import Navbar from "./bars/Navbar";
import Sidebar from "./bars/Sidebar";

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
  max-width: 170rem;
  height: 100%;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  gap: 3.2rem;
`;

export default function AppLayout() {
  return (
    <StyledAppLayout>
      <Navbar />
      <Sidebar />
      <Main>
        <Container>
          <Outlet />
        </Container>
      </Main>
    </StyledAppLayout>
  );
}
