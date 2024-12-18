import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { EffectBlueprint } from "../EffectBlueprintForm";
import { updateEffectBlueprint as updateEffectBlueprintApi } from "../../../services/apiEffectBlueprints";

export function useUpdateEffectBlueprint(
  onSuccess: () => void,
  effectId: number,
  powerId: number
) {
  const queryClient = useQueryClient();
  const { mutate: updateEffectBlueprint, isPending } = useMutation({
    mutationFn: (effectBlueprint: EffectBlueprint) =>
      updateEffectBlueprintApi(effectBlueprint),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["effectBlueprint", effectId],
      });

      queryClient.invalidateQueries({ queryKey: ["power", powerId] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Effect Blueprint update failed");
    },
  });

  return {
    updateEffectBlueprint,
    isPending,
  };
}
