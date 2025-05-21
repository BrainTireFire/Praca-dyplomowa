import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { MaterialComponent } from "../models/power";
import { addMaterialComponent } from "../../../services/apiPowers";

export function useCreateMaterialComponent(
  onSuccess: () => void,
  powerId: number
) {
  const queryClient = useQueryClient();
  const { mutate: createMaterialComponent, isPending } = useMutation({
    mutationFn: (materialComponent: MaterialComponent) =>
      addMaterialComponent(materialComponent, powerId),
    onSuccess: () => {
      console.log("Create: " + powerId);
      queryClient.invalidateQueries({ queryKey: ["power", powerId] });
      // Explicitly refetch the query after invalidation
      queryClient.refetchQueries({ queryKey: ["power", powerId] });
      toast.success("Material component added");
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Material component creation failed");
    },
  });

  return {
    createMaterialComponent,
    isPending,
  };
}
