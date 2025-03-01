import { useQuery } from "@tanstack/react-query";
import { getImmaterialResourceBlueprints } from "../../../services/apiImmaterialResourceBlueprints";

export function useImmaterialResourceBlueprints() {
  const {
    isLoading,
    data: immaterialResourceBlueprints,
    error,
  } = useQuery({
    queryKey: ["immaterialResourceBlueprints"],
    queryFn: getImmaterialResourceBlueprints,
  });

  return { isLoading, immaterialResourceBlueprints, error };
}
