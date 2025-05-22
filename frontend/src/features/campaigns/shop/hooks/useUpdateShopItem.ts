import { useMutation, useQueryClient } from "@tanstack/react-query";
import { patchShopItem } from "../../../../services/apiShops";
import toast from "react-hot-toast";
import { ShopItem } from "../../../../models/shop";

export default function useUpdateShopItem() {
  const queryClient = useQueryClient();
  const { mutate: updateShopItem, isPending } = useMutation({
    mutationFn: ({
      shopId,
      shopItem,
    }: {
      shopId: number;
      shopItem: ShopItem;
    }) => patchShopItem(shopId, shopItem),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["shopItems"],
      });
    },
    onError: (error) => toast.error(error.message),
  });

  return { updateShopItem, isPending };
}
