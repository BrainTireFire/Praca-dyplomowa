import { useQuery } from "@tanstack/react-query";
import { getRaces } from "../../../services/apiRaces";

export function useRaces() {
  const {
    isLoading,
    data: races,
    error,
  } = useQuery({
    queryKey: ["races"],
    queryFn: getRaces,
  });

  return { isLoading, races, error };
}
