import { useQuery } from "@tanstack/react-query";
import { getParticipanceData } from "../../../services/apiEncounter";

export function useParticipanceData(encounterId: number, characterId: number) {
  const {
    isLoading,
    data: participance,
    error,
  } = useQuery({
    queryKey: ["participance", encounterId, characterId],
    queryFn: () => {
      if (!!encounterId && !!characterId) {
        return getParticipanceData(encounterId, characterId);
      }
      return Promise.reject(
        new Error("Encounter or character ID is not defined")
      );
    },
    retry: false,
    enabled: !!encounterId && !!characterId, // Only run query if encounter and character ID is defined
  });

  return { isLoading, participance, error };
}
