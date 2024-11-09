import React from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled from "styled-components";
import { CharacterItem } from "../../models/character";

const StyledElementBox = styled.div`
  text-align: center;
`;

export default function CharacterItemBox({
  character,
  onClick,
  showButtons = true,
}: {
  character: CharacterItem;
  onClick: any;
  showButtons: boolean;
}) {
  return (
    <Box radius="tiny" onClick={() => onClick(character.id)}>
      <Heading as="h3">{character.name}</Heading>
      <StyledElementBox>
        {character.class} & {character.race}
      </StyledElementBox>
      <StyledElementBox>{character.description}</StyledElementBox>
      {showButtons && (
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
      )}
    </Box>
  );
}
