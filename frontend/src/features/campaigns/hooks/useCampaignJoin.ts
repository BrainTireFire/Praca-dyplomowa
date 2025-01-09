import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addCharacterToCampaign } from "../../../services/apiCampaigns";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

function useCampaignJoin() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { mutate: joinCampaign, isPending } = useMutation({
    mutationFn: ({
      campaignId,
      characterId,
    }: {
      campaignId: number;
      characterId: number;
    }) => addCharacterToCampaign(campaignId, characterId),
    onSuccess: (campaignId: number) => {
      toast.success("Successfuly Joined");
      queryClient.invalidateQueries({
        queryKey: ["campaigns"],
      });
      queryClient.invalidateQueries({
        queryKey: ["character"],
      });
      navigate(`/campaigns/${campaignId}`);
    },
    onError: (error) => toast.error(error.message),
  });

  return { joinCampaign, isPending };
}

export default useCampaignJoin;
