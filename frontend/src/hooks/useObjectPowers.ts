import { useQuery } from "@tanstack/react-query";
import { getItemPowers } from "../services/apiItems";
import {
  getCharacterPowers,
  getCharacterPowersPrepared,
  getCharacterPowersToPrepare,
} from "../services/apiCharacters";

export function useObjectPowers(
  objectId: number | null,
  objectType: "Item" | "Character" | "CharacterPrepared" | "CharacterToPrepare"
) {
  const {
    isLoading,
    data: powers,
    error,
  } = useQuery({
    queryKey:
      objectType === "Item"
        ? ["itemPowerList"]
        : objectType === "Character"
        ? ["characterPowerList"]
        : objectType === "CharacterPrepared"
        ? ["characterPreparedPowerList"]
        : ["characterToPreparePowerList"],
    queryFn: () => {
      if (objectId) {
        console.log("Load: " + objectId);
        if (objectType === "Item") return getItemPowers(objectId);
        if (objectType === "Character") return getCharacterPowers(objectId);
        if (objectType === "CharacterPrepared")
          return getCharacterPowersPrepared(objectId);
        if (objectType === "CharacterToPrepare")
          return getCharacterPowersToPrepare(objectId);
      }
      return Promise.reject(new Error("Object ID is undefined"));
    },
  });

  return { isLoading, powers, error };
}
