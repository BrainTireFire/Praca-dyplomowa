import styled from "styled-components";
import Line from "../../ui/separators/Line";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

const HeaderLeft = styled.div`
  display: flex;
  align-items: end;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1rem;
`;

export default function CampaignInstance() {
  const navigate = useNavigate();
  const { t } = useTranslation();

  return (
    <>
      <div>
        <Heading as="h4">Campaign #1</Heading>
        <Line size="small" />
      </div>
      <div>
        <HeaderLeft>
          <Heading as="h2" align="left">
            Details
          </Heading>
          <Button size="large">{t("campaignInstance.giveXP")}</Button>
          <Button size="large">{t("campaignInstance.shortRest")}</Button>
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
        </HeaderLeft>
        <Line size="verySmall" />
      </div>
      <div>
        <Heading as="h2" align="left">
          Description
        </Heading>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim
        veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea
        commodo consequat. Duis aute irure dolor in reprehenderit in voluptate
        velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
        occaecat cupidatat non proident, sunt in culpa qui officia deserunt
        mollit anim id est laborum.
        <Line size="verySmall" />
      </div>
      <div>
        <Heading as="h2" align="left">
          Game master
        </Heading>
        <div></div>
        <Line size="verySmall" />
      </div>
    </>
  );
}
