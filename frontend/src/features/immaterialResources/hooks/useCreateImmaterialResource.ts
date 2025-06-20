import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { postImmaterialResourceBlueprint as postImmaterialResourceBlueprintApi } from "../../../services/apiImmaterialResourceBlueprints";
import { ImmaterialResourceBlueprint } from "../../../models/immaterialResourceBlueprint";

export function useCreateImmaterialResource(onSuccess: (id: number) => void) {
  const queryClient = useQueryClient();
  const { mutate: createImmaterialResourceBlueprint, isPending } = useMutation({
    mutationFn: (immaterialResourceBlueprint: ImmaterialResourceBlueprint) =>
      postImmaterialResourceBlueprintApi(immaterialResourceBlueprint),
    onSuccess: (id: number) => {
      queryClient.invalidateQueries({
        queryKey: ["immaterialResourceBlueprints"],
      });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({
        queryKey: ["immaterialResourceBlueprints"],
      });
      toast.success("Immaterial resource created");
      onSuccess(id);
    },
    onError: (error) => {
      toast.error("Immaterial resource creation failed");
    },
  });

  return {
    createImmaterialResourceBlueprint,
    isPending,
  };
}
