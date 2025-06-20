import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { EffectBlueprint } from "../../effects/EffectBlueprintForm";
import { addConstantEffectInstance } from "../../../services/apiCharacters";

export function useCreateConstantEffectInstance(
  onSuccess: () => void,
  characterId: number
) {
  const queryClient = useQueryClient();
  const { mutate: createConstantEffectInstance, isPending } = useMutation({
    mutationFn: (effectInstance: EffectBlueprint) =>
      addConstantEffectInstance(effectInstance, characterId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["character", characterId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["character", characterId] });
      onSuccess();
    },
    onError: (error) => {
      toast.error("Effect instance creation failed");
    },
  });

  return {
    createConstantEffectInstance,
    isPending,
  };
}
