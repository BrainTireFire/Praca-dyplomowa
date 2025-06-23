import { useQuery } from "@tanstack/react-query";
import { getPowers } from "../../../services/apiEncounter";

export function useGetPowers(characterId: number, encounterId: number) {
  const {
    isLoading,
    isFetching,
    data: powers,
    error,
    isError,
  } = useQuery({
    queryKey: ["powers", characterId],
    queryFn: () => getPowers(characterId, encounterId),
  });

  return { isLoading: isLoading || isFetching, powers, error, isError };
}
