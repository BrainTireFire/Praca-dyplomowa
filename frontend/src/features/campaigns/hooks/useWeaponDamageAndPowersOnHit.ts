import { useQuery } from "@tanstack/react-query";
import { getWeaponDamageAndPowersOnHit } from "../../../services/apiEncounter";

export function useWeaponDamageAndPowersOnHit(
  encounterId: number,
  characterId: number,
  weaponId: number
) {
  const {
    isLoading,
    data: weaponAttackData,
    error,
  } = useQuery({
    queryKey: [
      "weaponAttackDataWithPowers",
      encounterId,
      characterId,
      weaponId,
    ],
    queryFn: () => {
      if (!!encounterId && !!characterId && !!weaponId) {
        return getWeaponDamageAndPowersOnHit(
          encounterId,
          characterId,
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
    enabled: !!encounterId && !!characterId && !!weaponId, // Only run query if encounter and character ID is defined
  });

  return { isLoading, weaponAttackData, error };
}
