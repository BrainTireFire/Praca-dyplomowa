import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { createBoard as createBoardApi } from "../,./../../../services/apiBoard";
import { useNavigate } from "react-router-dom";

export function useCreateBoard() {
  const navigate = useNavigate();

  const { mutate: createBoard, isLoading } = useMutation({
    mutationFn: createBoardApi,
    onSuccess: (board) => {
      toast.success("Board created successfully.");
      navigate("/homebrew/map", { replace: true });
    },
    onError: (error) => {
      toast.error("Error creating board. Please try again.");
    },
  });

  return {
    createBoard,
    isLoading,
  };
}
