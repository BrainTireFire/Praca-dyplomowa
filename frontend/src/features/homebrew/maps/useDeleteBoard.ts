import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteBoard as deleteBoardApi } from "../../../services/apiBoard";
import toast from "react-hot-toast";

export function useDeleteBoard() {
  const queryClient = useQueryClient();

  const { isLoading: isDeleting, mutate: deleteBoard } = useMutation({
    mutationFn: deleteBoardApi,
    onSuccess: () => {
      toast.success("Board successfully deleted");

      queryClient.invalidateQueries({
        queryKey: ["boards"],
      });
    },
    onError: (err) => {
      toast.error(err.message);
    },
  });

  return { isDeleting, deleteBoard };
}
