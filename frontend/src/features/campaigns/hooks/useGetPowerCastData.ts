import { useQuery } from "@tanstack/react-query";
import { getPowerData } from "../../../services/apiEncounter";

export function useGetPowerCastData(
  encounterId: number,
  characterId: number,
  powerId: number,
  targetIds: number[]
) {
  const {
    isLoading,
    data: powerCastData,
    error,
  } = useQuery({
    queryKey: ["powerCastData", encounterId, characterId, powerId, targetIds],
    queryFn: () => {
      if (
        !!encounterId &&
        !!characterId &&
        !!powerId &&
        !!targetIds &&
        targetIds.length > 0
      ) {
        return getPowerData(encounterId, characterId, powerId, targetIds);
      }
      return Promise.reject(
        new Error("Encounter or character or target or power ID is not defined")
      );
    },
    retry: false,
    enabled:
      !!encounterId &&
      !!characterId &&
      !!targetIds &&
      targetIds.length > 0 &&
      !!powerId, // Only run query if encounter and character ID is defined
  });

  return { isLoading, powerCastData, error };
}
