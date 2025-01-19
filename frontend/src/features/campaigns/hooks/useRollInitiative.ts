import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { rollInitiative as rollInitiativeApi } from "../../../services/apiEncounter";

function useRollInitiative(encounterId: number, onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: rollInitiative, isPending } = useMutation({
    mutationFn: () => rollInitiativeApi(encounterId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["initiativeQueue", encounterId],
      });
      queryClient.refetchQueries({
        queryKey: ["initiativeQueue", encounterId],
      });
      toast.success("Initiative roll succesfull");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { rollInitiative, isPending };
}

export default useRollInitiative;
