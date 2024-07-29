import styled from "styled-components";
import Line from "../../ui/separators/Line";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import MemberBox from "./MemberBox";
import InputCopyToClipboard from "../../ui/forms/InputCopyToClipboard";
import Modal from "../../ui/containers/Modal";
import ShortRest from "./ShortRestModal";
import GiveXP from "./GiveXP";
import { useCampaign } from "./useCampaign";
import Spinner from "../../ui/interactive/Spinner";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  align-items: center;
`;

const MemberContainer = styled.div`
  display: grid;
  grid-template-columns: repeat(3, 2fr);
  gap: 20px;
  margin: 20px;
`;

const HeaderLeft = styled.div`
  display: flex;
  align-items: end;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1rem;
`;

const HeaderButtons = styled.div`
  display: flex;
  align-items: end;
  gap: 1rem;
  margin-bottom: 1rem;
`;

const DescriptionStyled = styled.div`
  margin-bottom: 1rem;
  width: 50%;
`;

const Avatar = styled.div`
  width: 70px;
  height: 70px;
  border-radius: 50%;
  padding: 10px;
`;

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

  const { id, name, description, gameMaster, members, shops }: Campaign =
    campaign;

  return (
    <>
      <Container>
        <Heading as="h4">
          Campaign #{id} - {name}
        </Heading>
        <Line size="percantage" bold="large" />
      </Container>
      <div>
        <HeaderLeft>
          <Heading as="h2" align="left">
            Details
          </Heading>
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
                <Button size="large">{t("campaignInstance.shortRest")}</Button>
              </Modal.Open>
              <Modal.Window name="ShortRestModal">
                <ShortRest membersList={members} />
              </Modal.Window>
            </Modal>
            <Button size="large">{t("campaignInstance.longRest")}</Button>
            <Button
              size="large"
              onClick={() => navigate(`/campaigns/session/1`)}
            >
              {t("campaignInstance.session")}
            </Button>
            <Button size="large" onClick={() => navigate("shops")}>
              {t("campaignInstance.shops")}
            </Button>
            <Button size="large" onClick={() => navigate(`/encounter`)}>
              {t("campaignInstance.createEncounter")}
            </Button>
            <Button size="large">{t("campaignInstance.remove")}</Button>
          </HeaderButtons>
        </HeaderLeft>
        <Line size="percantage" />
      </div>
      <div>
        <Heading as="h2" align="left">
          Description
        </Heading>
        <DescriptionStyled>
          {description}
          <br />
          <br />
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
          aliquip ex ea commodo consequat. Duis aute irure dolor in
          reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
          pariatur. Excepteur sint occaecat cupidatat non proident, sunt in
          culpa qui officia deserunt mollit anim id est laborum.
        </DescriptionStyled>
        <Line size="percantage" />
      </div>
      <div>
        <Heading as="h2" align="left">
          Game Master
        </Heading>
        <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
          <Avatar>
            <img src={gameMaster.img} alt="avatar"></img>
          </Avatar>
          <span>
            {gameMaster.name} - {gameMaster.description}
          </span>
        </div>
        <Line size="percantage" />
      </div>
      <div>
        <Heading as="h2" align="left">
          Members
        </Heading>
        <MemberContainer>
          {members.map((e) => (
            <MemberBox>{e}</MemberBox>
          ))}
        </MemberContainer>
        <Line size="percantage" />
      </div>
      <div style={{ display: "flex", gap: "30px" }}>
        <Heading as="h2" align="left">
          Link for invite to the campaign
        </Heading>
        <InputCopyToClipboard valueDefault="http://ddbutbetter.com/1234" />
      </div>
    </>
  );
}
