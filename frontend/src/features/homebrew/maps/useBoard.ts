import { useQuery } from "@tanstack/react-query";
import { getBoard } from "../../../services/apiBoard";
import { useParams } from "react-router-dom";

export function useBoard() {
  const { boardId } = useParams<{ boardId: string }>();
  const numberboardId = Number(boardId);

  const {
    isLoading,
    data: board,
    isError,
    error,
  } = useQuery({
    queryKey: ["board", numberboardId],
    queryFn: () => {
      if (numberboardId) {
        return getBoard(Number(numberboardId));
      }
      return Promise.reject(new Error("Board ID is undefined"));
    },
    retry: false,
    enabled: !!numberboardId,
  });

  return { isLoading, board, error, isError };
}
