import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { MaterialComponent } from "../models/power";
import { updateMaterialComponent as updateMaterialComponentApi } from "../../../services/apiPowers";

export function useUpdateMaterialComponent(
  onSuccess: () => void,
  powerId: number
) {
  const queryClient = useQueryClient();
  const { mutate: updateMaterialComponent, isPending } = useMutation({
    mutationFn: (materialComponent: MaterialComponent) =>
      updateMaterialComponentApi(materialComponent, powerId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["power", powerId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["power", powerId] });
      toast.success("Material component updated");
      onSuccess();
    },
    onError: (error) => {
      toast.error("Material component creation failed");
    },
  });

  return {
    updateMaterialComponent,
    isPending,
  };
}
