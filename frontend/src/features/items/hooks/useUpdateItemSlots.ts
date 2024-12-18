import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateItemSlots as updateItemSlotsApi } from "../../../services/apiItems";
import { Slot } from "../../../models/slot";

export function useUpdateItemSlots(onSuccess: () => void, itemId: number) {
  const queryClient = useQueryClient();
  const { mutate: updateItemSlots, isPending } = useMutation({
    mutationFn: (slots: Slot[]) => updateItemSlotsApi(itemId, slots),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["itemSlotList"] });
      queryClient.invalidateQueries({ queryKey: ["item", itemId] });
      queryClient.refetchQueries({ queryKey: ["itemSlotList"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Item update failed");
    },
  });

  return {
    updateItemSlots,
    isPending,
  };
}
