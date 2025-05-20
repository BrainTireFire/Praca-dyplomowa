import { useQuery } from "@tanstack/react-query";
import { getPowers } from "../../../services/apiPowers";

export function usePowers(params?: Record<string, string | number | boolean>) {
  const {
    isLoading,
    data: powers,
    error,
  } = useQuery({
    queryKey: ["powerList", params],
    queryFn: () => getPowers(params),
  });

  return { isLoading, powers, error };
}
