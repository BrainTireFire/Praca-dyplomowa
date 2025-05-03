import { useQuery } from "@tanstack/react-query";
import { getSkillRollConditionalEffects } from "../../../services/apiCharacters";
import { skill } from "../../effects/skills";
// import { useParams } from "react-router-dom";

export function useGetCharactersSkillConditionalEffects(characterId: number, skill: skill) {

  const {
    isLoading,
    data: conditionalEffects,
    isError,
    error,
  } = useQuery({
    queryKey: ["characterSkillConditionalEffects", characterId, skill],
    queryFn: () => {
      if (characterId && skill) {
        return getSkillRollConditionalEffects(characterId, skill);
      }
      return Promise.reject(new Error("Character ID or skill is undefined"));
    },
    retry: false,
    enabled: !!characterId && !!skill, // Only run query if characterId is defined
  });

  return { isLoading, conditionalEffects, error, isError };
}
