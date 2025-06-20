import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteEffectBlueprint as deleteEffectBlueprintApi } from "../../../services/apiEffectBlueprints";

export function useDeleteEffectBlueprint(
  onSuccess: () => void,
  powerId: number
) {
  const queryClient = useQueryClient();
  const { mutate: deleteEffectBlueprint, isPending } = useMutation({
    mutationFn: (id: number) => deleteEffectBlueprintApi(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["power", powerId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["power", powerId] });
      toast.success("Effect deleted");
      onSuccess();
    },
    onError: (error) => {
      toast.error("Effect blueprint deletion failed");
    },
  });

  return {
    deleteEffectBlueprint,
    isPending,
  };
}
