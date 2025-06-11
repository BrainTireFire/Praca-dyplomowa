import { useQuery } from "@tanstack/react-query";
import { getShopCharacter } from "../../../../services/apiShops";

export function useShopCharacters(characterId: number) {
  const {
    isLoading,
    data: shopCharacter,
    error,
  } = useQuery({
    queryKey: ["shopCharacter", characterId],
    queryFn: () => getShopCharacter(characterId),
  });

  return { isLoading, shopCharacter, error };
}
