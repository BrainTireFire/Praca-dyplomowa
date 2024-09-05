import { useQuery } from "@tanstack/react-query";
import { getCampaign } from "../../services/apiCampaigns";
import { useParams } from "react-router-dom";

export function useCampaign() {
  const { campaignId } = useParams<{ campaignId: string }>();

  const {
    isLoading,
    data: campaign,
    error,
  } = useQuery({
    queryKey: ["campaign", campaignId],
    queryFn: () => {
      if (campaignId) {
        return getCampaign(Number(campaignId));
      }
      return Promise.reject(new Error("Campaign ID is undefined"));
    },
    retry: false,
    enabled: !!campaignId, // Only run query if campaignId is defined
  });

  return { isLoading, campaign, error };
}
