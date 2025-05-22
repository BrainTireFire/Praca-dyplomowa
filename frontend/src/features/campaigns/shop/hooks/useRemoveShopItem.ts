import { useMutation, useQueryClient } from "@tanstack/react-query";
import { removeShopItem as removeShopItemApi } from "../../../../services/apiShops";
import toast from "react-hot-toast";

export default function useRemoveShopItem() {
  const queryClient = useQueryClient();
  const { mutate: removeShopItem, isPending } = useMutation({
    mutationFn: ({
      shopId,
      itemId,
      quantity,
    }: {
      shopId: number;
      itemId: number;
      quantity: number;
    }) => removeShopItemApi(shopId, itemId, quantity),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["shopsItems"],
      });
    },
    onError: (err) => toast.error(err.message),
  });

  return { removeShopItem, isPending };
}
