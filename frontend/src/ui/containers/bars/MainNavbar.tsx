import { NavLink } from "react-router-dom";
import styled from "styled-components";
import { RxAvatar } from "react-icons/rx";
import { IoSettingsOutline, IoLogOutOutline } from "react-icons/io5";
import DropdownNav from "../../links/DropdownNav";
import { useQueryClient } from "@tanstack/react-query";
import { useLogout } from "../../../features/account/useLogout";
import { useTranslation } from "react-i18next";

const NavList = styled.ul`
  display: flex;
  flex-direction: row;
  gap: 0.8rem;

  /* @media (max-width: 768px) {
    flex-direction: column;
    display: ${(props) => (props.open ? "block" : "none")};
  } */
`;

// const Hamburger = styled.div`
//   display: none;

//   @media (max-width: 768px) {
//     display: block;
//   }
// `;

const LinkWithIconContainer = styled.span`
  display: flex;
  align-items: center;
  flex-direction: row;
  gap: 0.8rem;
`;

const StyledNavLink = styled(NavLink)`
  &:link,
  &:visited {
    display: flex;
    align-items: center;
    gap: 1.2rem;

    color: var(--color-secondary-text);
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
    color: var(--color-secondary-text);
    transition: all 0.3s;
  }

  &:hover svg,
  &:active svg,
  &.active:link svg,
  &.active:visited svg {
    color: var(--color-secondary-hover-text);
  }
`;

const StyledRowLi = styled.li`
  display: flex;
  flex-direction: row;
  margin-top: 12px;
  gap: 0.8rem;

  &:link,
  &:visited {
    display: flex;
    align-items: center;
    gap: 1.2rem;

    color: var(--color-secondary-text);
    font-size: 1.6rem;
    font-weight: 500;
    padding: 1.2rem 2.4rem;
    transition: all 0.3s;
  }

  /* &:hover,
  &:active,
  &.active:link,
  &.active:visited {
    color: var(--color-header-text);
    //background-color: var(--color-header-text);
    border-radius: var(--border-radius-sm);
  } */

  & svg {
    width: 2.4rem;
    height: 2.4rem;
    color: var(--color-secondary-text);
    transition: all 0.3s;
  }

  /* &:hover svg,
  &:active svg,
  &.active:link svg,
  &.active:visited svg {
    color: var(--color-secondary-hover-text);
  } */
`;

const MainNavbarStyled = styled.nav`
  height: 7rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

export default function MainNavbar() {
  const { t } = useTranslation();
  const queryClient = useQueryClient();
  const user = queryClient.getQueryData(["user"]);
  const { logout, isLoading } = useLogout();

  // const [isOpen, setIsOpen] = useState(false);

  // const handleHamburgerClick = () => {
  //   setIsOpen(!isOpen);
  // };

  return (
    <MainNavbarStyled>
      {/* <Hamburger onClick={handleHamburgerClick}>☰</Hamburger>
      <NavList open={isOpen}> */}
      <NavList>
        <li>
          <StyledNavLink to="/main">
            <img src="https://via.placeholder.com/25" alt="logo" />
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/campaigns">
            <span>{t("main.navbar.link.my.campaigns")}</span>
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/characters">
            <span>{t("main.navbar.link.my.characters")}</span>
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/homebrew">
            <span>{t("main.navbar.link.homebrew")}</span>
          </StyledNavLink>
        </li>
        <li>
          <StyledNavLink to="/contact">
            <span>{t("main.navbar.link.concact")}</span>
          </StyledNavLink>
        </li>

        <DropdownNav>
          <StyledNavLink to="/profile">
            <LinkWithIconContainer>
              <RxAvatar />
              {user?.username || "Nickname"}
            </LinkWithIconContainer>
          </StyledNavLink>
          <DropdownNav.Menu>
            <DropdownNav.Link to="/profile">
              <LinkWithIconContainer>
                <IoSettingsOutline /> {t("main.navbar.link.profile")}
              </LinkWithIconContainer>
            </DropdownNav.Link>
            <DropdownNav.Button onClick={logout} disabled={isLoading}>
              <LinkWithIconContainer>
                <IoLogOutOutline /> {t("main.navbar.link.signout")}
              </LinkWithIconContainer>
            </DropdownNav.Button>
          </DropdownNav.Menu>
        </DropdownNav>
      </NavList>
    </MainNavbarStyled>
  );
}
