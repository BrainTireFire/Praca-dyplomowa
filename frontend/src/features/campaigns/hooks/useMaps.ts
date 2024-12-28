import { useQuery } from "@tanstack/react-query";
import { getBoards } from "../../../services/apiBoard";

export function useMaps() {
  const {
    isLoading,
    data: maps,
    error,
  } = useQuery({
    queryKey: ["maps"],
    queryFn: getBoards,
  });

  return { isLoading, maps, error };
}
