import React from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";
import { CharacterItem } from "../../models/character";

const BoxCustomStyles = css`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-gap: 10px;
  justify-content: center;
  text-align: center;
`;

export default function CharacterDetailBox({ children }) {
  return (
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
        <ButtonGroup justify="center">
          <Button variation="primary" size="large">
            View
          </Button>
          <Button variation="primary" size="large">
            Kick
          </Button>
        </ButtonGroup>
      </div>
    </Box>
  );
}
