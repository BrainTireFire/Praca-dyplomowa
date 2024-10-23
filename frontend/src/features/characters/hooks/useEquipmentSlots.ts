import { useQuery } from "@tanstack/react-query";
import { getEquipmentAndSlots } from "../../../services/apiCharacters";

export function useEquipmentSlots(characterId: number) {
  const {
    isLoading,
    data: equipmentAndSlots,
    isError,
    error,
  } = useQuery({
    queryKey: ["equipmentAndSlots", characterId],
    queryFn: () => {
      if (characterId) {
        return getEquipmentAndSlots(characterId);
      }
      return Promise.reject(new Error("Character ID is undefined"));
    },
    retry: false,
    enabled: !!characterId, // Only run query if characterId is defined
  });

  return { isLoading, equipmentAndSlots, error, isError };
}
