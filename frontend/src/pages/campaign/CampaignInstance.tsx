import styled from "styled-components";
import Line from "../../ui/separators/Line";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import MemberBox from "../../features/campaigns/MemberBox";
import InputCopyToClipboard from "../../ui/forms/InputCopyToClipboard";
import Modal from "../../ui/containers/Modal";
import ShortRest from "./ShortRestModal";

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
  /* justify-content: space-between; */
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

const members = [
  {
    member: "Agent007",
    character: "Michael",
    level: 1,
    race: "Wolf",
    classs: "Fighter",
    img: "../avatar.jpg",
  },
  {
    member: "xXDestroyerXx",
    character: "John",
    level: 11,
    race: "Human",
    classs: "Mage",
    img: "../avatar.jpg",
  },
  {
    member: "murderOnStick",
    character: "Fred",
    level: 6,
    race: "Wyvern",
    classs: "Monk",
    img: "../avatar.jpg",
  },
];

export default function CampaignInstance() {
  const navigate = useNavigate();
  const { t } = useTranslation();

  return (
    <>
      <div>
        <Heading as="h4">Campaign #1</Heading>
        <Line size="percantage" bold="large" />
      </div>
      <div>
        <HeaderLeft>
          <Heading as="h2" align="left">
            Details
          </Heading>
          <HeaderButtons>
            <Button size="large">{t("campaignInstance.giveXP")}</Button>
            <Modal>
              <Modal.Open opens="shortRest">
                <Button size="large">{t("campaignInstance.shortRest")}</Button>
              </Modal.Open>
              <Modal.Window name="shortRest">
                <ShortRest />
              </Modal.Window>
            </Modal>
            <Button size="large">{t("campaignInstance.longRest")}</Button>
            <Button size="large" onClick={() => navigate(`/session`)}>
              {t("campaignInstance.session")}
            </Button>
            <Button size="large" onClick={() => navigate(`/shops`)}>
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
          Game master
        </Heading>
        <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
          <Avatar>
            <img src="../avatar.jpg" alt="avatar"></img>
          </Avatar>
          <span>Game master - disaster</span>
        </div>
        <Line size="percantage" />
      </div>
      <div>
        <Heading as="h2" align="left">
          Members
        </Heading>
        <div
          style={{
            height: "275px",
            width: "2000px",
            display: "flex",
            gridTemplateColumns: "repeat(auto-fill, minmax(250px, 1fr))",
            gap: "20px",
            paddingBottom: "20px",
          }}
        >
          {members.map((e) => (
            <MemberBox
              member={e.member}
              character={e.character}
              level={e.level}
              race={e.race}
              classs={e.classs}
              img={e.img}
            />
          ))}
        </div>
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
