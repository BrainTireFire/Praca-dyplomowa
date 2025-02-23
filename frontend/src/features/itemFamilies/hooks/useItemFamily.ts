import { useQuery } from "@tanstack/react-query";
import { getItemFamily } from "../../../services/apiItemFamilies";

export function useItemFamily(itemFamilyId: number | null) {
  const {
    isLoading,
    data: itemFamily,
    error,
  } = useQuery({
    queryKey: ["itemFamily", itemFamilyId],
    queryFn: () => {
      if (itemFamilyId) {
        return getItemFamily(itemFamilyId);
      }
      return Promise.reject(new Error("Item Family ID is undefined"));
    },
    retry: false,
    enabled: !!itemFamilyId, // Only run query if itemFamilyId is defined
  });

  return { isLoading, itemFamily, error };
}
