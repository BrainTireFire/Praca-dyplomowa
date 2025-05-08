import { useQuery } from "@tanstack/react-query";
import { getShopItems } from "../../../../services/apiShops";
import { useParams } from "react-router-dom";

export function useShopItems() {
  const { shopId } = useParams<{ shopId: string }>();

  const {
    isLoading,
    data: shopItems,
    error,
  } = useQuery({
    queryKey: ["itemShopList", Number(shopId)],
    queryFn: () => getShopItems(Number(shopId)),
  });

  return { isLoading, shopItems, error };
}
