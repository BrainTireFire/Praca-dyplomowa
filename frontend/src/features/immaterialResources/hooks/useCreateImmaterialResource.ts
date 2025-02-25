import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { postImmaterialResourceBlueprint as postImmaterialResourceBlueprintApi } from "../../../services/apiImmaterialResourceBlueprints";
import { ImmaterialResourceBlueprint } from "../../../models/immaterialResourceBlueprint";

export function useCreateImmaterialResource(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: createImmaterialResourceBlueprint, isPending } = useMutation({
    mutationFn: (immaterialResourceBlueprint: ImmaterialResourceBlueprint) =>
      postImmaterialResourceBlueprintApi(immaterialResourceBlueprint),
    onSuccess: () => {
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
      toast.error("Immaterial resource creation failed");
    },
  });

  return {
    createImmaterialResourceBlueprint,
    isPending,
  };
}
