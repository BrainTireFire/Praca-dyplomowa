import { useQuery } from "@tanstack/react-query";
import { getImmaterialResourceBlueprints } from "../../../services/apiPowers";

export function useImmaterialResourceBlueprints() {
  const {
    isLoading,
    data: immaterialResourceBlueprints,
    error,
  } = useQuery({
    queryKey: ["immaterialResourceBlueprint"],
    queryFn: getImmaterialResourceBlueprints,
  });

  return { isLoading, immaterialResourceBlueprints, error };
}
