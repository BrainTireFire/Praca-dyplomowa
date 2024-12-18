import { useQuery } from "@tanstack/react-query";
import { getSlots } from "../../../services/apiSlots";

export function useSlots() {
  const {
    isLoading,
    data: slots,
    error,
  } = useQuery({
    queryKey: ["allSlotList"],
    queryFn: getSlots,
  });

  return { isLoading, slots, error };
}
