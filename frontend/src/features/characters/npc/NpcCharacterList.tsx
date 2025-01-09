import styled from "styled-components";
import { useNpcCharacters } from "../hooks/useNpcCharacters";
import { useState } from "react";
import NewNpcCharacter from "./NewNpcCharacter";
import Box from "../../../ui/containers/Box";
import HorizontalDiv from "../../../ui/containers/HorizontalDiv";
import Spinner from "../../../ui/interactive/Spinner";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Modal from "../../../ui/containers/Modal";
import Button from "../../../ui/interactive/Button";
import CharacterItemBox from "../CharacterItemBox";

const Container = styled(Box)`
  max-height: 100%;
  overflow: hidden;
  display: flex;
  flex-direction: column;
`;

const ControlContainer = styled(HorizontalDiv)`
  max-height: 10%;
  height: 10%;
`;

const ListContainer = styled(Box)`
  overflow-x: auto;
  scrollbar-width: thin;
  scrollbar-color: var(--color-button-primary) var(--color-main-background);
  max-height: 90%;
  height: 90%;
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
  padding: 5;
`;

const NoCharactersMessage = styled.div`
  text-align: center;
  font-style: italic;
  margin: 1rem 0;
`;
export default function NpcCharacterList({
  onNpcCharacterIdChosen,
}: {
  onNpcCharacterIdChosen: any;
}) {
  const { isLoading, npcCharacters, error } = useNpcCharacters();
  const [searchTerm, setSearchTerm] = useState<string>("");

  if (isLoading) {
    return <Spinner />;
  }

  if (error) {
    return <>{`${error}`}</>;
  }

  const filteredCharacters = npcCharacters?.filter((character) => {
    const searchRegex = new RegExp(searchTerm, "i"); // Create a case-insensitive regex
    return (
      searchRegex.test(character.name || "") ||
      searchRegex.test(character.description || "") ||
      searchRegex.test(character.class || "") ||
      searchRegex.test(character.race || "")
    );
  });

  return (
    <Container radius="tiny">
      <ControlContainer>
        <FormRowVertical
          label="Search"
          assistiveText="Search by name, race, class or description"
        >
          <Input
            placeholder="Type here"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          ></Input>
        </FormRowVertical>
        <Modal>
          <Modal.Open opens="BatchRollModal">
            <Button variation="primary">Create new NPC</Button>
          </Modal.Open>
          <Modal.Window name="BatchRollModal">
            <NewNpcCharacter onCloseModal={undefined}></NewNpcCharacter>
          </Modal.Window>
        </Modal>
      </ControlContainer>
      <ListContainer radius="tiny">
        {filteredCharacters && filteredCharacters?.length > 0 ? (
          filteredCharacters?.map((character) => (
            <CharacterItemBox
              key={character.id}
              character={character}
              onClick={onNpcCharacterIdChosen}
              showButtons={true}
            />
          ))
        ) : (
          <NoCharactersMessage>No characters available.</NoCharactersMessage>
        )}
      </ListContainer>
    </Container>
  );
}
