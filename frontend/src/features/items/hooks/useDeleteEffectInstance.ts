import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteEffectBlueprint as deleteEffectBlueprintApi } from "../../../services/apiEffectBlueprints";

export function useDeleteEffectInstance(onSuccess: () => void, itemId: number) {
  const queryClient = useQueryClient();
  const { mutate: deleteEffectInstance, isPending } = useMutation({
    mutationFn: (id: number) => deleteEffectBlueprintApi(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["item", itemId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["item", itemId] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Effect blueprint deletion failed");
    },
  });

  return {
    deleteEffectInstance,
    isPending,
  };
}
