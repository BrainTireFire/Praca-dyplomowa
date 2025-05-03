import { useQuery } from "@tanstack/react-query";
import { getSavingThrowRollConditionalEffects } from "../../../services/apiCharacters";
import { ability } from "../../effects/abilities";
// import { useParams } from "react-router-dom";

export function useGetCharactersSavingThrowConditionalEffects(characterId: number, ability: ability) {

  const {
    isLoading,
    data: conditionalEffects,
    isError,
    error,
  } = useQuery({
    queryKey: ["characterSavingThrowConditionalEffects", characterId, ability],
    queryFn: () => {
      if (characterId && ability) {
        return getSavingThrowRollConditionalEffects(characterId, ability);
      }
      return Promise.reject(new Error("Character ID or ability is undefined"));
    },
    retry: false,
    enabled: !!characterId && !!ability, // Only run query if characterId is defined
  });

  return { isLoading, conditionalEffects, error, isError };
}
