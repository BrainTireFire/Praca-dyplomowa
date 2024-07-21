import { useQuery } from "@tanstack/react-query";
import { getCharacters } from "../../../services/apiCharacters";

export function useCharacters() {
  const {
    isLoading,
    data: characters,
    error,
  } = useQuery({
    queryKey: ["characters"],
    queryFn: getCharacters,
  });

  return { isLoading, characters, error };
}
