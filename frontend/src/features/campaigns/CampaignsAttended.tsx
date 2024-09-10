import Spinner from "../../ui/interactive/Spinner";
import Heading from "../../ui/text/Heading";
import { useCampaigns } from "./hooks/useCampaigns";

function CampaignsAttended() {
  const { isLoading, campaigns } = useCampaigns();

  if (isLoading) {
    return <Spinner />;
  }

  if (!campaigns || campaigns.length == 0)
    return <Heading as="h6">Didn't find any campaings</Heading>;

  return campaigns?.map((e) => (
    <div>{`Campaign: ${e.name} - ${e.description} - ${e.invitationLink}`}</div>
  ));
}

export default CampaignsAttended;
