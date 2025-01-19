import { useQuery } from "@tanstack/react-query";
import { getConditionalEffectsForWeaponHit } from "../../../services/apiEncounter";

export function useConditionalEffectsForWeaponHit(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number
) {
  const {
    isLoading,
    data: conditionalEffects,
    error,
  } = useQuery({
    queryKey: [
      "conditionalEffectsForWeaponHit",
      encounterId,
      characterId,
      targetId,
      weaponId,
    ],
    queryFn: () => {
      if (!!encounterId && !!characterId && !!targetId && !!weaponId) {
        return getConditionalEffectsForWeaponHit(
          encounterId,
          characterId,
          targetId,
          weaponId
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
