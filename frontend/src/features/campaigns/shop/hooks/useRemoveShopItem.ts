import { useMutation, useQueryClient } from "@tanstack/react-query";
import { removeShopItem as removeShopItemApi } from "../../../../services/apiShops";
import toast from "react-hot-toast";
import { useParams } from "react-router-dom";

export default function useRemoveShopItem() {
  const { shopId } = useParams<{ shopId: string }>();
  const queryClient = useQueryClient();
  const { mutate: removeShopItem, isPending } = useMutation({
    mutationFn: ({ shopId, itemId }: { shopId: number; itemId: number }) =>
      removeShopItemApi(shopId, itemId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["itemShopList", Number(shopId)],
      });
    },
    onError: (err) => toast.error(err.message),
  });

  return { removeShopItem, isPending };
}
