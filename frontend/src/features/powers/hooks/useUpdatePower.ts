import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { Power } from "../models/power";
import { updatePower as updatePowerApi } from "../../../services/apiPowers";

export function useUpdatePower(onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: updatePower, isPending } = useMutation({
    mutationFn: (power: Power) => updatePowerApi(power),
    onSuccess: (result: any) => {
      queryClient.invalidateQueries({ queryKey: ["powerList"] });
      toast.success("Power updated");
      onSuccess();
    },
    onError: (error) => {
      toast.error("Power update failed: " + error.message);
    },
  });

  return {
    updatePower,
    isPending,
  };
}
