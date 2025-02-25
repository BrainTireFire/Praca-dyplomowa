import { useQuery } from "@tanstack/react-query";
import { getImmaterialResourceBlueprints } from "../../../services/apiImmaterialResourceBlueprints";

export function useImmaterialResources() {
  const {
    isLoading,
    data: immaterialResources,
    error,
  } = useQuery({
    queryKey: ["immaterialResourceBlueprints"],
    queryFn: getImmaterialResourceBlueprints,
  });

  return { isLoading, immaterialResources, error };
}
