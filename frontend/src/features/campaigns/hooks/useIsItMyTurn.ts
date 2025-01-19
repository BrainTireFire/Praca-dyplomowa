import { useQuery } from "@tanstack/react-query";
import { getIsItMyTurn } from "../../../services/apiEncounter";

export function useIsItMyTurn(
  encounterId: number,
  controlledCharacterId: number
) {
  const {
    isLoading,
    data: isItMyTurn,
    isError,
    error,
  } = useQuery({
    queryKey: ["isItMyTurn", encounterId, controlledCharacterId],
    queryFn: () => {
      if (encounterId) {
        return getIsItMyTurn(encounterId, controlledCharacterId);
      }
      return Promise.reject(new Error("Encounter ID is undefined"));
    },
    retry: false,
    enabled: !!encounterId && !!controlledCharacterId,
  });

  return { isLoading, isItMyTurn, error, isError };
}
