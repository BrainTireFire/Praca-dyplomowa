import { useQuery } from "@tanstack/react-query";
import { getItemResources } from "../../../services/apiItems";

export function useItemResources(itemId: number | null) {
  const {
    isLoading,
    data: resources,
    error,
  } = useQuery({
    queryKey: ["itemResourceList"],
    queryFn: () => {
      if (itemId) {
        console.log("Load: " + itemId);
        return getItemResources(itemId);
      }
      return Promise.reject(new Error("Item ID is undefined"));
    },
  });

  return { isLoading, resources, error };
}
