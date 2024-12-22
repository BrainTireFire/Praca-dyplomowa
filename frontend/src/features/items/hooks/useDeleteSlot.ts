import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { removeSlot as removeSlotApi } from "../../../services/apiItems";

export function useDeleteSlot(onSuccess: () => void, itemId: number) {
  const queryClient = useQueryClient();
  const { mutate: removeSlot, isPending } = useMutation({
    mutationFn: (slotId: number) => removeSlotApi(slotId, itemId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["slotList"] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["slotList"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Item creation failed");
    },
  });

  return {
    removeSlot,
    isPending,
  };
}
