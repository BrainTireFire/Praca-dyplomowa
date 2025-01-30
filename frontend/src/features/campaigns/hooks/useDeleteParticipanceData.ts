import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { deleteParticipanceData as deleteParticipanceDataApi } from "../../../services/apiEncounter";

function useDeleteParticipanceData(
  encounterId: number,
  characterId: number,
  onSuccess: () => void
) {
  const queryClient = useQueryClient();
  const { mutate: deleteParticipanceData, isPending } = useMutation({
    mutationFn: () => deleteParticipanceDataApi(encounterId, characterId),
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
      queryClient.invalidateQueries({
        queryKey: ["encounter", encounterId],
      });
      queryClient.refetchQueries({
        queryKey: ["encounter", encounterId],
      });
      toast.success("Removed participant");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { deleteParticipanceData, isPending };
}

export default useDeleteParticipanceData;
