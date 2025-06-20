import { useQuery } from "@tanstack/react-query";
import { getItemResources } from "../services/apiItems";
import { getCharacterResources } from "../services/apiCharacters";

export function useObjectResources(
  objectId: number | null,
  objectType: "Item" | "Character"
) {
  const {
    isLoading,
    data: resources,
    error,
  } = useQuery({
    queryKey:
      objectType === "Item" ? ["itemResourceList"] : ["characterResourceList"],
    queryFn: () => {
      if (objectId) {
        return objectType === "Item"
          ? getItemResources(objectId)
          : getCharacterResources(objectId);
      }
      return Promise.reject(new Error("Item ID is undefined"));
    },
  });

  return { isLoading, resources, error };
}
