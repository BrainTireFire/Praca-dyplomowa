import { useQuery } from "@tanstack/react-query";
import { getAttacks } from "../../../services/apiEncounter";

export function useGetWeaponAttacks(characterId: number, encounterId: number) {
  const {
    isLoading,
    data: weaponAttacks,
    error,
    isError,
  } = useQuery({
    queryKey: ["weaponAttacks", characterId],
    queryFn: () => getAttacks(characterId, encounterId),
  });

  return { isLoading, weaponAttacks, error, isError };
}
