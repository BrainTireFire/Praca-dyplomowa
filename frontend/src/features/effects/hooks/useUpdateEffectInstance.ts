import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { EffectBlueprint } from "../EffectBlueprintForm";
import { updateEffectInstance as updateEffectInstanceApi } from "../../../services/apiEffectInstances";

export function useUpdateEffectInstance(
  onSuccess: () => void,
  effectId: number,
  powerId: number
) {
  const queryClient = useQueryClient();
  const { mutate: updateEffectInstance, isPending } = useMutation({
    mutationFn: (effectBlueprint: EffectBlueprint) =>
      updateEffectInstanceApi(effectBlueprint),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["effectInstance", effectId],
      });

      queryClient.invalidateQueries({ queryKey: ["power", powerId] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Effect Instance update failed");
    },
  });

  return {
    updateEffectInstance,
    isPending,
  };
}
