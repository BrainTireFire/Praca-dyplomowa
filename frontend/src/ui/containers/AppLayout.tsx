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
  padding-top: 4rem;
  /* padding: 4rem 4.8rem 6.4rem; */
  overflow: scroll;
`;

const Container = styled.div`
  max-width: 170rem;
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
