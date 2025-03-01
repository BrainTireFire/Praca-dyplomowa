import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { moveCharacter as moveCharacterApi } from "../../../services/apiEncounter";

function useMoveCharacter(
  encounterId: number,
  characterId: number,
  onSuccess: (x: number[]) => void
) {
  const queryClient = useQueryClient();
  const { mutate: moveCharacter, isPending } = useMutation({
    mutationFn: (fieldIds: number[]) => {
      return moveCharacterApi(encounterId, characterId, fieldIds);
    },
    onSuccess: (traversablePath: number[]) => {
      queryClient.invalidateQueries({
        queryKey: ["encounter", encounterId],
      });
      queryClient.invalidateQueries({
        queryKey: ["participance", encounterId, characterId],
      });
      toast.success(traversablePath.toString());
      onSuccess(traversablePath);
    },
    onError: (err) => toast.error(err.message),
  });

  return { moveCharacter, isPending };
}

export default useMoveCharacter;
