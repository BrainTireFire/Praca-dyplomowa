import { useMutation, useQueryClient } from "@tanstack/react-query";
import { signup as signupApi } from "../../services/apiAuth";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

export function useSignup() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const { mutate: signup, isLoading } = useMutation({
    mutationFn: signupApi,
    onSuccess: (user) => {
      toast.success("Account created successfully.");
      queryClient.setQueryData(["user"], user);
      navigate("/main", { replace: true });
    },
    onError: (error) => {
      toast.error("Error signing up. Please try again.");
    },
  });

  return {
    signup,
    isLoading,
  };
}
