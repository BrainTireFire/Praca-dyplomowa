import { useQuery } from "@tanstack/react-query";
import { getNpcCharacters } from "../../../services/apiCharacters";

export function useNpcs() {
  const {
    isLoading,
    data: npcs,
    error,
  } = useQuery({
    queryKey: ["npcs"],
    queryFn: getNpcCharacters,
  });

  return { isLoading, npcs, error };
}
