import { useQuery } from "@tanstack/react-query";
import { getItems } from "../../../services/apiItems";

export function useItems(blueprintOrInstance: "blueprint" | "instance" | null) {
  const {
    isLoading,
    data: items,
    error,
  } = useQuery({
    queryKey: ["itemList"],
    queryFn: () => getItems(blueprintOrInstance),
  });

  return { isLoading, items, error };
}
