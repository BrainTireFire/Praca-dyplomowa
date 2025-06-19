import { useMutation, useQueryClient } from "@tanstack/react-query";
import { patchShopItem } from "../../../../services/apiShops";
import toast from "react-hot-toast";
import { ShopItem } from "../../../../models/shop";
import { useParams } from "react-router-dom";

export default function useUpdateShopItem() {
  const queryClient = useQueryClient();
  const { shopId } = useParams<{ shopId: string }>();
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
        queryKey: ["itemShopList", Number(shopId)],
      });
    },
    onError: (error) => toast.error(error.message),
  });

  return { updateShopItem, isPending };
}
