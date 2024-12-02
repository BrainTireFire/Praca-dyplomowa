import { useQuery } from "@tanstack/react-query";
import { getMaterialComponents } from "../../../services/apiPowers";

export function useMaterialComponents(powerId: number) {
  const {
    isLoading,
    data: materialComponents,
    error,
  } = useQuery({
    queryKey: ["materialResources"],
    queryFn: () => getMaterialComponents(powerId),
  });

  return { isLoading, materialComponents, error };
}
