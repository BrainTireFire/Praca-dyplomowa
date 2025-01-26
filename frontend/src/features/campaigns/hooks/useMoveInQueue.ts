import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { moveInQueue as moveInQueueApi } from "../../../services/apiEncounter";

function useMoveInQueue(
  encounterId: number,
  characterId: number,
  onSuccess: () => void
) {
  const queryClient = useQueryClient();
  const { mutate: moveInQueue, isPending } = useMutation({
    mutationFn: (up: boolean) => moveInQueueApi(encounterId, characterId, up),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["participance", encounterId, characterId],
      });
      queryClient.refetchQueries({
        queryKey: ["participance", encounterId, characterId],
      });
      queryClient.invalidateQueries({
        queryKey: ["initiativeQueue", encounterId],
      });
      queryClient.refetchQueries({
        queryKey: ["initiativeQueue", encounterId],
      });
      toast.success("Changed place in queue");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { moveInQueue, isPending };
}

export default useMoveInQueue;
