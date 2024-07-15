import { useQuery } from "@tanstack/react-query";
import { getCharacter } from "../../services/apiCharacters";
// import { useParams } from "react-router-dom";

export function useCharacter(characterId: number | null) {
  // const { characterId } = useParams<{ characterId: string }>();

  const {
    isLoading,
    data: character,
    isError,
    error,
  } = useQuery({
    queryKey: ["character", characterId],
    queryFn: () => {
      if (characterId) {
        return getCharacter(characterId);
      }
      return Promise.reject(new Error("Character ID is undefined"));
    },
    retry: false,
    enabled: !!characterId, // Only run query if characterId is defined
  });

  return { isLoading, character, error, isError };
}
