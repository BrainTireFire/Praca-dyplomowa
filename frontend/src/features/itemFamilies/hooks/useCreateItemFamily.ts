import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { postItemFamily } from "../../../services/apiItemFamilies";
import { ItemFamily } from "../../../models/itemfamily";

export function useCreateItemFamily(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: createItemFamily, isPending } = useMutation({
    mutationFn: (itemFamily: ItemFamily) => postItemFamily(itemFamily),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["itemFamilies"] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["itemFamilies"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Item creation failed");
    },
  });

  return {
    createItemFamily,
    isPending,
  };
}
