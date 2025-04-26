import { useMutation } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { RollDto, rollSavingThrowDice as rollSavingThrowDiceApi } from "../../../services/apiCharacters";
import { ability } from "../../../features/effects/abilities";

export function useRollSavingThrowDice(
  onSuccess: (roll: RollDto) => void,
  characterId: number,
  ability: ability,
) {
  const { mutate: rollSavingThrowDice, isPending, data: rollOutcome } = useMutation({
    mutationFn: () => rollSavingThrowDiceApi(characterId, ability),
    onSuccess: (outcome) => {
      onSuccess(outcome);
    },
    onError: (error) => {
      console.error(error);
      toast.error("Saving throw roll failed.");
    },
  });

  return {
    rollSavingThrowDice,
    isPending,
    rollOutcome
  };
}
