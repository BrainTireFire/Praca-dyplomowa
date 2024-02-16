import React from "react";
import { NavLink } from "react-router-dom";
import styled from "styled-components";

const NavList = styled.ul`
  display: flex;
  flex-direction: row;
  gap: 0.8rem;
`;

const StyledNavLink = styled(NavLink)`
  &:link,
  &:visited {
    display: flex;
    align-items: center;
    gap: 1.2rem;

    color: var(--color-grey-600);
    font-size: 1.6rem;
    font-weight: 500;
    padding: 1.2rem 2.4rem;
    transition: all 0.3s;
  }

  &:hover,
  &:active,
  &.active:link,
  &.active:visited {
    color: var(--color-header-text);
    //background-color: var(--color-header-text);
    border-radius: var(--border-radius-sm);
  }

  & svg {
    width: 2.4rem;
    height: 2.4rem;
    color: var(--color-grey-400);
    transition: all 0.3s;
  }

  &:hover svg,
  &:active svg,
  &.active:link svg,
  &.active:visited svg {
    color: var(--color-brand-600);
  }
`;

export default function MainNavbar() {
  return (
    <nav>
      <NavList>
        <li>
          <StyledNavLink to="/campaigns">
            <span>My campaigns</span>
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/characters">
            <span>My Characters</span>
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/homebrew">
            <span>Homebrew</span>
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/contact">
            <span>Concact</span>
          </StyledNavLink>
        </li>
      </NavList>
    </nav>
  );
}
