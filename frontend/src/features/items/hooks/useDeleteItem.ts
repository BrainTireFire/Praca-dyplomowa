import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteItem as deleteItemApi } from "../../../services/apiItems";

export function useDeleteItem(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: deleteItem, isPending } = useMutation({
    mutationFn: (itemId: number) => deleteItemApi(itemId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["itemList"] });
      toast.success("Item deleted");
      onSuccess();
    },
    onError: (error) => {
      toast.error(error.message);
    },
  });

  return {
    deleteItem,
    isPending,
  };
}
