import { useMutation, useQueryClient } from "@tanstack/react-query";
import { removeShop as removeShopApi } from "../../../../services/apiShops";
import toast from "react-hot-toast";

export default function useRemoveShop() {
  const queryClient = useQueryClient();
  const { mutate: removeShop, isPending } = useMutation({
    mutationFn: (shopId: number) => removeShopApi(shopId),
    onSuccess: () => {
      toast.success("Shop has been removed");
      queryClient.invalidateQueries({
        queryKey: ["shops"],
      });
    },
    onError: (err) => toast.error(err.message),
  });

  return { removeShop, isPending };
}
