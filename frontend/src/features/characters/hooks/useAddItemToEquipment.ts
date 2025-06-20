import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { addToEquipment as addToEquipmentApi } from "../../../services/apiCharacters";

export function useAddItemToEquipment(
  onSuccess: () => void,
  characterId: number
) {
  const queryClient = useQueryClient();
  const { mutate: addToEquipment, isPending } = useMutation({
    mutationFn: (itemId: number) => addToEquipmentApi(characterId, itemId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["equipmentAndSlots"] });
      queryClient.invalidateQueries({ queryKey: ["character"] });
      toast.success("Item added to character's possessions");
      onSuccess();
    },
    onError: (error) => {
      toast.error("Adding item failed!");
    },
  });

  return {
    addToEquipment,
    isPending,
  };
}
