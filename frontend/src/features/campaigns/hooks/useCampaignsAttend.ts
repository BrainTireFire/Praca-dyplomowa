import { useQuery } from "@tanstack/react-query";
import { getCampaignsAttend as getCampaignsAttendApi } from "../../../services/apiCampaigns";

export function useCampaignsAttend() {
  const {
    isLoading,
    data: campaigns,
    error,
  } = useQuery({
    queryKey: ["attendCampaigns"],
    queryFn: getCampaignsAttendApi,
  });

  return { isLoading, campaigns, error };
}
