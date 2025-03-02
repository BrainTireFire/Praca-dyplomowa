import { useQuery } from "@tanstack/react-query";
import { getShops } from "../../../../services/apiShops";
import { useParams } from "react-router-dom";

export function useShops() {
  const { campaignId } = useParams<{ campaignId: string }>();
  const {
    isPending,
    data: shops,
    error,
  } = useQuery({
    queryKey: ["shops"],
    queryFn: () => getShops(Number(campaignId)),
  });

  return { isPending, shops, error };
}
