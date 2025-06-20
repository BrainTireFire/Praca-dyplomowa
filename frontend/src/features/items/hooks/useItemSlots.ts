import { useQuery } from "@tanstack/react-query";
import { getItemSlots } from "../../../services/apiItems";

export function useItemSlots(itemId: number | null) {
  const {
    isLoading,
    data: slots,
    error,
  } = useQuery({
    queryKey: ["itemSlotList"],
    queryFn: () => {
      if (itemId) {
        return getItemSlots(itemId);
      }
      return Promise.reject(new Error("Item ID is undefined"));
    },
  });

  return { isLoading, slots, error };
}
