import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { addSlot as addSlotApi } from "../../../services/apiItems";

export function useAddSlot(onSuccess: () => void, itemId: number) {
  const queryClient = useQueryClient();
  const { mutate: addSlot, isPending } = useMutation({
    mutationFn: (slotId: number) => addSlotApi(slotId, itemId),
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
    addSlot,
    isPending,
  };
}
