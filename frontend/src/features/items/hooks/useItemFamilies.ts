import { useQuery } from "@tanstack/react-query";
import { getItemFamilies } from "../../../services/apiItems";
import { ItemType } from "../../../pages/items/itemTypes";

export function useItemFamilies(
  itemId: number | null,
  itemIdentities: ItemType[]
) {
  const {
    isLoading,
    data: itemFamilies,
    error,
  } = useQuery({
    queryKey: ["itemFamilies", itemId, itemIdentities],
    queryFn: () => getItemFamilies(itemId, itemIdentities),
  });

  return { isLoading, itemFamilies, error };
}
