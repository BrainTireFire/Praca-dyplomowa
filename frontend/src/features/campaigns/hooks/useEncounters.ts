import { useQuery } from "@tanstack/react-query";
import { getEncounters } from "../../../services/apiEncounter";

export function useEncounters() {
  const {
    isLoading,
    data: encounters,
    error,
  } = useQuery({
    queryKey: ["encounters"],
    queryFn: getEncounters,
  });

  return { isLoading, encounters, error };
}
