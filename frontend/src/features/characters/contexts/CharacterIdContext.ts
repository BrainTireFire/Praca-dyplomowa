import { createContext } from "react";

export const CharacterIdContext = createContext<CharacterIdContextType>({
  characterId: -1,
});

export type CharacterIdContextType = {
  characterId: number;
  //   setCharacterId: (characterId: number) => void;
};
