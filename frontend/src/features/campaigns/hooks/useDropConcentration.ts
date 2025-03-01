import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { dropConcentration as dropConcentrationApi } from "../../../services/apiCharacters";

function useDropConcentration(
  encounterId: number,
  characterId: number,
  onSuccess: () => void
) {
  const queryClient = useQueryClient();
  const { mutate: dropConcentration, isPending } = useMutation({
    mutationFn: () => dropConcentrationApi(characterId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["participance", encounterId, characterId],
      });
      queryClient.invalidateQueries({
        queryKey: ["concentration", characterId],
      });
      toast.success("Changed active character");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { dropConcentration, isPending };
}

export default useDropConcentration;
