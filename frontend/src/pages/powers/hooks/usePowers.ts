import { useQuery } from "@tanstack/react-query";
import { getPowers } from "../../../services/apiPowers";

export function usePowers() {
  const {
    isLoading,
    data: powers,
    error,
  } = useQuery({
    queryKey: ["powerList"],
    queryFn: getPowers,
  });

  return { isLoading, powers, error };
}
