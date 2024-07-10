import styled from "styled-components";
import CharactersSheet from "../features/characters/CharactersSheet";
import CharacterList from "../features/characters/CharacterList";
import { useState } from "react";

const Container = styled.div`
  display: flex; /* Enable flexbox layout */
  width: 100%; /* Ensure the parent takes the full width of its container */
  box-sizing: border-box; /* Include padding and border in the element's total width and height */
`;
const Column1 = styled.div`
  flex: 1 0 30%; /* Flex-grow: 0, Flex-shrink: 1, Flex-basis: 20% */
  max-width: 70%; /* Ensure it does not exceed 20% */
  height: 100%;
  min-height: 100%; /* Ensure the parent takes the full width of its container */
  box-sizing: border-box;
`;
const Column2 = styled.div`
  flex: 1 1 auto; /* Flex-grow: 1, Flex-shrink: 1, Flex-basis: auto */
  box-sizing: border-box; /* Include padding and border in the element's total width and height */
`;

export default function Characters() {
  const [chosenCharacterId, setChosenCharacterId] = useState<number>(1);

  const handleChangeCharacter = (chosenCharacterId: number) => {
    console.log(chosenCharacterId);
    setChosenCharacterId(chosenCharacterId);
  };
  return (
    <Container>
      <Column1>
        <CharacterList
          onCharacterIdChosen={handleChangeCharacter}
        ></CharacterList>
      </Column1>
      <Column2>
        <CharactersSheet characterId={chosenCharacterId}></CharactersSheet>
      </Column2>
    </Container>
  );
}
