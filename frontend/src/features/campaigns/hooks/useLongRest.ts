import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { longRest as longRestApi } from "../../../services/apiCampaigns";

function useLongRest(campaignId: number, onSuccess: () => void) {
  const queryClient = useQueryClient();
  const { mutate: longRest, isPending } = useMutation({
    mutationFn: () => longRestApi(campaignId),
    onSuccess: () => {
      toast.success("Long rest performed!");
      onSuccess();
    },
    onError: (err) => toast.error(err.message),
  });

  return { longRest, isPending };
}

export default useLongRest;
