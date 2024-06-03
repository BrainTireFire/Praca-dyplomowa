import styled from "styled-components";
import Heading from "../ui/text/Heading";
import Line from "../ui/separators/Line";
import CampaignList from "../features/campaigns/CampaignList";

const CampaginsLayout = styled.main`
  min-height: 5vh;
  display: grid;
  align-content: center;
  justify-content: center;
  gap: 3rem;
`;

export default function Campagins() {
  return (
    <>
      <div>
        <Heading as="h4">My campaigns</Heading>
        <Line size="small" />
      </div>
      <CampaignList />
    </>
  );
}
