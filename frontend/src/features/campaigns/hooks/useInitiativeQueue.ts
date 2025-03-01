import { useQuery } from "@tanstack/react-query";
import { getInitiativeQueue } from "../../../services/apiEncounter";

export function useInitiativeQueue(encounterId: number) {
  const {
    isLoading,
    data: initiativeQueue,
    isError,
    error,
  } = useQuery({
    queryKey: ["initiativeQueue", encounterId],
    queryFn: () => {
      if (encounterId) {
        return getInitiativeQueue(encounterId);
      }
      return Promise.reject(new Error("Encounter ID is undefined"));
    },
    retry: false,
    enabled: !!encounterId,
  });

  return { isLoading, initiativeQueue, error, isError };
}
