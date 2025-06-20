import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateItemResources as updateItemResourcesForItem } from "../services/apiItems";
import { updateItemResources as updateItemResourcesForCharacter } from "../services/apiCharacters";
import { ImmaterialResourceAmount } from "../models/immaterialResourceAmount";

export function useUpdateObjectResources(
  onSuccess: () => void,
  objectId: number,
  objectType: "Item" | "Character"
) {
  const queryClient = useQueryClient();
  const { mutate: updateObjectResources, isPending } = useMutation({
    mutationFn: (resources: ImmaterialResourceAmount[]) =>
      objectType === "Item"
        ? updateItemResourcesForItem(objectId, resources)
        : updateItemResourcesForCharacter(objectId, resources),
    onSuccess: () => {
      if (objectType === "Item") {
        queryClient.invalidateQueries({ queryKey: ["itemResourceList"] });
        queryClient.refetchQueries({ queryKey: ["itemResourceList"] });
        queryClient.invalidateQueries({ queryKey: ["item", objectId] });
      }
      if (objectType === "Character") {
        queryClient.invalidateQueries({ queryKey: ["characterResourceList"] });
        queryClient.refetchQueries({ queryKey: ["characterResourceList"] });
        queryClient.invalidateQueries({ queryKey: ["character", objectId] });
      }
      toast.success("Resource selection saved.");
      onSuccess();
    },
    onError: (error) => {
      toast.error("Resource selection failed.");
    },
  });

  return {
    updateObjectResources,
    isPending,
  };
}
