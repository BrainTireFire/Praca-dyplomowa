import { useMutation } from "@tanstack/react-query";
import { postCampaign } from "../../../services/apiCampaigns";
import toast from "react-hot-toast";

function useCreateCampaign() {
  const { mutate: createCampaign, isPending } = useMutation({
    mutationFn: (campaign: CampaignInsertDto) => postCampaign(campaign),
    onSuccess: () => toast.success("Campaign created"),
    onError: () => toast.error("Something went wrong"),
  });

  return { createCampaign, isPending };
}

export default useCreateCampaign;
