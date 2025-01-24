import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { nextTurn as nexTurnApi } from "../../../services/apiEncounter";

function useNextTurn(encounterId: number, onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: nextTurn, isPending } = useMutation({
    mutationFn: () => nexTurnApi(encounterId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["initiativeQueue", encounterId],
      });
      queryClient.invalidateQueries({
        queryKey: ["isItMyTurn", encounterId],
      });
      toast.success("Next turn!");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { nextTurn, isPending };
}

export default useNextTurn;
