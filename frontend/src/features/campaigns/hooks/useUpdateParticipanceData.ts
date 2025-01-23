import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import {
  ParticipanceData,
  updateParticipanceData as updateParticipanceDataApi,
} from "../../../services/apiEncounter";

function useUpdateParticipanceData(
  encounterId: number,
  characterId: number,
  onSuccess: () => void
) {
  const queryClient = useQueryClient();
  const { mutate: updateParticipanceData, isPending } = useMutation({
    mutationFn: (participanceData: ParticipanceData) =>
      updateParticipanceDataApi(encounterId, characterId, participanceData),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["participance", encounterId, characterId],
      });
      queryClient.refetchQueries({
        queryKey: ["participance", encounterId, characterId],
      });
      toast.success("Initiative roll succesfull");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { updateParticipanceData, isPending };
}

export default useUpdateParticipanceData;
