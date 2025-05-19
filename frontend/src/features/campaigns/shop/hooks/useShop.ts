import { useQuery } from "@tanstack/react-query";
import { getShop } from "../../../../services/apiShops";
import { useParams } from "react-router-dom";

export function useShop() {
  const { shopId } = useParams<{ shopId: string }>();

  const {
    isLoading,
    data: shop,
    error,
  } = useQuery({
    queryKey: ["shop", Number(shopId)],
    queryFn: () => getShop(Number(shopId)),
  });

  return { isLoading, shop, error };
}
