import styled from "styled-components";
import CharacterItemBox from ".././CharacterItemBox";
import Box from "../../../ui/containers/Box";
import Input from "../../../ui/forms/Input";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Button from "../../../ui/interactive/Button";
import HorizontalDiv from "../../../ui/containers/HorizontalDiv";
import Spinner from "../../../ui/interactive/Spinner";
import Modal from "../../../ui/containers/Modal";
import NewNpcCharacter from "./NewNpcCharacter";
import Heading from "../../../ui/text/Heading";
import { useNpcCharacters } from "../hooks/useNpcCharacters";

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

export default function NpcCharacterList({
  onNpcCharacterIdChosen,
}: {
  onNpcCharacterIdChosen: any;
}) {
  const { isLoading, npcCharacters, error } = useNpcCharacters();

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
            <Button variation="primary">Create npc character</Button>
          </Modal.Open>
          <Modal.Window name="BatchRollModal">
            <NewNpcCharacter></NewNpcCharacter>
          </Modal.Window>
        </Modal>
      </HorizontalDiv>
      <Heading as="h2">NPC Characters</Heading>
      <Box radius="tiny">
        <CharacterListLayout>
          {npcCharacters && npcCharacters?.length > 0 ? (
            npcCharacters?.map((npcCharacter) => (
              <CharacterItemBox
                key={npcCharacter.id}
                character={npcCharacter}
                onClick={onNpcCharacterIdChosen}
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
