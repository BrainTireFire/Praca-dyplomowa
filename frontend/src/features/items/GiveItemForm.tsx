import styled from "styled-components";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import Spinner from "../../ui/interactive/Spinner";
import CharacterItemBox from "../characters/CharacterItemBox";
import { useCharacters } from "../characters/hooks/useCharacters";
import { useCampaign } from "../campaigns/hooks/useCampaign";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  height: 10%;
  max-height: 100%;
`;

const CharacterListLayout = styled.div`
  display: flex;
  flex-wrap: wrap;
  flex-direction: row;
  justify-content: center;
  gap: 8px;
`;

export default function GiveItemForm({ itemId }: { itemId: number | null }) {
  const { isLoading, campaign, isInvalidId } = useCampaign();
  const characters = campaign?.members;

  if (isLoading) return <Spinner />;

  if (isInvalidId) {
    return (
      <Container>
        <Box variation="squaredMedium">
          <Heading as="h2">No active Campaign</Heading>
          <Heading as="h3" color="textColor">
            No active campaign was found.
          </Heading>
        </Box>
      </Container>
    );
  }

  return (
    <Container>
      <Box variation="squaredMedium">
        <Heading as="h2">Give Item</Heading>
        <Heading as="h3" color="textColor">
          Select a character to give the item to
        </Heading>
        <CharacterListLayout>
          {characters &&
            characters.length > 0 &&
            characters.map((e) => (
              <CharacterItemBox
                key={e.id}
                character={e}
                onClick={(chosenCharacterId: number) => {}}
                showButtons={false}
              />
            ))}
        </CharacterListLayout>
      </Box>
    </Container>
  );
}
