import { useMutation, useQueryClient } from "@tanstack/react-query";
import { removeCampaign as removeCampaignApi } from "../../../services/apiCampaigns";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";

function useRemoveCampaign() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { mutate: removeCampaign, isPending } = useMutation({
    mutationFn: (campaignId: number) => removeCampaignApi(campaignId),
    onSuccess: () => {
      toast.success("Campaign has been removed");
      queryClient.invalidateQueries({
        queryKey: ["campaigns"],
      });
      navigate("/campaigns");
    },
    onError: (err) => toast.error(err.message),
  });

  return { removeCampaign, isPending };
}

export default useRemoveCampaign;
