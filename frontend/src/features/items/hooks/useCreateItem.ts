import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { postItem } from "../../../services/apiItems";
import { Item } from "../models/item";

export function useCreateItem(onSuccess: (id: number) => void) {
  const queryClient = useQueryClient();
  const { mutate: createItem, isPending } = useMutation({
    mutationFn: (item: Item) => postItem(item),
    onSuccess: (id: number) => {
      queryClient.invalidateQueries({ queryKey: ["itemList"] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["itemList"] });
      toast.success("Item created");
      onSuccess(id);
    },
    onError: (error) => {
      toast.error("Item creation failed");
    },
  });

  return {
    createItem,
    isPending,
  };
}
