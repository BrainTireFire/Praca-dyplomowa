import { useQuery } from "@tanstack/react-query";
import { getEncounter } from "../../../services/apiEncounter";

export function useEncounter(encounterId: number) {
  const {
    isLoading,
    data: encounter,
    isError,
    error,
  } = useQuery({
    queryKey: ["encounter", encounterId],
    queryFn: () => {
      if (encounterId) {
        return getEncounter(encounterId);
      }
      return Promise.reject(new Error("Encounter ID is undefined"));
    },
    retry: false,
    enabled: !!encounterId,
  });

  return { isLoading, encounter, error, isError };
}
