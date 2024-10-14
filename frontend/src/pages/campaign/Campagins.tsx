import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Line from "../../ui/separators/Line";
import CampaignList from "../../features/campaigns/CampaignList";
import Button from "../../ui/interactive/Button";
import { useTranslation } from "react-i18next";
import Modal from "../../ui/containers/Modal";
import CreateCampaign from "../../features/campaigns/CreateCampaign";

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

export default function Campagins() {
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
          <Modal>
            <Modal.Open opens="CreateCampaign">
              <Button size="large">{t("campaigns.create.text")}</Button>
            </Modal.Open>
            <Modal.Window name="CreateCampaign">
              <CreateCampaign />
            </Modal.Window>
          </Modal>
        </CampaignHeader>
        <Line size="percantage" />
      </Container>
      <CampaignList />
    </>
  );
}
