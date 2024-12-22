import { createContext } from "react";

export const CharacterIdContext = createContext<CharacterIdContextType>({
  characterId: null,
});

export type CharacterIdContextType = {
  characterId: number | null;
  //   setCharacterId: (characterId: number) => void;
};
