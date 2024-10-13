import { useQuery } from "@tanstack/react-query";
import { getCharacterNextClassLevels } from "../../../services/apiCharacters";

export function useCharacterNextClassLevels(characterId: number | null) {
  const {
    isLoading,
    data: nextLevels,
    isError,
    error,
  } = useQuery({
    queryKey: ["nextLevels", characterId],
    queryFn: () => {
      if (characterId) {
        return getCharacterNextClassLevels(characterId);
      }
      return Promise.reject(new Error("Character ID is undefined"));
    },
    retry: false,
    enabled: !!characterId, // Only run query if characterId is defined
  });

  return { isLoading, nextLevels, error, isError };
}
