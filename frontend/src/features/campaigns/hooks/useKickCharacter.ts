import { useMutation, useQueryClient } from "@tanstack/react-query";
import { removeCharacterFromCampaign } from "../../../services/apiCampaigns";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

function useKickCharacter() {
  const queryClient = useQueryClient();
  const { mutate: kickCharacter, isPending } = useMutation({
    mutationFn: (characterId: number) =>
      removeCharacterFromCampaign(characterId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["campaign"],
      });
      toast.success("Character kick successful");
    },
    onError: (err) => toast.error(err.message),
  });

  return { kickCharacter, isPending };
}

export default useKickCharacter;
