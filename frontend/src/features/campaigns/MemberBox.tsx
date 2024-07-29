import React from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";

const BoxCustomStyles = css`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-gap: 15px;
  justify-content: center;
  text-align: center;
`;

export default function MemberBox({ children }) {
  const { id, character, level, xp, race, className, rest, img } = children;
  return (
    <Box customStyles={BoxCustomStyles}>
      <img
        src={img}
        alt="member"
        style={{ gridColumn: "1/2", maxWidth: "180px" }}
      ></img>
      <div style={{ gridColumn: "2/3" }}>
        <Heading as="h6">{id}</Heading>
        <Heading as="h3">{character}</Heading>
        <p>Level: {level}</p>
        <p>XP: {xp}</p>
        <p>Race: {race}</p>
        <p>Class: {className}</p>
        <p>Rest: {rest ? "true" : "false"}</p>
      </div>
      <div style={{ gridColumn: "1/3", gridRow: "2/3" }}>
        <ButtonGroup justify="center">
          <Button variation="primary" size="large">
            View
          </Button>
          <Button variation="primary" size="large">
            Edit
          </Button>
          <Button variation="primary" size="large">
            Kick
          </Button>
        </ButtonGroup>
      </div>
    </Box>
  );
}
