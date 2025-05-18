import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteImmaterialResourceBlueprint as deleteImmaterialResourceBlueprintApi } from "../../../services/apiImmaterialResourceBlueprints";

export function useDeleteImmaterialResource(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: deleteImmaterialResourceBlueprint, isPending } = useMutation({
    mutationFn: (immaterialResourceBlueprintId: number) =>
      deleteImmaterialResourceBlueprintApi(immaterialResourceBlueprintId),
    onSuccess: () => {
      console.log('success');
      queryClient.invalidateQueries({
        queryKey: ["immaterialResourceBlueprints"],
      });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({
        queryKey: ["immaterialResourceBlueprints"],
      });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error(error.message);
    },
  });

  return {
    deleteImmaterialResourceBlueprint,
    isPending,
  };
}
