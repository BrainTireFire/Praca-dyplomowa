import { createContext } from "react";

export const EffectContext = createContext<EffectContextType>({
  effect: "Blueprint",
});

export type EffectContextType = {
  effect: "Blueprint" | "Instance";
};
