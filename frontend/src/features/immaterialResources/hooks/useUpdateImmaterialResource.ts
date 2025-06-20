import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateImmaterialResourceBlueprint as updateImmaterialResourceBlueprintApi } from "../../../services/apiImmaterialResourceBlueprints";
import { ImmaterialResourceBlueprint } from "../../../models/immaterialResourceBlueprint";

export function useUpdateImmaterialResource(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: updateImmaterialResourceBlueprint, isPending } = useMutation({
    mutationFn: (immaterialResourceBlueprint: ImmaterialResourceBlueprint) =>
      updateImmaterialResourceBlueprintApi(immaterialResourceBlueprint),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["immaterialResourceBlueprints"],
      });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({
        queryKey: ["immaterialResourceBlueprints"],
      });
      toast.success("Immaterial resource updated");
      onSuccess();
    },
    onError: (error) => {
      toast.error(error.message);
    },
  });

  return {
    updateImmaterialResourceBlueprint,
    isPending,
  };
}
