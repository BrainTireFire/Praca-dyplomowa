import { createContext } from "react";

export const PowerIdContext = createContext<PowerIdContextType>({
  powerId: -1,
});

export type PowerIdContextType = {
  powerId: number;
};
