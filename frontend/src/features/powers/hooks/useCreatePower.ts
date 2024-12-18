import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { Power } from "../../../features/powers/models/power";
import { postPower } from "../../../services/apiPowers";

export function useCreatePower(onSuccess: (id: number) => void) {
  const queryClient = useQueryClient();
  const { mutate: createPower, isPending } = useMutation({
    mutationFn: (power: Power) => postPower(power),
    onSuccess: (id: number) => {
      queryClient.invalidateQueries({ queryKey: ["powerList"] });
      onSuccess(id);
    },
    onError: (error) => {
      console.error(error);
      toast.error("Power creation failed");
    },
  });

  return {
    createPower,
    isPending,
  };
}
