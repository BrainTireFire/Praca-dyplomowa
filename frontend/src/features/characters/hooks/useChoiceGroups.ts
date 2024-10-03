import { useQuery } from "@tanstack/react-query";
import { getCharactersChoiceGroups } from "../../../services/apiCharacters";

export function useChoiceGroups(characterId: number | null) {
  const {
    isLoading,
    data: choiceGroups,
    isError,
    error,
  } = useQuery({
    queryKey: ["choiceGroups", characterId],
    queryFn: () => {
      if (characterId) {
        return getCharactersChoiceGroups(characterId);
      }
      return Promise.reject(new Error("Character ID is undefined"));
    },
    retry: false,
    enabled: !!characterId, // Only run query if characterId is defined
  });

  return { isLoading, choiceGroups, error, isError };
}
