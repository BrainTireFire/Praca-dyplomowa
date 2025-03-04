import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { updateBoard as updateBoardApi } from "../,./../../../services/apiBoard";
import { useNavigate, useParams } from "react-router-dom";
import { BoardUpdateDto } from "../../../models/map/BoardUpdate";

export function useUpdateBoard() {
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const { boardId } = useParams<{ boardId: string }>();
  const numberboardId = Number(boardId);

  const { mutate: updateBoard, isPending: isUpdating } = useMutation({
    mutationFn: (updateData: BoardUpdateDto) =>
      updateBoardApi(numberboardId, updateData),
    onSuccess: (board) => {
      toast.success("Board updated successfully.");
      queryClient.invalidateQueries({
        queryKey: ["board", boardId],
      });
      navigate("/homebrew/map", { replace: true });
    },
    onError: (error) => {
      toast.error("Error updating board. Please try again.");
    },
  });

  return {
    updateBoard,
    isUpdating,
  };
}
