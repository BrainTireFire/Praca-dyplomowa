import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Line from "../../ui/separators/Line";
import CampaignList from "../../features/campaigns/CampaignList";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 20px;
  align-items: center;
`;

const CampaignHeader = styled.div`
  display: flex;
  align-items: end;
  justify-content: space-between;
  width: 100%;
`;

const HeaderButtons = styled.div`
  display: flex;
  align-items: end;
  /* justify-content: space-between; */
  gap: 1rem;
  margin-bottom: 1rem;
`;

export default function Campagins() {
  const navigate = useNavigate();
  const { t } = useTranslation();

  return (
    <>
      <Container>
        <Heading as="h4">{t("campaigns.heading")}</Heading>
        <Line size="percantage" bold="large" />
        <CampaignHeader>
          <Heading as="h2" align="left">
            {t("campaigns.header")}
          </Heading>
          <Button size="large" onClick={() => navigate(`/login`)}>
            {t("campaigns.create.text")}
          </Button>
        </CampaignHeader>
        <Line size="percantage" />
      </Container>
      <CampaignList />
    </>
  );
}
