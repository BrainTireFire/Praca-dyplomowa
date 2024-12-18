import { useQuery } from "@tanstack/react-query";
import { getItemPowers } from "../services/apiItems";
import { getCharacterPowers } from "../services/apiCharacters";

export function useObjectPowers(
  objectId: number | null,
  objectType: "Item" | "Character"
) {
  const {
    isLoading,
    data: powers,
    error,
  } = useQuery({
    queryKey: ["itemPowerList"],
    queryFn: () => {
      if (objectId) {
        console.log("Load: " + objectId);
        return objectType === "Item"
          ? getItemPowers(objectId)
          : getCharacterPowers(objectId);
      }
      return Promise.reject(new Error("Object ID is undefined"));
    },
  });

  return { isLoading, powers, error };
}
