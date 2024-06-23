import React from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";

const StyledElementBox = styled.div`
  text-align: center;
`;

export default function CharacterItemBox({ character }) {
  return (
    <Box radius="tiny">
      <Heading as="h3">{character.name}</Heading>
      <StyledElementBox>
        {character.class} & {character.race}
      </StyledElementBox>
      <StyledElementBox>{character.description}</StyledElementBox>
      <div>
        <ButtonGroup justify="center">
          <Button variation="primary" size="medium">
            Remove
          </Button>
          <Button variation="primary" size="medium">
            Show campaign
          </Button>
        </ButtonGroup>
      </div>
    </Box>
  );
}
