import styled from "styled-components";
import CharactersSheet from "../features/characters/CharactersSheet";
import CharacterList from "../features/characters/CharacterList";
import { useState } from "react";
import { CharacterIdContext } from "../features/characters/contexts/CharacterIdContext";
import { useNavigate, useSearchParams } from "react-router-dom";

const Container = styled.div`
  display: flex; /* Enable flexbox layout */
  width: 100%; /* Ensure the parent takes the full width of its container */
  height: 100%;
  box-sizing: border-box; /* Include padding and border in the element's total width and height */
`;
const Column1 = styled.div`
  flex: 1 0 30%; /* Flex-grow: 0, Flex-shrink: 1, Flex-basis: 20% */
  max-width: 20%; /* Ensure it does not exceed 20% */
  height: 100%;
  min-height: 100%; /* Ensure the parent takes the full width of its container */
  box-sizing: border-box;
`;
const Column2 = styled.div`
  flex: 1 1 auto; /* Flex-grow: 1, Flex-shrink: 1, Flex-basis: auto */
  box-sizing: border-box; /* Include padding and border in the element's total width and height */
`;

export default function Characters() {
  const navigate = useNavigate();

  const handleChangeCharacter = (chosenCharacterId: number) => {
    navigate(`/characters?id=${chosenCharacterId}`);
  };

  const [searchParams] = useSearchParams();
  const characterId = searchParams.get("id");
  return (
    <Container>
      <Column1>
        <CharacterList
          onCharacterIdChosen={handleChangeCharacter}
        ></CharacterList>
      </Column1>
      <Column2>
        {!!Number(characterId) && (
          <CharacterIdContext.Provider
            value={{
              characterId: Number(characterId),
              // setCharacterId: setChosenCharacterId,
            }}
          >
            <CharactersSheet></CharactersSheet>
          </CharacterIdContext.Provider>
        )}
      </Column2>
    </Container>
  );
}
