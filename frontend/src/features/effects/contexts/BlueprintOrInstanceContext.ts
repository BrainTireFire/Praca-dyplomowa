import { createContext } from "react";

export const EffectContext = createContext<EffectContextType>({
  effect: "Blueprint",
  effectId: null,
});

export type EffectContextType = {
  effect: "Blueprint" | "Instance";
  effectId: number | null;
};
