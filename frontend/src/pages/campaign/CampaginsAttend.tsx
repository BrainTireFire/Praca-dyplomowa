import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Line from "../../ui/separators/Line";
import CampaignList from "../../features/campaigns/CampaignList";
import Button from "../../ui/interactive/Button";
import { useTranslation } from "react-i18next";
import Modal from "../../ui/containers/Modal";
import CreateCampaign from "../../features/campaigns/CreateCampaign";
import CampaignListAttends from "../../features/campaigns/CampaignListAttends";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding-top: 15px;
  gap: 20px;
  align-items: center;
`;

export default function CampaginsAttend() {
  const { t } = useTranslation();

  return (
    <>
      <Container>
        <Heading as="h1">Campaings I attend</Heading>
        <Line size="percantage" bold="large" />
      </Container>
      <CampaignListAttends />
      <Container></Container>
    </>
  );
}
