import styled from "styled-components";
import CharacterItemBox from "./CharacterItemBox";
import Box from "../../ui/containers/Box";
import Input from "../../ui/forms/Input";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Button from "../../ui/interactive/Button";
import HorizontalDiv from "../../ui/containers/HorizontalDiv";
import { useCharacters } from "./hooks/useCharacters";
import Spinner from "../../ui/interactive/Spinner";
import Modal from "../../ui/containers/Modal";
import NewCharacter from "./NewCharacter";

const CharacterListLayout = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
  padding: 0;
`;

const NoCharactersMessage = styled.div`
  text-align: center;
  font-style: italic;
  margin: 1rem 0;
`;

export default function CharacterList({
  onCharacterIdChosen,
}: {
  onCharacterIdChosen: any;
}) {
  const { isLoading, characters, error } = useCharacters();

  if (isLoading) {
    return <Spinner />;
  }

  if (error) {
    return <>{`${error}`}</>;
  }

  return (
    <Box radius="tiny">
      <HorizontalDiv>
        <FormRowVertical
          label="Search"
          assistiveText="Search by name, race, class or description"
        >
          <Input placeholder="Type here"></Input>
        </FormRowVertical>
        <Modal>
          <Modal.Open opens="BatchRollModal">
            <Button variation="primary">Create character</Button>
          </Modal.Open>
          <Modal.Window name="BatchRollModal">
            <NewCharacter></NewCharacter>
          </Modal.Window>
        </Modal>
      </HorizontalDiv>
      <Box radius="tiny">
        <CharacterListLayout>
          {characters && characters?.length > 0 ? (
            characters?.map((character) => (
              <CharacterItemBox
                key={character.id}
                character={character}
                onClick={onCharacterIdChosen}
              />
            ))
          ) : (
            <NoCharactersMessage>No characters available.</NoCharactersMessage>
          )}
        </CharacterListLayout>
      </Box>
    </Box>
  );
}
