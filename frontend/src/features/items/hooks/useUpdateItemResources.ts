import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateItemResources as updateItemResourcesApi } from "../../../services/apiItems";
import { ImmaterialResourceAmount } from "../../../models/immaterialResourceAmount";

export function useUpdateItemResources(onSuccess: () => void, itemId: number) {
  const queryClient = useQueryClient();
  const { mutate: updateItemResources, isPending } = useMutation({
    mutationFn: (resources: ImmaterialResourceAmount[]) =>
      updateItemResourcesApi(itemId, resources),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["itemResourceList"] });
      queryClient.invalidateQueries({ queryKey: ["item", itemId] });
      queryClient.refetchQueries({ queryKey: ["itemResourceList"] });
      onSuccess();
    },
    onError: (error) => {
      console.error(error);
      toast.error("Item update failed");
    },
  });

  return {
    updateItemResources,
    isPending,
  };
}
