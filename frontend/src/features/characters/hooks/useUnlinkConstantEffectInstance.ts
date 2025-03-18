import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { unlinkEffectInstance as unlinkEffectInstanceApi } from "../../../services/apiEffectInstances";

export function useUnlinkConstantEffectInstance(
  onSuccess: () => void,
  characterId: number
) {
  const queryClient = useQueryClient();
  const { mutate: unlinkEffectInstance, isPending } = useMutation({
    mutationFn: (id: number) => unlinkEffectInstanceApi(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["character", characterId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["character", characterId] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Effect instance unlinking from character failed");
    },
  });

  return {
    unlinkEffectInstance,
    isPending,
  };
}
