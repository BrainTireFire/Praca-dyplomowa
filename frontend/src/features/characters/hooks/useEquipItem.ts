import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { equipItemInSlot as equipItemInSlotApi } from "../../../services/apiCharacters";

export function useEquipItem(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: equipItemInSlot, isPending } = useMutation({
    mutationFn: ({
      characterId,
      slotId,
      itemId,
    }: {
      characterId: number;
      slotId: number;
      itemId: number;
    }) => equipItemInSlotApi(characterId, itemId, slotId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["equipmentAndSlots"] });
      queryClient.invalidateQueries({ queryKey: ["character"] });
      toast.success("Item equipped");
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Item equipment failed!");
    },
  });

  return {
    equipItemInSlot,
    isPending,
  };
}
