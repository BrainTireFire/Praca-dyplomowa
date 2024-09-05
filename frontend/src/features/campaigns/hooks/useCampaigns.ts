import { useQuery } from "@tanstack/react-query";
import { getCampaigns } from "../../services/apiCampaigns";

export function useCampaigns() {
  const {
    isLoading,
    data: campaigns,
    error,
  } = useQuery({
    queryKey: ["campaigns"],
    queryFn: getCampaigns,
  });

  return { isLoading, campaigns, error };
}
