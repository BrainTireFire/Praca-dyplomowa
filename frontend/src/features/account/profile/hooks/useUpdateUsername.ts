import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { UpdateUserDto } from "../../../../models/account/updateUserDto";
import { updateUsername as updateUsernameApi } from "../../../../services/apiUser";

export function useUpdateUsername() {
  const queryClient = useQueryClient();

  const { mutate: updateUsername, isPending: isUpdating } = useMutation({
    mutationFn: (updateUserDto: UpdateUserDto) =>
      updateUsernameApi(updateUserDto),
    onSuccess: () => {
      toast.success("Username updated successfully.");
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
    onError: (error) => {
      toast.error("Error updating username. Please try again.");
    },
  });

  return {
    updateUsername,
    isUpdating,
  };
}
