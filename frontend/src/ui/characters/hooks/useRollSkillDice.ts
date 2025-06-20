import { useMutation } from "@tanstack/react-query";
import toast from "react-hot-toast";
import {
  RollDto,
  rollSkillDice as rollSkillDiceApi,
} from "../../../services/apiCharacters";
import { skill } from "../../../features/effects/skills";

export function useRollSkillDice(
  onSuccess: (roll: RollDto) => void,
  characterId: number,
  skill: skill
) {
  const {
    mutate: rollSkillDice,
    isPending,
    data: rollOutcome,
  } = useMutation({
    mutationFn: () => rollSkillDiceApi(characterId, skill),
    onSuccess: (outcome) => {
      onSuccess(outcome);
    },
    onError: (error) => {
      toast.error("Skill roll failed.");
    },
  });

  return {
    rollSkillDice,
    isPending,
    rollOutcome,
  };
}
