import { useQuery } from "@tanstack/react-query";
import { getBoards } from "../,./../../../services/apiBoard";

export function useBoards() {
  const {
    isLoading,
    data: boards,
    error,
  } = useQuery({
    queryKey: ["boards"],
    queryFn: getBoards,
  });

  return { isLoading, boards, error };
}
