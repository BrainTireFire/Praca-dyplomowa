import styled from "styled-components";
import CampaignItem from "./CampaignItemBox";

const CampaignListLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 1rem;
`;

export default function CampaignList() {
  const campaigns = [
    {
      id: 1,
      name: "Campaign 1",
      description: "This is the first campaign.",
      player: "John Doe",
    },
    {
      id: 2,
      name: "Campaign 2",
      description: "This is the second campaign.",
      player: "John Doe",
    },
    {
      id: 3,
      name: "Campaign 3",
      description: "This is the third campaign.",
      player: "John Doe",
    },
    {
      id: 3,
      name: "Campaign 3",
      description: "This is the third campaign.",
      player: "John Doe",
    },
    {
      id: 3,
      name: "Campaign 3",
      description: "This is the third campaign.",
      player: "John Doe",
    },
    {
      id: 3,
      name: "Campaign 3",
      description: "This is the third campaign.",
      player: "John Doe",
    },
    {
      id: 3,
      name: "Campaign 3",
      description: "This is the third campaign.",
      player: "John Doe",
    },
  ];

  return (
    <CampaignListLayout>
      {campaigns.map((campaign) => (
        <CampaignItem key={campaign.id} campaign={campaign} />
      ))}
    </CampaignListLayout>
  );
}
