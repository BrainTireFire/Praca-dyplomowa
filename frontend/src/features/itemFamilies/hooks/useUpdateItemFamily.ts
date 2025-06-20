import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { ItemFamily } from "../../../models/itemfamily";
import { updateItemFamily as updateItemFamilyApi } from "../../../services/apiItemFamilies";

export function useUpdateItemFamily(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: updateItemFamily, isPending } = useMutation({
    mutationFn: (itemFamily: ItemFamily) => updateItemFamilyApi(itemFamily),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["itemFamilies"] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["itemFamilies"] });
      toast.success("Item family updated");
      onSuccess();
    },
    onError: (error) => {
      toast.error(error.message);
    },
  });

  return {
    updateItemFamily,
    isPending,
  };
}
