import React from "react";
import styled from "styled-components";
import Box from "../ui/containers/Box";
import Heading from "../ui/text/Heading";
import Button from "../ui/Button";
import Homebrew from "../features/mainDashboard/Homebrew";
import Characters from "../features/mainDashboard/Characters";
import Campaigns from "../features/mainDashboard/Campaigns";

const MainDashboardStyled = styled.main`
  margin: 5rem 5rem 5rem 5rem;
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 2rem;
`;

export default function MainDashboard() {
  return (
    <MainDashboardStyled>
      <Box>
        <Homebrew />
      </Box>
      <Box>
        <Characters />
      </Box>
      <Box>
        <Campaigns />
      </Box>
    </MainDashboardStyled>
  );
}
