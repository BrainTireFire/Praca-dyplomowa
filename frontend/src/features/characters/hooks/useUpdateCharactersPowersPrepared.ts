import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateCharacterPreparedPowers } from "../../../services/apiCharacters";
import { Slot } from "../../../models/slot";

export function useUpdateCharactersPowersPrepared(
  onSuccess: () => void,
  characterId: number,
  classId: number
) {
  const queryClient = useQueryClient();
  const { mutate: updateCharactersChosenPowersForClass, isPending } =
    useMutation({
      mutationFn: (slots: Slot[]) =>
        updateCharacterPreparedPowers(characterId, classId, slots),
      onSuccess: () => {
        queryClient.invalidateQueries({
          queryKey: ["characterPreparedPowerList"],
        });
        queryClient.refetchQueries({
          queryKey: ["characterPreparedPowerList"],
        });
        queryClient.invalidateQueries({ queryKey: ["character", characterId] });
        toast.success("Prepared powers saved.");
        onSuccess();
      },
      onError: (error) => {
        console.error(error);
        toast.error("Power preparation failed.");
      },
    });

  return {
    updateCharactersChosenPowersForClass,
    isPending,
  };
}
