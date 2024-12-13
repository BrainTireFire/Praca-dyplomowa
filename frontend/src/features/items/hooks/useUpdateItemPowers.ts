import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateItemPowers as updateItemPowersApi } from "../../../services/apiItems";
import { Slot } from "../../../models/slot";

export function useUpdateItemPowers(onSuccess: () => void, itemId: number) {
  const queryClient = useQueryClient();
  const { mutate: updateItemPowers, isPending } = useMutation({
    mutationFn: (slots: Slot[]) => updateItemPowersApi(itemId, slots),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["itemPowerList"] });
      queryClient.invalidateQueries({ queryKey: ["item", itemId] });
      queryClient.refetchQueries({ queryKey: ["itemPowerList"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Item update failed");
    },
  });

  return {
    updateItemPowers,
    isPending,
  };
}
