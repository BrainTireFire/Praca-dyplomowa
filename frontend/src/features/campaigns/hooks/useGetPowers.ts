import { useQuery } from "@tanstack/react-query";
import { getPowers } from "../../../services/apiEncounter";

export function useGetPowers(characterId: number, encounterId: number) {
  const {
    isLoading,
    data: powers,
    error,
    isError,
  } = useQuery({
    queryKey: ["powers", characterId],
    queryFn: () => getPowers(characterId, encounterId),
  });

  return { isLoading, powers, error, isError };
}
