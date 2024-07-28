import React from "react";
import styled from "styled-components";
import SessionLayout from "../../../features/campaigns/session/SessionLayout";

const Container = styled.div`
  display: grid;
  grid-template-columns: 3fr 1fr;
  grid-template-rows: auto 1fr;
  gap: 10px;
  height: 100vh;
`;

export default function MainBoard() {
  return (
    <Container>
      <SessionLayout />
    </Container>
  );
}
