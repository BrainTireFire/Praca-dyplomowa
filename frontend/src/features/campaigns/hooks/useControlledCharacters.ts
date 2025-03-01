import { useQuery } from "@tanstack/react-query";
import { getControlledCharacters } from "../../../services/apiEncounter";

export function useControlledCharacters(encounterId: number) {
  const {
    isLoading,
    data: characterIds,
    error,
  } = useQuery({
    queryKey: ["controlledCharacters"],
    queryFn: () => getControlledCharacters(encounterId),
  });

  return { isLoading, characterIds, error };
}
