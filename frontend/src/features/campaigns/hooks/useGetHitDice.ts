import { useQuery } from "@tanstack/react-query";
import { getHitDice as getHitDiceApi } from "../../../services/apiCampaigns";


export function useGetHitDice(campaignId: number) {
  const {
    isLoading,
    data: hitDice,
    error,
  } = useQuery({
    queryKey: ["hitDice", campaignId],
    queryFn: () => getHitDiceApi(campaignId),
  });

  return { isLoading, hitDice, error };
}