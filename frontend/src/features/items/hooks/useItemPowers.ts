import { useQuery } from "@tanstack/react-query";
import { getItemPowers } from "../../../services/apiItems";

export function useItemPowers(itemId: number | null) {
  const {
    isLoading,
    data: powers,
    error,
  } = useQuery({
    queryKey: ["itemPowerList"],
    queryFn: () => {
      if (itemId) {
        console.log("Load: " + itemId);
        return getItemPowers(itemId);
      }
      return Promise.reject(new Error("Item ID is undefined"));
    },
  });

  return { isLoading, powers, error };
}
