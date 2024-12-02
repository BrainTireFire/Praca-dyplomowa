import { useQuery } from "@tanstack/react-query";
import { getMaterialComponent } from "../../../services/apiPowers";

export function useMaterialResources(componentId: number) {
  const {
    isLoading,
    data: materialComponent,
    error,
  } = useQuery({
    queryKey: ["materialComponent", componentId],
    queryFn: () => getMaterialComponent(componentId),
  });

  return { isLoading, materialComponent, error };
}
