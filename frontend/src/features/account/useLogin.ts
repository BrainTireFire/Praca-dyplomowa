import { login as loginApi } from "../../services/apiAuth";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import { useAuth } from "../../context/AuthContext";

type LoginData = {
  username: string;
  password: string;
};

export function useLogin() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { setPayloadContainer } = useAuth();

  const { mutate: login, isLoading } = useMutation({
    mutationFn: ({ username, password }: LoginData) =>
      loginApi({ username, password }),
    onSuccess: (data) => {
      queryClient.setQueryData(["user"], data);
      setPayloadContainer({ payload: true });
      navigate("/main", { replace: true });
    },
    onError: (error) => {
      toast.error("Provided credentials are incorrect");
    },
  });

  return {
    login,
    isLoading,
  };
}
