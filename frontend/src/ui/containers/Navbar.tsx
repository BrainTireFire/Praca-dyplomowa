import React from "react";
import styled from "styled-components";
import MainNavbar from "./MainNavbar";

const StyledNavbar = styled.aside`
  background-color: var(--color-navbar);
`;

export default function Navbar() {
  return (
    <StyledNavbar>
      <MainNavbar />
    </StyledNavbar>
  );
}
