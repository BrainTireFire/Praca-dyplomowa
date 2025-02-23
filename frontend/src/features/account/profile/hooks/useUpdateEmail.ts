import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { UpdateUserDto } from "../../../../models/account/updateUserDto";
import { updateEmail as updateEmailApi } from "../../../../services/apiUser";

export function useUpdateEmail() {
  const queryClient = useQueryClient();

  const { mutate: updateEmail, isPending: isUpdating } = useMutation({
    mutationFn: (updateUserDto: UpdateUserDto) => updateEmailApi(updateUserDto),
    onSuccess: () => {
      toast.success("Email updated successfully.");
      queryClient.invalidateQueries({ queryKey: ["user"] });
    },
    onError: (error) => {
      toast.error("Error updating email. Please try again.");
    },
  });

  return {
    updateEmail,
    isUpdating,
  };
}
