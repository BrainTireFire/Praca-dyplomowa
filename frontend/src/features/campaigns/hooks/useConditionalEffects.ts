import { useQuery } from "@tanstack/react-query";
import { getConditionalEffects } from "../../../services/apiEncounter";

export function useConditionalEffects(
  encounterId: number,
  characterId: number,
  targetId: number
) {
  const {
    isLoading,
    data: conditionalEffects,
    error,
  } = useQuery({
    queryKey: ["conditionalEffects", encounterId, characterId, targetId],
    queryFn: () => {
      if (!!encounterId && !!characterId && !!targetId) {
        return getConditionalEffects(encounterId, characterId, targetId);
      }
      return Promise.reject(
        new Error(
          "Encounter or character or target or weapon ID is not defined"
        )
      );
    },
    retry: false,
    enabled: !!encounterId && !!characterId && !!targetId, // Only run query if encounter and character ID is defined
  });

  return { isLoading, conditionalEffects, error };
}
