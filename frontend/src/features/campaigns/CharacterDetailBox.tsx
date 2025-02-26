import React from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";
import { CharacterItem } from "../../models/character";
import Modal from "../../ui/containers/Modal";
import { CharacterIdContext } from "../characters/contexts/CharacterIdContext";
import CharactersSheet from "../characters/CharactersSheet";

const BoxCustomStyles = css`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-gap: 10px;
  justify-content: center;
  text-align: center;

  transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
  cursor: pointer;

  &:hover {
    transform: translateY(-3px);
    box-shadow: var(--shadow-md);
    background: var(--color-navbar-hover);
  }
`;

export default function CharacterDetailBox({
  children,
  handleKickCharacter,
  gameMaster,
}: {
  children: CharacterItem;
  handleKickCharacter: Function;
  gameMaster: boolean;
}) {
  return (
    <Modal>
      <Modal.Open opens="CharactersSheet">
        <Box customStyles={BoxCustomStyles} style={{ borderRadius: "10px" }}>
          <div style={{ gridColumn: "1/3" }}>
            <Heading as="h3">{children.name}</Heading>
            {/* <p>Level: {level}</p> */}
            {/* <p>XP: {xp}</p> */}
            <p>Race: {children.race}</p>
            <p>Class: {children.class}</p>
            {/* <p>Rest: {rest ? "true" : "false"}</p> */}
          </div>
          <div style={{ gridColumn: "1/3", gridRow: "2/3" }}>
            {/* TODO: Implement View button !*/}
            {gameMaster && (
              <ButtonGroup justify="center">
                <Button
                  variation="primary"
                  size="large"
                  onClick={() => handleKickCharacter(children.id)}
                >
                  Kick
                </Button>
              </ButtonGroup>
            )}
          </div>
        </Box>
      </Modal.Open>
      <Modal.Window name="CharactersSheet">
        <CharacterIdContext.Provider value={{ characterId: children.id }}>
          <CharactersSheet />
        </CharacterIdContext.Provider>
      </Modal.Window>
    </Modal>
  );
}
