import Heading from "../../ui/text/Heading";
import Box from "../../ui/containers/Box";
import styled from "styled-components";
import { useCampaign } from "./hooks/useCampaign";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import Spinner from "../../ui/interactive/Spinner";
import CharacterItemBox from "../characters/CharacterItemBox";
import { useCharacters } from "../characters/hooks/useCharacters";
import { addCharacterToCampaign } from "../../services/apiCampaigns";

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
  const navigate = useNavigate();
  const { t } = useTranslation();

  if (!campaign) {
    return <Spinner />;
  }

  if (isLoading) {
    return <Spinner />;
  }

  if (error) {
    return <>{`${error}`}</>;
  }

  const handleChangeCharacter = async (chosenCharacterId: number) => {
    await addCharacterToCampaign(campaign.id, chosenCharacterId);
    navigate(`/campaigns/${campaign.id}`);
  };

  return (
    <Container>
      <Box variation="squaredLarge">
        {}
        <BoxContent>
          <Heading as="h1">Looks like you want to join to a campaign</Heading>
          <Heading as="h1" color="textColor">
            {campaign?.name}
          </Heading>
          <Heading as="h2">Pick a character</Heading>
          <CharacterListLayout>
            {characters && characters?.length > 0 ? (
              characters?.map((character) => (
                <CharacterItemBox
                  key={character.id}
                  character={character}
                  onClick={handleChangeCharacter}
                  showButtons={false}
                />
              ))
            ) : (
              <Heading as="h1" color="textColor">
                No characters available.
              </Heading>
            )}
          </CharacterListLayout>
        </BoxContent>
      </Box>
    </Container>
  );
}

export default CampaignJoin;
