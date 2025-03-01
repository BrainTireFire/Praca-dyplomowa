import styled from "styled-components";
import CampaignItemBox from "./CampaignItemBox";
import { useCampaigns } from "./hooks/useCampaigns";
import Spinner from "../../ui/interactive/Spinner";
import { Campaign } from "../../models/campaign";
import Heading from "../../ui/text/Heading";
import { useCampaignsAttend } from "./hooks/useCampaignsAttend";

const CampaignListLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 1rem;
  padding-left: 1.5vw;
  padding-right: 1.5vw;
`;

const ConatinerCenter = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
`;

export default function CampaignListAttends() {
  const { isLoading, campaigns } = useCampaignsAttend();

  if (isLoading) {
    return <Spinner />;
  }

  if (!campaigns || campaigns.length === 0) {
    return <ConatinerCenter>No campaigns available.</ConatinerCenter>;
  }

  return (
    <CampaignListLayout>
      {campaigns.map((campaign: Campaign) => (
        <CampaignItemBox key={campaign.id} campaign={campaign} />
      ))}
    </CampaignListLayout>
  );
}
