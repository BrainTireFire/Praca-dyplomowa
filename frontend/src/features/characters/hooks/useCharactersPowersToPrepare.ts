import { useQuery } from "@tanstack/react-query";
import { getCharacterPowersToPrepare } from "../../../services/apiCharacters";

export function useCharactersPowersToPrepare(characterId: number | null) {
  const {
    isLoading,
    isFetching,
    data: powerSelection,
    error,
  } = useQuery({
    queryKey: ["characterToPreparePowerList"],
    queryFn: () => {
      if (characterId) {
        return getCharacterPowersToPrepare(characterId);
      }
      return Promise.reject(new Error("Object ID is undefined"));
    },
  });

  return { isLoading: isLoading || isFetching, powerSelection, error };
}
