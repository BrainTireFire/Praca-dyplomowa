import { NavLink } from "react-router-dom";
import styled from "styled-components";

const Menu = styled.div`
  display: none;
  position: absolute;
  background-color: var(--color-navbar);
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
`;

const DropdownLink = styled(NavLink)`
  color: var(--color-grey-600);
  padding: 12px 16px;
  text-decoration: none;
  display: block;

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
  }
`;

const DropdownButton = styled.button`
  background: none;
  border: none;
  color: var(--color-grey-600);
  cursor: pointer;
  padding: 12px 16px;
  text-decoration: none;
  display: inline-block;

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
  }
`;

const StyledDropdown = styled.li`
  position: relative;
  display: inline-block;
  margin-left: auto;
  padding-left: 50px;

  &:hover ${Menu} {
    display: block;
  }
`;

function DropdownNav({ children }: { children: React.ReactNode }) {
  return <StyledDropdown>{children}</StyledDropdown>;
}

DropdownNav.Menu = Menu;
DropdownNav.Link = DropdownLink;
DropdownNav.Button = DropdownButton;

export default DropdownNav;
