import { useQuery } from "@tanstack/react-query";
import { getEncounters } from "../../../services/apiEncounter";

export function useEncounters(campaignId: string) {
  const {
    isLoading,
    data: encounters,
    error,
  } = useQuery({
    queryKey: ["encounters"],
    queryFn: () => getEncounters(campaignId),
  });

  return { isLoading, encounters, error };
}
