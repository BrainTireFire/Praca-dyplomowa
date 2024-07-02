import React from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";

const BoxCustomStyles = css`
  display: grid;
  grid-template-rows: 0.5fr 0.5fr 1.5fr 0.5fr;
`;

const StyledElementBox = styled.div`
  text-align: center;
`;

export default function MemberBox({
  member,
  character,
  level,
  race,
  classs,
  img,
}) {
  return (
    <Box radius="tiny" customStyles={BoxCustomStyles} style={{ gap: "20px" }}>
      <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
        <img
          src={img}
          alt="member"
          style={{ width: "150px", height: "150px" }}
        ></img>
        <div>
          <Heading as="h6">{member}</Heading>
          <Heading as="h3">{character}</Heading>
          <StyledElementBox>Level: {level}</StyledElementBox>
          <StyledElementBox>Race: {race}</StyledElementBox>
          <StyledElementBox>Class: {classs}</StyledElementBox>
        </div>
      </div>
      <div>
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
