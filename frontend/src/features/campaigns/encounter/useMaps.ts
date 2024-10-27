import { useQuery } from "@tanstack/react-query";
import { getBoardsShort } from "../,./../../../services/apiBoard";

export function useMaps() {
  const {
    isLoading,
    data: maps,
    error,
  } = useQuery({
    queryKey: ["maps"],
    queryFn: getBoardsShort,
  });

  return { isLoading, maps, error };
}
