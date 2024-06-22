import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import Line from "../../ui/separators/Line";
import CampaignList from "../../features/campaigns/CampaignList";
import Button from "../../ui/interactive/Button";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

const CampaginsLayout = styled.main`
  min-height: 5vh;
  display: grid;
  align-content: center;
  justify-content: center;
  gap: 3rem;
`;

const ButtonContainer = styled.div`
  display: flex;
  justify-content: end;
  gap: 2rem;
`;

const HeaderLeft = styled.div`
  display: flex;
  align-items: end;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1rem;
`;

export default function Campagins() {
  const navigate = useNavigate();
  const { t } = useTranslation();

  return (
    <>
      <div>
        <Heading as="h4">My campaigns</Heading>
        <Line size="small" />
      </div>
      <div>
        <HeaderLeft>
          <Heading as="h2" align="left">
            Campaigns
          </Heading>
          <Button size="large" onClick={() => navigate(`/login`)}>
            {t("campaigns.create.text")}
          </Button>
        </HeaderLeft>
        <Line size="verySmall" />
      </div>
      <CampaignList />
    </>
  );
}
