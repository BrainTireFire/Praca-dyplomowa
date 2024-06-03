import { NavLink } from "react-router-dom";
import styled from "styled-components";
import { PiSwordDuotone, PiClock } from "react-icons/pi";
import { AiOutlineThunderbolt } from "react-icons/ai";
import { FaDice, FaRegAddressBook } from "react-icons/fa";
import { MdBackpack } from "react-icons/md";

const NavList = styled.ul`
  display: flex;
  flex-direction: column;
  gap: 2rem;
`;

const StyledNavLink = styled(NavLink)`
  & svg {
    width: 5rem;
    height: 5rem;
    color: var(--color-link);
    transition: all 0.3s;
  }

  &:hover svg,
  &:active svg,
  &.active:link svg,
  &.active:visited svg {
    color: var(--color-link-hover);
  }
`;

function MainSidebar() {
  return (
    <nav>
      <NavList>
        <li>
          <StyledNavLink to="/">
            <FaRegAddressBook />
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/">
            <PiSwordDuotone />
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/">
            <PiClock />
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/">
            <AiOutlineThunderbolt />
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/">
            <MdBackpack />
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/">
            <FaDice />
          </StyledNavLink>
        </li>
      </NavList>
    </nav>
  );
}

export default MainSidebar;
