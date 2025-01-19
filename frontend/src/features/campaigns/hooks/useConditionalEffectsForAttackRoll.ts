import { useQuery } from "@tanstack/react-query";
import { getConditionalEffectsForAttackRoll } from "../../../services/apiEncounter";

export function useConditionalEffectsForAttackRoll(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean
) {
  const {
    isLoading,
    data: conditionalEffects,
    error,
  } = useQuery({
    queryKey: [
      "conditionalEffects",
      encounterId,
      characterId,
      targetId,
      weaponId,
      isRanged,
    ],
    queryFn: () => {
      if (!!encounterId && !!characterId && !!targetId && !!weaponId) {
        return getConditionalEffectsForAttackRoll(
          encounterId,
          characterId,
          targetId,
          weaponId,
          isRanged
        );
      }
      return Promise.reject(
        new Error(
          "Encounter or character or target or weapon ID is not defined"
        )
      );
    },
    retry: false,
    enabled: !!encounterId && !!characterId && !!targetId && !!weaponId, // Only run query if encounter and character ID is defined
  });

  return { isLoading, conditionalEffects, error };
}
