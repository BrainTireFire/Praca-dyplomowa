import { useQuery } from "@tanstack/react-query";
import { getPowers } from "../../../services/apiCharacters";

export function useGetPowers(characterId: number) {
  const {
    isLoading,
    data: powers,
    error,
    isError,
  } = useQuery({
    queryKey: ["powers", characterId],
    queryFn: () => getPowers(characterId),
  });

  return { isLoading, powers, error, isError };
}
