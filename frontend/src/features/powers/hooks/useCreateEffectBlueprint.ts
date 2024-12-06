import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { EffectBlueprint } from "../../effects/EffectBlueprintForm";
import { addEffectBlueprint } from "../../../services/apiPowers";

export function useCreateEffectBlueprint(
  onSuccess: () => void,
  powerId: number
) {
  const queryClient = useQueryClient();
  const { mutate: createEffectBlueprint, isPending } = useMutation({
    mutationFn: (effectBlueprint: EffectBlueprint) =>
      addEffectBlueprint(effectBlueprint, powerId),
    onSuccess: () => {
      console.log("Create: " + powerId);
      queryClient.invalidateQueries({ queryKey: ["power", powerId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["power", powerId] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Effect blueprint creation failed");
    },
  });

  return {
    createEffectBlueprint,
    isPending,
  };
}
