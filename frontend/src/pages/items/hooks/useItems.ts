import { useQuery } from "@tanstack/react-query";
import { getItems } from "../../../services/apiItems";

export function useItems() {
  const {
    isLoading,
    data: items,
    error,
  } = useQuery({
    queryKey: ["itemList"],
    queryFn: getItems,
  });

  return { isLoading, items, error };
}
