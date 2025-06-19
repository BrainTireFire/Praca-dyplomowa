import { useMutation, useQueryClient } from "@tanstack/react-query";
import { buyItem } from "../../../../services/apiShops";
import { useParams } from "react-router-dom";
import { toast } from "react-hot-toast";

export function useBuyItem() {
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
    }) => buyItem(Number(shopId), characterId, itemId),
    onSuccess: (_data, variables) => {
      queryClient.invalidateQueries({
        queryKey: ["itemShopList", Number(shopId)],
      });
      queryClient.invalidateQueries({
        queryKey: ["shopCharacter", Number(campaignId)],
      });
    },
    onError: (error: any) => {
      toast.error(
        error?.message.message || "Something went wrong while buying the item."
      );
    },
  });
}
