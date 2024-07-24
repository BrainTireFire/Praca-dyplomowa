import { NavLink } from "react-router-dom";
import styled from "styled-components";
import { PiSwordDuotone, PiClock } from "react-icons/pi";
import { AiOutlineThunderbolt } from "react-icons/ai";
import { FaDice, FaRegAddressBook } from "react-icons/fa";
import { IoMdSettings } from "react-icons/io";
import { MdBackpack } from "react-icons/md";
import Modal from "../Modal";
import SettingsSideBarModal from "../../../features/settings/SettingsSideBarModal";

const NavList = styled.ul`
  display: flex;
  flex-direction: column;
  gap: 2rem;
`;

const IconContainer = styled.div`
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

const BottomContainer = styled.nav`
  display: flex;
  flex-direction: column;
  margin-top: auto;
`;

function MainSidebar() {
  return (
    <>
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
      <BottomContainer>
        <Modal>
          <Modal.Open opens="settings">
            <IconContainer>
              <IoMdSettings />
            </IconContainer>
          </Modal.Open>
          <Modal.Window name="settings">
            <SettingsSideBarModal />
          </Modal.Window>
        </Modal>
      </BottomContainer>
    </>
  );
}

export default MainSidebar;
