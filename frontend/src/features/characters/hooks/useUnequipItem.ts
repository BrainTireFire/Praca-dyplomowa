import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { unequipItemInSlot as unequipItemInSlotApi } from "../../../services/apiCharacters";

export function useUnequipItem(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: unequipItemInSlot, isPending } = useMutation({
    mutationFn: ({
      characterId,
      slotId,
      itemId,
    }: {
      characterId: number;
      slotId: number;
      itemId: number;
    }) => unequipItemInSlotApi(characterId, itemId, slotId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["equipmentAndSlots"] });
      queryClient.invalidateQueries({ queryKey: ["character"] });
      toast.success("Item unequipped");
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Item unequipment failed!");
    },
  });

  return {
    unequipItemInSlot,
    isPending,
  };
}
