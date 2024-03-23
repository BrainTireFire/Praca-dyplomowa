import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";

const StyledElementBox = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 2rem;
  margin-top: 1.5rem;
`;

export default function Characters() {
  return (
    <>
      <Heading as="h4" align="left">
        Characters
      </Heading>
      <StyledElementBox>
        <Button size="large" variation="primary">
          My playable characters
        </Button>
        <Button size="large" variation="secondary">
          New PC
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button size="large" variation="primary">
          My non-playable characters
        </Button>
        <Button size="large" variation="secondary">
          New NPC
        </Button>
      </StyledElementBox>
    </>
  );
}
