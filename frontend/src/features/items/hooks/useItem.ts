import { useQuery } from "@tanstack/react-query";
import { getItem } from "../../../services/apiItems";

export function useItem(itemId: number | null) {
  const {
    isLoading,
    data: item,
    error,
  } = useQuery({
    queryKey: ["item", itemId],
    queryFn: () => {
      if (itemId) {
        return getItem(itemId);
      }
      return Promise.reject(new Error("Power ID is undefined"));
    },
    retry: false,
    enabled: !!itemId, // Only run query if powerId is defined
  });

  return { isLoading, item, error };
}
