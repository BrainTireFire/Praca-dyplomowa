import styled from "styled-components";
import CharacterItemBox from "./CharacterItemBox";
import Box from "../../ui/containers/Box";
import Input from "../../ui/forms/Input";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Button from "../../ui/interactive/Button";
import HorizontalDiv from "../../ui/containers/HorizontalDiv";
import { useCharacters } from "./useCharacters";
import Spinner from "../../ui/interactive/Spinner";

const CharacterListLayout = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
  padding: 0;
`;

export default function CharacterList({
  onCharacterIdChosen,
}: {
  onCharacterIdChosen: any;
}) {
  const { isLoading, characters } = useCharacters();
  if (isLoading) {
    return <Spinner />;
  }
  return (
    <Box radius="tiny">
      Character list
      <HorizontalDiv>
        <FormRowVertical
          label="Search"
          assistiveText="Search by name, race, class or description"
        >
          <Input placeholder="Type here"></Input>
        </FormRowVertical>
        <FormRowVertical padlabel={true} padassistiveText={true}>
          <Button variation="primary">Create character</Button>
        </FormRowVertical>
      </HorizontalDiv>
      <Box radius="tiny">
        <CharacterListLayout>
          {characters?.map((character) => (
            <CharacterItemBox
              key={character.id}
              character={character}
              onClick={onCharacterIdChosen}
            />
          ))}
        </CharacterListLayout>
      </Box>
    </Box>
  );
}
