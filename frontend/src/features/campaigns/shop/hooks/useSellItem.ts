import { useMutation, useQueryClient } from "@tanstack/react-query";
import { sellItem } from "../../../../services/apiShops";
import { useParams } from "react-router-dom";

export function useSellItem() {
  const queryClient = useQueryClient();
  const { shopId } = useParams<{ shopId: string }>();
  const { campaignId } = useParams<{ campaignId: string }>();
  return useMutation({
    mutationFn: ({
      characterId,
      itemId,
    }: {
      characterId: number;
      itemId: number;
    }) => sellItem(Number(shopId), characterId, itemId),
    onSuccess: (_data, variables) => {
      queryClient.invalidateQueries({
        queryKey: ["itemShopList", Number(shopId)],
      });
      queryClient.invalidateQueries({
        queryKey: ["shopCharacter", Number(campaignId)],
      });
    },
  });
}
