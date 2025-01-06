import styled, { keyframes } from "styled-components";
import Button from "../../../ui/interactive/Button";
import { HiBackward, HiXMark } from "react-icons/hi2";
import { useParams } from "react-router-dom";
import { useMyCharacter } from "../hooks/useMyCharacter";
import Spinner from "../../../ui/interactive/Spinner";
import { CharacterMiniSheet } from "./CharacterMiniSheet";
import { Character } from "../../../models/character";
import { EquipmentSheet } from "./EquipmentSheet";
import { SpellSheet } from "./SpellSheet";
import { InitiativeQueue } from "./InitiativeQueue";

// Keyframes for the sliding animation
const slideIn = keyframes`
  from {
    transform: translateX(-100%);
  }
  to {
    transform: translateX(0);
  }
`;

const slideOut = keyframes`
  from {
    transform: translateX(0);
  }
  to {
    transform: translateX(-100%);
  }
`;

type OverlayProps = {
  isClosing: boolean;
};

const Overlay = styled.div<OverlayProps>`
  position: absolute;
  top: 0;
  left: calc(7rem); /* Adjust based on sidebar width */
  width: 35%; /* Adjust based on main area width */
  max-height: 100%;
  height: 100%;
  background: #1f2421;
  display: flex;
  flex-direction: column;
  z-index: 1000;
  /* Sliding animation */
  animation: ${({ isClosing }) => (isClosing ? slideOut : slideIn)} 0.3s
    forwards;
`;

const OverlayContent = styled.div`
  background: rgba(var(--color-secondary-background-rgb), 0.05);
  max-height: 95%;
  height: auto;
  border-radius: 8px;
  padding: 10px;
`;

export function SidebarOverlay({
  isClosing,
  handleClose,
  activeComponent,
}: {
  isClosing: boolean;
  handleClose: () => void;
  activeComponent: string;
}) {
  const { isLoading, character, isError, error } = useMyCharacter();
  if (!isLoading && !character) {
    handleClose();
  }
  return (
    <Overlay isClosing={isClosing}>
      <Button onClick={handleClose}>
        <HiBackward />
      </Button>
      {!isLoading && (
        <>
          <OverlayContent>
            {activeComponent === "Component1" && (
              <CharacterMiniSheet
                character={character as Character}
              ></CharacterMiniSheet>
            )}
            {activeComponent === "Component2" && (
              <EquipmentSheet
                character={character as Character}
              ></EquipmentSheet>
            )}
            {activeComponent === "Component3" && (
              <SpellSheet character={character as Character}></SpellSheet>
            )}
            {activeComponent === "Component4" && (
              <InitiativeQueue></InitiativeQueue>
            )}
          </OverlayContent>
        </>
      )}
      {isLoading && <Spinner></Spinner>}
      {isError && <>Error</>}
    </Overlay>
  );
}
