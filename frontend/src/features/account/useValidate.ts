import { useMutation, useQueryClient } from "@tanstack/react-query";
import { validateToken } from "../../services/apiAuth";
import toast from "react-hot-toast";

export function useValidate() {
  const queryClient = useQueryClient();

  const { mutateAsync: validate, isLoading } = useMutation({
    mutationFn: validateToken,
    onSuccess: (data) => {
      const existingUserData = queryClient.getQueryData(["user"]) || {};
      var userData = null;
      if (existingUserData && !existingUserData?.username) {
        userData = data;
      } else {
        userData = existingUserData;
      }
      console.log(userData);
      const updatedData = { ...userData, isAuthenticated: true };
      queryClient.setQueryData(["user"], updatedData);
    },
    onError: (error) => {
      console.error(error);
      toast.error("Something went wrong! Please try again.");
    },
  });

  return {
    validate,
    isLoading,
  };
}
