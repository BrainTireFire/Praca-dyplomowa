import { useQuery } from "@tanstack/react-query";
import { getCampaign } from "../../../services/apiCampaigns";
import { useParams } from "react-router-dom";

const decode = (obfuscated: string): number => {
  const reversed = obfuscated.split("").reverse().join("");
  const decodedString = atob(reversed);
  const originalString = decodedString.replace("zoNK", "");
  return parseInt(originalString, 10);
};

export function useCampaign() {
  const { campaignId } = useParams<{ campaignId: string }>();

  let ID: number;

  if (isNaN(Number(campaignId))) {
    if (campaignId === undefined) throw new Error("Campaign ID is undefined");
    ID = decode(campaignId); // Use decode if not a number
  } else {
    ID = Number(campaignId);
  }

  const {
    isLoading,
    data: campaign,
    error,
  } = useQuery({
    queryKey: ["campaign", ID],
    queryFn: () => {
      if (ID) {
        return getCampaign(ID);
      }
      return Promise.reject(new Error("Campaign ID is undefined"));
    },
    retry: false,
    enabled: !!ID, // Only run query if campaignId is defined
  });

  return { isLoading, campaign, error };
}
