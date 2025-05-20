import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateItem as updateItemApi } from "../../../services/apiItems";
import { Item } from "../models/item";

export function useUpdateItem(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: updateItem, isPending } = useMutation({
    mutationFn: (item: Item) => updateItemApi(item),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["itemList"] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["itemList"] });
      toast.success('Item updated');
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error(error.message);
    },
  });

  return {
    updateItem,
    isPending,
  };
}
