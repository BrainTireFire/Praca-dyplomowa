import styled from "styled-components";
import CampaignItemBox from "./CampaignItemBox";
import { useCampaigns } from "./useCampaigns";
import Spinner from "../../ui/interactive/Spinner";

const CampaignListLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 1rem;
`;

export default function CampaignList() {
  const { isLoading, campaigns } = useCampaigns();

  if (isLoading) {
    return <Spinner />;
  }

  if (!campaigns || campaigns.length === 0) {
    return <div>No campaigns available.</div>;
  }

  return (
    <CampaignListLayout>
      {campaigns.map((campaign: Campaigns) => (
        <CampaignItemBox key={campaign.id} campaign={campaign} />
      ))}
    </CampaignListLayout>
  );
}
