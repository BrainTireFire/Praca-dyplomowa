import { useMutation, useQueryClient } from "@tanstack/react-query";
import { postCampaign } from "../../../services/apiCampaigns";
import toast from "react-hot-toast";

function useCreateCampaign() {
  const queryClient = useQueryClient();
  const { mutate: createCampaign, isPending } = useMutation({
    mutationFn: (campaign: CampaignInsertDto) => postCampaign(campaign),
    onSuccess: () => {
      toast.success("Campaign created");
      queryClient.invalidateQueries({
        queryKey: ["campaigns"],
      });
    },
    onError: () => toast.error("Something went wrong"),
  });

  return { createCampaign, isPending };
}

export default useCreateCampaign;
