import { useQuery } from "@tanstack/react-query";
import { getCharacterPowersPreparedForClass } from "../../../services/apiCharacters";

export function useCharactersPowersPreparedForClass(
  characterId: number | null,
  classId: number | null
) {
  const {
    isLoading,
    data: powers,
    error,
  } = useQuery({
    queryKey: ["characterPreparedPowerList"],
    queryFn: () => {
      if (characterId) {
        if (classId) {
          return getCharacterPowersPreparedForClass(characterId, classId);
        }
        return Promise.reject(new Error("Class ID is undefined"));
      }
      return Promise.reject(new Error("Character ID is undefined"));
    },
  });

  return { isLoading, powers, error };
}
