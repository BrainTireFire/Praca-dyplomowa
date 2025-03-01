import { useQuery } from "@tanstack/react-query";
import { getImmaterialResourceBlueprints } from "../../../services/apiPowers";

export function useImmaterialResourceBlueprints(powerId: number | null) {
  const {
    isLoading,
    data: immaterialResourceBlueprints,
    error,
  } = useQuery({
    queryKey: ["immaterialResourceBlueprint", powerId],
    queryFn: () => getImmaterialResourceBlueprints(powerId),
  });

  return { isLoading, immaterialResourceBlueprints, error };
}
