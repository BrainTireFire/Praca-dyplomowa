import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { EffectBlueprint } from "../../effects/EffectBlueprintForm";
import { addEffectInstance } from "../../../services/apiItems";

export function useCreateEffectInstance(onSuccess: () => void, itemId: number) {
  const queryClient = useQueryClient();
  const { mutate: createEffectInstance, isPending } = useMutation({
    mutationFn: (effectBlueprint: EffectBlueprint) =>
      addEffectInstance(effectBlueprint, itemId),
    onSuccess: () => {
      console.log("Create: " + itemId);
      queryClient.invalidateQueries({ queryKey: ["item", itemId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["item", itemId] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Effect blueprint creation failed");
    },
  });

  return {
    createEffectInstance,
    isPending,
  };
}