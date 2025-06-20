import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteMaterialComponent as deleteMaterialComponentApi } from "../../../services/apiPowers";

export function useDeleteMaterialComponent(
  onSuccess: () => void,
  powerId: number
) {
  const queryClient = useQueryClient();
  const { mutate: deleteMaterialComponent, isPending } = useMutation({
    mutationFn: (id: number) => deleteMaterialComponentApi(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["power", powerId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["power", powerId] });
      onSuccess();
    },
    onError: (error) => {
      toast.error("Material component deletion failed");
    },
  });

  return {
    deleteMaterialComponent,
    isPending,
  };
}
