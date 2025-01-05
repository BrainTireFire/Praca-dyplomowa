import { createContext } from "react";

export const EditModeContext = createContext<EditModeContextType>({
  editMode: true,
});

export type EditModeContextType = {
  editMode: boolean;
};
