import { useQuery } from "@tanstack/react-query";
import { useParams } from "react-router-dom";
import { getMyCharacter } from "../../../services/apiCampaigns";

export function useMyCharacter() {
  const { campaignId } = useParams<{ campaignId: string }>();

  const {
    isLoading,
    data: character,
    isError,
    error,
  } = useQuery({
    queryKey: ["campaignCharacter", campaignId],
    queryFn: () => {
      if (campaignId) {
        return getMyCharacter(Number(campaignId));
      }
      return Promise.reject(new Error("Campaign ID is undefined"));
    },
    retry: false,
    enabled: !!campaignId, // Only run query if campaignId is defined
  });

  return { isLoading, character, error, isError };
}
