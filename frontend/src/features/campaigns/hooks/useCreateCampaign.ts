import { useMutation, useQueryClient } from "@tanstack/react-query";
import { postCampaign } from "../../../services/apiCampaigns";
import toast from "react-hot-toast";
import { CampaignInsertDto } from "../../../models/campaign";
import { useNavigate } from "react-router-dom";

function useCreateCampaign() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { mutate: createCampaign, isPending } = useMutation({
    mutationFn: (campaign: CampaignInsertDto) => postCampaign(campaign),
    onSuccess: (campaignId: number) => {
      toast.success("Campaign created");
      queryClient.invalidateQueries({
        queryKey: ["campaigns"],
      });
      navigate(`/campaigns/${campaignId}`);
    },
    onError: (error) => toast.error(error.message),
  });

  return { createCampaign, isPending };
}

export default useCreateCampaign;
