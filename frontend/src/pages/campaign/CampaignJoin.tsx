import Heading from "../../ui/text/Heading";
import Box from "../../ui/containers/Box";
import styled from "styled-components";
import { useCampaign } from "../../features/campaigns/hooks/useCampaign";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import Spinner from "../../ui/interactive/Spinner";
import CharacterItemBox from "../../features/characters/CharacterItemBox";
import { useCharacters } from "../../features/characters/hooks/useCharacters";
import { addCharacterToCampaign } from "../../services/apiCampaigns";
import useCampaignJoin from "../../features/campaigns/hooks/useCampaignJoin";

const Container = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  gap: 100px;
`;

const BoxContent = styled.div`
  display: flex;
  flex-direction: column;
  padding-top: 4vh;
  gap: 50px;
`;

const CharacterListLayout = styled.div`
  display: flex;
  flex-wrap: wrap;
  flex-direction: row;
  justify-content: center;
  gap: 10px;
`;

function CampaignJoin() {
  const { campaign } = useCampaign();
  const { isLoading, characters, error } = useCharacters();
  const { joinCampaign, isPending } = useCampaignJoin();
  const { t } = useTranslation();

  const filteredCharacters = characters?.filter(
    // Filter out those Characters that are already assigned to a Campaign
    (character) => !character.campaignId
  );

  if (!campaign) {
    return <Spinner />;
  }

  if (isLoading || isPending) {
    return <Spinner />;
  }

  if (error) {
    return <>{`${error}`}</>;
  }

  return (
    <Container>
      <Box variation="squaredLarge">
        <BoxContent>
          <Heading as="h1">Looks like you want to join to a campaign</Heading>
          <Heading as="h1" color="textColor">
            {campaign?.name}
          </Heading>
          <Heading as="h2">Pick a character</Heading>
          <CharacterListLayout>
            {filteredCharacters && filteredCharacters?.length > 0 ? (
              filteredCharacters?.map((character) => (
                <CharacterItemBox
                  key={character.id}
                  character={character}
                  onClick={(chosenCharacterId: number) =>
                    joinCampaign({
                      campaignId: campaign.id,
                      characterId: chosenCharacterId,
                    })
                  }
                  showButtons={false}
                />
              ))
            ) : (
              <Heading as="h1" color="textColor">
                No characters eligible for joining.
              </Heading>
            )}
          </CharacterListLayout>
        </BoxContent>
      </Box>
    </Container>
  );
}

export default CampaignJoin;
