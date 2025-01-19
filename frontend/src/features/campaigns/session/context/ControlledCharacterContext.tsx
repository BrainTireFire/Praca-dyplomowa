import React from "react";

export const ControlledCharacterContext = React.createContext<
  [number, React.SetStateAction<any>]
>([-1, () => {}]);
