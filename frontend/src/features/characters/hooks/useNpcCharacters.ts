import { useQuery } from "@tanstack/react-query";
import { getNpcCharacters } from "../../../services/apiCharacters";

export function useNpcCharacters() {
  const {
    isLoading,
    data: npcCharacters,
    error,
  } = useQuery({
    queryKey: ["npcCharacters"],
    queryFn: getNpcCharacters,
  });

  return { isLoading, npcCharacters, error };
}
