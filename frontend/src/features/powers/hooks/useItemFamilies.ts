import { useQuery } from "@tanstack/react-query";
import { getItemFamilies } from "../../../services/apiPowers";

export function useItemFamilies(powerId: number | null) {
  const {
    isLoading,
    data: itemFamilies,
    error,
  } = useQuery({
    queryKey: ["itemFamilies", powerId],
    queryFn: () => getItemFamilies(powerId),
  });

  return { isLoading, itemFamilies, error };
}
