import React, { forwardRef } from "react";
import styled, { css, keyframes } from "styled-components";
import Button from "../../../ui/interactive/Button";
import { RiArrowGoBackLine } from "react-icons/ri";
import { useParams } from "react-router-dom";
import { useMyCharacter } from "../hooks/useMyCharacter";
import Spinner from "../../../ui/interactive/Spinner";
import { CharacterMiniSheet } from "./CharacterMiniSheet";
import { Character } from "../../../models/character";
import { EquipmentSheet } from "./EquipmentSheet";
import { SpellSheet } from "./SpellSheet";
import { InitiativeQueue } from "./InitiativeQueue";
import { CharacterIdContext } from "../../characters/contexts/CharacterIdContext";

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
  width: auto; /* Adjust based on main area width */
  max-height: 100%;
  height: 100%;
  background: #1f2421;
  display: flex;
  flex-direction: column;
  z-index: 1000;
  /* Sliding animation */
  animation: ${({ isClosing }) => (isClosing ? slideOut : slideIn)} 0.3s
    forwards;
  overflow-y: auto;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  scrollbar-width: thin;
  scrollbar-gutter: stable;
`;

const OverlayContent = styled.div`
  background: rgba(var(--color-secondary-background-rgb), 0.05);
  height: auto;
  border-radius: 8px;
  padding: 10px;
`;

export const SidebarOverlay = forwardRef<
  HTMLDivElement,
  {
    isClosing: boolean;
    handleClose: () => void;
    activeComponent: string;
  }
>(({ isClosing, handleClose, activeComponent }, ref) => {
  const { isLoading, character, isError, error } = useMyCharacter();
  const equipmentSheetRef =
    ref as React.MutableRefObject<HTMLDivElement | null>;
  // if (!isLoading && !character) {
  //   handleClose();
  // }

  return (
    <Overlay isClosing={isClosing} ref={ref}>
      <Button
        onClick={handleClose}
        size="medium"
        customStyles={css`
          margin: 5px;
        `}
      >
        <div
          style={{
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "center",
            gap: "1rem",
          }}
        >
          Close
          <RiArrowGoBackLine title="Close overlay" />
        </div>
      </Button>
      {!isLoading && (
        <>
          <OverlayContent>
            {character && (
              <CharacterIdContext.Provider
                value={{ characterId: character.id }}
              >
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
              </CharacterIdContext.Provider>
            )}
            {isError && (
              <>
                {error?.message
                  ? JSON.parse(error?.message as string).message
                  : "Error"}
              </>
            )}
            {activeComponent === "Component4" && (
              <InitiativeQueue></InitiativeQueue>
            )}
          </OverlayContent>
        </>
      )}
      {isLoading && <Spinner></Spinner>}
    </Overlay>
  );
});
