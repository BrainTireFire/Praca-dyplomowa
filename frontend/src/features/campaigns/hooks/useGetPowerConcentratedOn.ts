import { useQuery } from "@tanstack/react-query";
import { getPowerConcentratedOn } from "../../../services/apiCharacters";

export function useGetPowerConcentratedOn(characterId: number) {
  const {
    isLoading,
    data: concentrationData,
    isError,
    error,
  } = useQuery({
    queryKey: ["concentration", characterId],
    queryFn: () => {
      if (characterId) {
        return getPowerConcentratedOn(characterId);
      }
      return Promise.reject(new Error("characterId ID is undefined"));
    },
    retry: false,
    enabled: !!characterId,
  });

  return { isLoading, concentrationData, error, isError };
}
