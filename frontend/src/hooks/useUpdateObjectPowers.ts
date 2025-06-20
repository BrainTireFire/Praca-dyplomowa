import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateItemPowers } from "../services/apiItems";
import {
  updateCharacterKnownPowers,
  updateCharacterPreparedPowers,
} from "../services/apiCharacters";
import { Slot } from "../models/slot";

export function useUpdateObjectPowers(
  onSuccess: () => void,
  objectId: number,
  objectType: "Item" | "Character"
) {
  const queryClient = useQueryClient();
  const { mutate: updateObjectPowers, isPending } = useMutation({
    mutationFn: (slots: Slot[]) =>
      objectType === "Item"
        ? updateItemPowers(objectId, slots)
        : updateCharacterKnownPowers(objectId, slots),
    onSuccess: () => {
      if (objectType === "Item") {
        queryClient.invalidateQueries({ queryKey: ["itemPowerList"] });
        queryClient.refetchQueries({ queryKey: ["itemPowerList"] });
        queryClient.invalidateQueries({ queryKey: ["item", objectId] });
      }
      if (objectType === "Character") {
        queryClient.invalidateQueries({ queryKey: ["characterPowerList"] });
        queryClient.refetchQueries({ queryKey: ["characterPowerList"] });
        queryClient.invalidateQueries({ queryKey: ["character", objectId] });
      }

      toast.success("Power selection saved succesfully");
      onSuccess();
    },
    onError: (error) => {
      toast.error("Power selection failed");
    },
  });

  return {
    updateObjectPowers,
    isPending,
  };
}
