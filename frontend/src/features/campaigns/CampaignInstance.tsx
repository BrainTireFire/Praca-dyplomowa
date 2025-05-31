import styled from "styled-components";
import Line from "../../ui/separators/Line";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import InputCopyToClipboard from "../../ui/forms/InputCopyToClipboard";
import Modal from "../../ui/containers/Modal";
import { useCampaign } from "./hooks/useCampaign";
import useKickCharacter from "./hooks/useKickCharacter";
import Spinner from "../../ui/interactive/Spinner";
import CharacterDetailBox from "./CharacterDetailBox";
import { Campaign } from "../../models/campaign";
import useRemoveCampaign from "./hooks/useRemoveCampaign";
import ConfirmDelete from "../../ui/containers/ConfirmDelete";
import GiveXP from "./GiveXP";
import useLongRest from "./hooks/useLongRest";
import { useQueryClient } from "@tanstack/react-query";
import ShortRestModalGM from "./ShortRestModalGM";
import ShortRestModalCharacter from "./ShortRestModalCharacter";
import Box from "../../ui/containers/Box";

const Container = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  width: 100%;
  gap: 3%;
  justify-items: center;
  margin-top: 1%;
`;

const CharacterContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
  & > * {
    min-width: 180px;
  }
`;

const HeaderButtons = styled.div`
  display: flex;
  gap: 1rem;
  /* margin-bottom: 1rem; */
`;

export default function CampaignInstance() {
  const { isLoading, campaign, isInvalidId } = useCampaign();
  const queryClient = useQueryClient();
  const { removeCampaign, isPending: isRemoving } = useRemoveCampaign();
  const { kickCharacter, isPending: isKicking } = useKickCharacter();
  const { isPending: isPendingLongRest, longRest } = useLongRest(
    campaign?.id!,
    () => {}
  );
  const navigate = useNavigate();
  const { t } = useTranslation();

  if (isLoading || isRemoving || isKicking) {
    return <Spinner />;
  }

  if (!campaign) {
    return <div>{t("campaign.error.notFound")}</div>;
  }

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

  const { id, name, description, members }: Campaign = campaign;

  return (
    <Container>
      <Heading as="h1">{name}</Heading>
      <Line size="percantage" bold="large" />
      {campaign.isGameMaster && (
        <>
          <div>
            <HeaderButtons>
              <Modal>
                <Modal.Open opens="GiveXP">
                  <Button size="large">{t("campaignInstance.giveXP")}</Button>
                </Modal.Open>
                <Modal.Window name="GiveXP">
                  <GiveXP membersList={members} />
                </Modal.Window>
              </Modal>
              <Modal>
                <Modal.Open opens="ShortRestModal">
                  <Button size="large">
                    {t("campaignInstance.shortRest")}
                  </Button>
                </Modal.Open>
                <Modal.Window name="ShortRestModal">
                  <ShortRestModalGM
                    membersList={members}
                    onCloseModal={() => {}}
                  />
                </Modal.Window>
              </Modal>
              <Button size="large" onClick={() => longRest()}>
                {t("campaignInstance.longRest")}
              </Button>
              <Button
                size="large"
                onClick={() => navigate(`/campaigns/${id}/createSession`)}
              >
                {t("campaignInstance.session")}
              </Button>
              <Button size="large" onClick={() => navigate("shops")}>
                {t("campaignInstance.shops")}
              </Button>
              <Button
                size="large"
                onClick={() => navigate(`/campaigns/${id}/encounters`)}
              >
                Manage encounters
              </Button>
            </HeaderButtons>
          </div>

          <Line size="percantage" />
        </>
      )}
      {!campaign.isGameMaster && (
        <>
          <div>
            <HeaderButtons>
              <Modal>
                <Modal.Open opens="ShortRestModal">
                  <Button size="large">
                    {t("campaignInstance.shortRest")}
                  </Button>
                </Modal.Open>
                <Modal.Window name="ShortRestModal">
                  <ShortRestModalCharacter onCloseModal={() => {}} />
                </Modal.Window>
              </Modal>
            </HeaderButtons>
          </div>
        </>
      )}
      <Heading as="h2">Description</Heading>
      {description === "" ? (
        <p>No description</p>
      ) : (
        <div style={{ width: "73%", textAlign: "justify" }}>{description}</div>
      )}
      <Line size="percantage" />

      <Heading as="h2">Game Master</Heading>
      <p>{campaign.gameMaster}</p>

      <Line size="percantage" />
      <Heading as="h2">Members</Heading>
      <CharacterContainer>
        {members && members.length > 0 ? (
          members.map((e) => (
            <CharacterDetailBox
              key={e.id}
              handleKickCharacter={() => kickCharacter(e.id)}
              gameMaster={campaign.isGameMaster}
            >
              {e}
            </CharacterDetailBox>
          ))
        ) : (
          <p>There are no members in this campaign</p>
        )}
      </CharacterContainer>
      {campaign.isGameMaster && (
        <>
          <Line size="percantage" />
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              gap: "1%",
              alignItems: "center",
            }}
          >
            <Heading as="h2">Link for invite to the campaign</Heading>
            <InputCopyToClipboard campaignId={id} />
          </div>
          <Line size="percantage" />
          <Modal>
            <Modal.Open opens="ConfirmDelete">
              <Button size="large">{t("campaignInstance.remove")}</Button>
            </Modal.Open>
            <Modal.Window name="ConfirmDelete">
              <ConfirmDelete
                resourceName={name + " campaign"}
                onConfirm={() => removeCampaign(campaign.id)}
              />
            </Modal.Window>
          </Modal>
        </>
      )}
    </Container>
  );
}
