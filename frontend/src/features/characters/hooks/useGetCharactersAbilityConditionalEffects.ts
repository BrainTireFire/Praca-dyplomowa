import { useQuery } from "@tanstack/react-query";
import { getAbilityRollConditionalEffects } from "../../../services/apiCharacters";
import { ability } from "../../effects/abilities";
// import { useParams } from "react-router-dom";

export function useGetCharactersAbilityConditionalEffects(characterId: number, ability: ability) {

  const {
    isLoading,
    data: conditionalEffects,
    isError,
    error,
  } = useQuery({
    queryKey: ["characterAbilityConditionalEffects", characterId, ability],
    queryFn: () => {
      if (characterId && ability) {
        return getAbilityRollConditionalEffects(characterId, ability);
      }
      return Promise.reject(new Error("Character ID or ability is undefined"));
    },
    retry: false,
    enabled: !!characterId && !!ability, // Only run query if characterId is defined
  });

  return { isLoading, conditionalEffects, error, isError };
}
