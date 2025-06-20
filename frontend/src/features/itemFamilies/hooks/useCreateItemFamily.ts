import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { postItemFamily } from "../../../services/apiItemFamilies";
import { ItemFamily } from "../../../models/itemfamily";

export function useCreateItemFamily(onSuccess: (id: number) => void) {
  const queryClient = useQueryClient();
  const { mutate: createItemFamily, isPending } = useMutation({
    mutationFn: (itemFamily: ItemFamily) => postItemFamily(itemFamily),
    onSuccess: (id: number) => {
      queryClient.invalidateQueries({ queryKey: ["itemFamilies"] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["itemFamilies"] });
      toast.success("Item family created");
      onSuccess(id);
    },
    onError: (error) => {
      toast.error("Item family creation failed");
    },
  });

  return {
    createItemFamily,
    isPending,
  };
}
