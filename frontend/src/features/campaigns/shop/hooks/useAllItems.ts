import { useQuery } from "@tanstack/react-query";
import { getAllItems } from "../../../../services/apiShops";

export function useAllItems() {
  const {
    isLoading,
    data: items,
    error,
  } = useQuery({
    queryKey: ["itemList"],
    queryFn: () => getAllItems(),
  });

  return { isLoading, items, error };
}
