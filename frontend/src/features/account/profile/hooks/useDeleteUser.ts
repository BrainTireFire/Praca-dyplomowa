import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteUser as deleteUserAPI } from "../../../../services/apiUser";

export function useDeleteUser() {
  const queryClient = useQueryClient();

  const { mutate: deleteUser, isPending: isDeleting } = useMutation({
    mutationFn: deleteUserAPI,
    onSuccess: () => {
      toast.success("User is gone!");
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
    onError: (error) => {
      toast.error("Error deleting the user. Please try again.");
    },
  });

  return {
    deleteUser,
    isDeleting,
  };
}
