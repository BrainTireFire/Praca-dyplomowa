import React from "react";
import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";

const StyledElementBox = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 2rem;
  margin-top: 1.5rem;
`;

export default function Characters() {
  const navigate = useNavigate();
  return (
    <>
      <Heading as="h4" align="left">
        Characters
      </Heading>
      <StyledElementBox>
        <Button
          size="large"
          variation="primary"
          onClick={() => navigate(`/characters`)}
        >
          My playable characters
        </Button>
      </StyledElementBox>
      <StyledElementBox>
        <Button
          size="large"
          variation="primary"
          onClick={() => navigate(`/npc`)}
        >
          My non-playable characters
        </Button>
      </StyledElementBox>
    </>
  );
}
