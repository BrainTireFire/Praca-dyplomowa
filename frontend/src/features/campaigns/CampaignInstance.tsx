import styled from "styled-components";
import Line from "../../ui/separators/Line";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import InputCopyToClipboard from "../../ui/forms/InputCopyToClipboard";
import Modal from "../../ui/containers/Modal";
import { useCampaign } from "./hooks/useCampaign";
import Spinner from "../../ui/interactive/Spinner";
import CharacterDetailBox from "./CharacterDetailBox";
import { Campaign } from "../../models/campaign";
import { removeCampaign } from "../../services/apiCampaigns";
import ConfirmDelete from "../../ui/containers/ConfirmDelete";

const Container = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  width: 100%;
  gap: 3%;
  justify-items: center;
  margin-top: 1%;
`;

const CharacterContainer = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
  max-width: 70vw;
  gap: 2%;
`;

const HeaderButtons = styled.div`
  display: flex;
  gap: 1rem;
  /* margin-bottom: 1rem; */
`;

const encode = (number: number): string => {
  let base64 = btoa(number.toString() + "zoNK");
  base64 = base64.split("").reverse().join("");
  return base64;
};

export default function CampaignInstance() {
  const { isLoading, campaign } = useCampaign();
  const navigate = useNavigate();
  const { t } = useTranslation();

  if (isLoading) {
    return <Spinner />;
  }

  if (!campaign) {
    return <div>{t("campaign.error.notFound")}</div>;
  }

  const { id, name, description, members }: Campaign = campaign;

  const handleRemove = async () => {
    await removeCampaign(campaign.id);
    navigate("/campaigns");
  };

  return (
    <Container>
      <Heading as="h1">{name}</Heading>
      <Line size="percantage" bold="large" />
      <div>
        <HeaderButtons>
          <Modal>
            <Modal.Open opens="GiveXP">
              <Button size="large">{t("campaignInstance.giveXP")}</Button>
            </Modal.Open>
            {/* <Modal.Window name="GiveXP">
                <GiveXP membersList={members} />
              </Modal.Window> */}
          </Modal>
          <Modal>
            <Modal.Open opens="ShortRestModal">
              <Button size="large">{t("campaignInstance.shortRest")}</Button>
            </Modal.Open>
            {/*    <Modal.Window name="ShortRestModal">
                <ShortRest membersList={members} />
              </Modal.Window>*/}
          </Modal>
          <Button size="large">{t("campaignInstance.longRest")}</Button>
          <Button size="large" onClick={() => navigate(`/campaigns/session/1`)}>
            {t("campaignInstance.session")}
          </Button>
          <Button size="large" onClick={() => navigate("shops")}>
            {t("campaignInstance.shops")}
          </Button>
          <Button size="large" onClick={() => navigate(`/campaigns/encounter`)}>
            {t("campaignInstance.createEncounter")}
          </Button>
        </HeaderButtons>
      </div>
      <Line size="percantage" />

      <Heading as="h2">Description</Heading>
      <div style={{ width: "73%", textAlign: "justify" }}>{description}</div>

      <Line size="percantage" />
      <div>
        <Heading as="h2">Game Master</Heading>
        {/* {gameMaster.name} - {gameMaster.description} */}
      </div>
      <Line size="percantage" />

      <Heading as="h2">Members</Heading>
      <CharacterContainer>
        {members.length > 0 ? (
          members.map((e) => (
            <CharacterDetailBox key={e.id}>{e}</CharacterDetailBox>
          ))
        ) : (
          <Heading as="h2">There are no members in this campaign</Heading>
        )}
      </CharacterContainer>

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
        <InputCopyToClipboard
          valueDefault={`localhost:5173/join/${encode(id)}`}
        />
      </div>
      <Line size="percantage" />
      <Modal>
        <Modal.Open opens="ConfirmDelete">
          <Button size="large" onClick={handleRemove}>
            {t("campaignInstance.remove")}
          </Button>
        </Modal.Open>
        <Modal.Window name="ConfirmDelete">
          <ConfirmDelete
            resourceName={name + " campaign"}
            onConfirm={handleRemove}
          />
        </Modal.Window>
      </Modal>
    </Container>
  );
}
