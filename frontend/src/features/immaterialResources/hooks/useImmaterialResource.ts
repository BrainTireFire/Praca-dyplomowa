import { useQuery } from "@tanstack/react-query";
import { getImmaterialResourceBlueprint } from "../../../services/apiImmaterialResourceBlueprints";

export function useImmaterialResource(immaterialResourceId: number | null) {
  const {
    isLoading,
    data: immaterialResource,
    error,
  } = useQuery({
    queryKey: ["immaterialResource", immaterialResourceId],
    queryFn: () => {
      if (immaterialResourceId) {
        return getImmaterialResourceBlueprint(immaterialResourceId);
      }
      return Promise.reject(new Error("Immaterial Resource ID is undefined"));
    },
    retry: false,
    enabled: !!immaterialResourceId, // Only run query if itemFamilyId is defined
  });

  return { isLoading, immaterialResource, error };
}
