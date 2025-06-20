import { useMutation } from "@tanstack/react-query";
import toast from "react-hot-toast";
import {
  rollAbilityDice as rollAbilityDiceApi,
  RollDto,
} from "../../../services/apiCharacters";
import { ability } from "../../../features/effects/abilities";

export function useRollAbilityDice(
  onSuccess: (roll: RollDto) => void,
  characterId: number,
  ability: ability
) {
  const {
    mutate: rollAbilityDice,
    isPending,
    data: rollOutcome,
  } = useMutation({
    mutationFn: () => rollAbilityDiceApi(characterId, ability),
    onSuccess: (outcome) => {
      onSuccess(outcome);
    },
    onError: (error) => {
      toast.error("Ability roll failed.");
    },
  });

  return {
    rollAbilityDice,
    isPending,
    rollOutcome,
  };
}
