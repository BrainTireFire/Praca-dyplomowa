import { DiceSetExtended } from "./DiceSetForm";
import { rollMoment } from "./rollMoment";

export type ValueEffect = {
  rollMoment: rollMoment;
  value: DiceSetExtended;
};
