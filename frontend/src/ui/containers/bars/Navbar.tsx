import React from "react";
import styled from "styled-components";
import MainNavbar from "./MainNavbar";

const StyledNavbar = styled.aside`
  background-color: var(--color-navbar);
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 0.8rem;
`;

export default function Navbar() {
  return (
    <StyledNavbar>
      <MainNavbar />
    </StyledNavbar>
  );
}
