import { useQuery } from "@tanstack/react-query";
import { getItemFamilies } from "../../../services/apiItemFamilies";

export function useItemFamilies() {
  const {
    isLoading,
    data: itemFamilies,
    error,
  } = useQuery({
    queryKey: ["itemFamilies"],
    queryFn: getItemFamilies,
  });

  return { isLoading, itemFamilies, error };
}
