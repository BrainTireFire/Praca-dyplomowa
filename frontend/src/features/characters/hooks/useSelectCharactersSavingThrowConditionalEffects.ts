import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { selectSavingThrowRollConditionalEffects as selectSavingThrowRollConditionalEffectsApi } from "../../../services/apiCharacters";
import { ability } from "../../effects/abilities";
import { ConditionalEffectSelection } from "../rollResolution/RollConditionalEffectsReducer";

export function useSelectCharactersSavingThrowConditionalEffects(
  onSuccess: () => void,
  characterId: number,
  savingThrow: ability
) {
  const queryClient = useQueryClient();
  const { mutate: selectSavingThrowRollConditionalEffects, isPending } =
    useMutation({
      mutationFn: ({
        conditionalEffects,
      }: {
        conditionalEffects: ConditionalEffectSelection[];
      }) =>
        selectSavingThrowRollConditionalEffectsApi(
          characterId,
          savingThrow,
          conditionalEffects.filter((e) => e.selected).map((e) => e.effectId)
        ),
      onSuccess: () => {
        queryClient.invalidateQueries({
          queryKey: [
            "characterSavingThrowConditionalEffects",
            characterId,
            savingThrow,
          ],
        });
        onSuccess();
      },
      onError: (error) => {
        toast.error("Saving throw roll failed!");
      },
    });

  return {
    selectSavingThrowRollConditionalEffects,
    isPending,
  };
}
