import React from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import ButtonGroup from "../../ui/interactive/ButtonGroup";
import Button from "../../ui/interactive/Button";
import styled, { css } from "styled-components";
import { CharacterItem } from "../../models/character";
import { useDeleteCharacter } from "./hooks/useDeleteCharacter";
import { useNavigate } from "react-router-dom";

const BoxCustomStyles = css`
  transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
  cursor: pointer;

  &:hover {
    transform: translateY(-2px);
    box-shadow: var(--shadow-md);
    background: var(--color-navbar-hover);
  }
`;

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
  const navigate = useNavigate();
  const { isPending, deleteCharacter } = useDeleteCharacter(() =>
    onClick(null)
  );
  return (
    <Box
      radius="tiny"
      onClick={() => onClick(character.id)}
      customStyles={BoxCustomStyles}
    >
      <Heading as="h3">{character.name}</Heading>
      <StyledElementBox>
        {character.class} & {character.race}
      </StyledElementBox>
      <StyledElementBox>{character.description}</StyledElementBox>
      {showButtons && (
        <div>
          <ButtonGroup justify="center">
            {!isPending && (
              <>
                  <Button
                    variation="primary"
                    size="medium"
                  onClick={(e) => {
                      e.preventDefault();
                      e.stopPropagation();
                      deleteCharacter(character.id);
                    }}
                >
                  Remove
                </Button>
                {character.campaignId && (
                  <Button
                    variation="primary"
                    size="medium"
                    onClick={(e) => {
                      e.preventDefault();
                      e.stopPropagation();
                      navigate(`/campaigns/${character.campaignId}`);
                    }}
                  >
                    Show campaign
                  </Button>
                )}
              </>
            )}
          </ButtonGroup>
        </div>
      )}
    </Box>
  );
}
