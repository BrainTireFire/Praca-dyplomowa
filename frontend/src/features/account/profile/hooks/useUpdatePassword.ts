import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { UpdateUserDto } from "../../../../models/account/updateUserDto";
import { updatePassword as updatePasswordApi } from "../../../../services/apiUser";

export function useUpdatePassword() {
  const queryClient = useQueryClient();

  const { mutate: updatePassword, isPending: isUpdating } = useMutation({
    mutationFn: (updateUserDto: UpdateUserDto) =>
      updatePasswordApi(updateUserDto),
    onSuccess: () => {
      toast.success("Password updated successfully.");
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
    onError: (error) => {
      toast.error("Error updating password. Please try again.");
    },
  });

  return {
    updatePassword,
    isUpdating,
  };
}
