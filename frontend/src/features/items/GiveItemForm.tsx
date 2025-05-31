import styled from "styled-components";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import Spinner from "../../ui/interactive/Spinner";
import CharacterItemBox from "../characters/CharacterItemBox";
import { useCharacters } from "../characters/hooks/useCharacters";
import { useCampaign } from "../campaigns/hooks/useCampaign";
import useGiveItem from "./hooks/useGiveItem";
import { Navigate, useNavigate } from "react-router-dom";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  justify-content: center;
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
  const characters =
    campaign?.members.filter((e) => !e.itemIds.includes(itemId!)) || [];
  const navigate = useNavigate();
  const { giveItem, isPending } = useGiveItem();

  if (isLoading || isPending) return <Spinner />;

  if (isInvalidId || itemId === null) {
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
      <Box variation="squaredSmall">
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
                onClick={async (chosenCharacterId: number) => {
                  giveItem({ itemId, characterId: chosenCharacterId });
                  navigate(0);
                }}
                showButtons={false}
              />
            ))}
        </CharacterListLayout>
      </Box>
    </Container>
  );
}
