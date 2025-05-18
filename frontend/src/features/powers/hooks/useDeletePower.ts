import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deletePower as deletePowerApi } from "../../../services/apiPowers";

export function useDeletePower(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: deletePower, isPending } = useMutation({
    mutationFn: (powerId: number) => deletePowerApi(powerId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["powerList"] });
      onSuccess();
    },
    onError: (error) => {
      toast.error(error.message);
    },
  });

  return {
    deletePower,
    isPending,
  };
}
