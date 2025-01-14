import { useQuery } from "@tanstack/react-query";
import { getIsGM } from "../../../services/apiEncounter";

export function useIsGm(encounterId: number) {
  const {
    isLoading,
    data: isGM,
    isError,
    error,
  } = useQuery({
    queryKey: ["isGM", encounterId],
    queryFn: () => {
      if (encounterId) {
        return getIsGM(encounterId);
      }
      return Promise.reject(new Error("Encounter ID is undefined"));
    },
    retry: false,
    enabled: !!encounterId,
  });

  return { isLoading, isGM, error, isError };
}
