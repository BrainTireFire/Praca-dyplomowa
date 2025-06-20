import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteEffectInstance as deleteEffectInstanceApi } from "../../../services/apiEffectInstances";

export function useDeleteConstantEffectInstance(
  onSuccess: () => void,
  itemId: number
) {
  const queryClient = useQueryClient();
  const { mutate: deleteEffectInstance, isPending } = useMutation({
    mutationFn: (id: number) => deleteEffectInstanceApi(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["character", itemId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["character", itemId] });
      onSuccess();
    },
    onError: (error) => {
      toast.error("Effect blueprint deletion failed");
    },
  });

  return {
    deleteEffectInstance,
    isPending,
  };
}
