import { useQuery } from "@tanstack/react-query";
import { getWeaponAttackData } from "../../../services/apiEncounter";

export function useGetWeaponAttackData(
  encounterId: number,
  characterId: number,
  targetId: number,
  weaponId: number,
  isRanged: boolean
) {
  const {
    isLoading,
    data: weaponAttackData,
    error,
  } = useQuery({
    queryKey: ["weaponAttackData", encounterId, characterId, weaponId],
    queryFn: () => {
      if (!!encounterId && !!characterId && !!weaponId) {
        return getWeaponAttackData(
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

  return { isLoading, weaponAttackData, error };
}
