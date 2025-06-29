import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteImmaterialResourceBlueprint as deleteImmaterialResourceBlueprintApi } from "../../../services/apiImmaterialResourceBlueprints";

export function useDeleteImmaterialResource(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: deleteImmaterialResourceBlueprint, isPending } = useMutation({
    mutationFn: (immaterialResourceBlueprintId: number) =>
      deleteImmaterialResourceBlueprintApi(immaterialResourceBlueprintId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["immaterialResourceBlueprints"],
      });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({
        queryKey: ["immaterialResourceBlueprints"],
      });
      toast.success("Immaterial resource deleted");
      onSuccess();
    },
    onError: (error) => {
      toast.error(error.message);
    },
  });

  return {
    deleteImmaterialResourceBlueprint,
    isPending,
  };
}
