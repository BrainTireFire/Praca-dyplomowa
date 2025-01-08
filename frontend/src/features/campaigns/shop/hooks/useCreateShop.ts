import { useMutation, useQueryClient } from "@tanstack/react-query";
import { postShop } from "../../../../services/apiShops";
import toast from "react-hot-toast";
import { ShopInsertDto } from "../../../../models/shop";
import { useNavigate } from "react-router-dom";

function useCreateShop() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { mutate: createShop, isPending } = useMutation({
    mutationFn: (shop: ShopInsertDto) => postShop(shop),
    onSuccess: (shopId: number) => {
      toast.success("Shop has been created");
      queryClient.invalidateQueries({
        queryKey: ["campaigns", "shops"],
      });
      navigate(0);
    },
    onError: (error) => toast.error(error.message),
  });

  return { createShop, isPending };
}

export default useCreateShop;
