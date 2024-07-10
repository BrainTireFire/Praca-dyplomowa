import { useMutation } from "@tanstack/react-query";
import { validateToken } from "../../services/apiAuth";
import toast from "react-hot-toast";

export function useValidate() {
  const { mutateAsync: validate, isLoading } = useMutation({
    mutationFn: validateToken,
    onSuccess: (data) => {
      console.log("data ", data);
    },
    onError: (error) => {
      console.error(error);
      toast.error("Provided credentials are incorrect");
    },
  });

  return {
    validate,
    isLoading,
  };
}
