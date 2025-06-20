import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { selectSkillRollConditionalEffects as selectSkillRollConditionalEffectsApi } from "../../../services/apiCharacters";
import { ConditionalEffectSelection } from "../rollResolution/RollConditionalEffectsReducer";
import { skill } from "../../effects/skills";

export function useSelectCharactersSkillConditionalEffects(
  onSuccess: () => void,
  characterId: number,
  skill: skill
) {
  const queryClient = useQueryClient();
  const { mutate: selectSkillRollConditionalEffects, isPending } = useMutation({
    mutationFn: ({
      conditionalEffects,
    }: {
      conditionalEffects: ConditionalEffectSelection[];
    }) =>
      selectSkillRollConditionalEffectsApi(
        characterId,
        skill,
        conditionalEffects.filter((e) => e.selected).map((e) => e.effectId)
      ),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["characterSkillConditionalEffects", characterId, skill],
      });
      onSuccess();
    },
    onError: (error) => {
      toast.error("Skill roll failed!");
    },
  });

  return {
    selectSkillRollConditionalEffects,
    isPending,
  };
}
