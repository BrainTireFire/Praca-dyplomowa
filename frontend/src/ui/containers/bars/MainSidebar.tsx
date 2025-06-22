import { NavLink, useNavigate, useParams } from "react-router-dom";
import styled from "styled-components";
import { PiSwordDuotone, PiClock } from "react-icons/pi";
import { AiOutlineThunderbolt } from "react-icons/ai";
import { FaDice, FaRegAddressBook } from "react-icons/fa";
import { GiMountainRoad } from "react-icons/gi";
import { GiHorizonRoad } from "react-icons/gi";

import { IoMdSettings } from "react-icons/io";
import { MdBackpack } from "react-icons/md";
import Modal from "../Modal";
import Button from "../../interactive/Button";
import BatchRollModal from "../../../features/mainDashboard/sidebar/BatchRollModal";
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

function MainSidebar({
  setActiveComponent,
  handleClose,
  activeComponent,
}: {
  setActiveComponent: (name: string | null) => void;
  handleClose: () => void;
  activeComponent: string;
}) {
  const { campaignId } = useParams<{ campaignId: string }>();
  const { groupName } = useParams<{ groupName: string }>();
  const navigate = useNavigate();

  return (
    <>
      <nav>
        <NavList>
          {campaignId && (
            <>
              <li>
                <IconContainer
                  onClick={() => navigate(`/campaigns/${campaignId}`)}
                >
                  <GiHorizonRoad title="Back to campaign"/>
                </IconContainer>
              </li>
              <li>
                <IconContainer
                  onClick={() => {
                    if (activeComponent === "Component1") {
                      handleClose();
                      return;
                    }
                    setActiveComponent("Component1");
                  }}
                >
                  <FaRegAddressBook title="Compact character sheet"/>
                </IconContainer>
              </li>
              <li>
                <IconContainer
                  onClick={() => {
                    if (activeComponent === "Component2") {
                      handleClose();
                      return;
                    }
                    setActiveComponent("Component2");
                  }}
                >
                  <PiSwordDuotone title="Equipment and attacks"/>
                </IconContainer>
              </li>
              <li>
                <IconContainer
                  onClick={() => {
                    if (activeComponent === "Component3") {
                      handleClose();
                      return;
                    }
                    setActiveComponent("Component3");
                  }}
                >
                  <AiOutlineThunderbolt title="Powers and resources"/>
                </IconContainer>
              </li>
            </>
          )}
          {groupName && (
            <>
              <li>
                <IconContainer
                  onClick={() => {
                    if (activeComponent === "Component4") {
                      handleClose();
                      return;
                    }
                    setActiveComponent("Component4");
                  }}
                >
                  <PiClock title="Initiative queue"/>
                </IconContainer>
              </li>
            </>
          )}
          {/* <li>
            <IconContainer>
              <MdBackpack />
            </IconContainer>
          </li> */}
          <li>
            <Modal>
              <Modal.Open opens="BatchRollModal">
                <IconContainer>
                  <FaDice title="Dice rolling"/>
                </IconContainer>
              </Modal.Open>
              <Modal.Window name="BatchRollModal">
                <BatchRollModal />
              </Modal.Window>
            </Modal>
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
