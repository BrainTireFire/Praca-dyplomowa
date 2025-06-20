import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { selectAbilityRollConditionalEffects as selectAbilityRollConditionalEffectsApi } from "../../../services/apiCharacters";
import { ability } from "../../effects/abilities";
import { ConditionalEffectSelection } from "../rollResolution/RollConditionalEffectsReducer";

export function useSelectCharactersAbilityConditionalEffects(
  onSuccess: () => void,
  characterId: number,
  ability: ability
) {
  const queryClient = useQueryClient();
  const { mutate: selectAbilityRollConditionalEffects, isPending } =
    useMutation({
      mutationFn: ({
        conditionalEffects,
      }: {
        conditionalEffects: ConditionalEffectSelection[];
      }) =>
        selectAbilityRollConditionalEffectsApi(
          characterId,
          ability,
          conditionalEffects.filter((e) => e.selected).map((e) => e.effectId)
        ),
      onSuccess: () => {
        queryClient.invalidateQueries({
          queryKey: [
            "characterAbilityConditionalEffects",
            characterId,
            ability,
          ],
        });
        onSuccess();
      },
      onError: (error) => {
        toast.error("Ability roll failed!");
      },
    });

  return {
    selectAbilityRollConditionalEffects,
    isPending,
  };
}
