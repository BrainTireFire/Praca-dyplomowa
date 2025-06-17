import { useQuery } from "@tanstack/react-query";
import { getShopCharacter } from "../../../../services/apiShops";
import { useParams } from "react-router-dom";

export function useShopCharacter() {
  const { campaignId } = useParams<{ campaignId: string }>();
  const {
    isLoading,
    data: shopCharacter,
    error,
  } = useQuery({
    queryKey: ["shopCharacter", campaignId],
    queryFn: () => getShopCharacter(Number(campaignId)),
  });

  return { isLoading, shopCharacter, error };
}
