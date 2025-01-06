import styled from "styled-components";
import CampaignItemBox from "./CampaignItemBox";
import { useCampaigns } from "./hooks/useCampaigns";
import Spinner from "../../ui/interactive/Spinner";
import { Campaign } from "../../models/campaign";
import Heading from "../../ui/text/Heading";

const CampaignListLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 1rem;
  padding-left: 1.5vw;
  padding-right: 1.5vw;
`;

export default function CampaignList() {
  const { isLoading, campaigns } = useCampaigns();

  if (isLoading) {
    return <Spinner />;
  }

  if (!campaigns || campaigns.length === 0) {
    return <Heading as="h1">No campaigns available.</Heading>;
  }

  return (
    <CampaignListLayout>
      {campaigns.map((campaign: Campaign) => (
        <CampaignItemBox key={campaign.id} campaign={campaign} />
      ))}
    </CampaignListLayout>
  );
}
