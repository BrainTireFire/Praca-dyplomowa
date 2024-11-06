import { useCallback, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";

export type Effect = {
  range: "melee" | "ranged";
  source: "weapon" | "spell";
  effectType: "bonus" | "rerollLowerThan";
  value: DiceSetExtended;
};

type Action = {
  type: "setEffectType" | "setValue" | "setRange" | "setSource";
  payload: any;
};

export const initialState: Effect = {
  range: "melee",
  source: "weapon",
  effectType: "bonus",
  value: DiceSetExtendedDefaultValue,
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setEffectType":
      newState = { ...state, effectType: action.payload };
      break;
    case "setValue":
      newState = { ...state, value: action.payload };
      break;
    case "setSource":
      newState = { ...state, source: action.payload };
      break;
    case "setRange":
      newState = { ...state, range: action.payload };
      break;
    default:
      newState = state;
      break;
  }
  console.log(newState);
  return newState;
};

export default function AttackRollEffectForm({
  onChange,
  effect,
}: {
  onChange: (updatedState: Effect) => void;
  effect: Effect;
}) {
  const [state, dispatch] = useReducer(effectReducer, effect);
  useEffect(() => {
    onChange(state);
  }, [state, onChange]);

  const handleValueFormStateUpdate = useCallback((e: DiceSetExtended) => {
    dispatch({ type: "setValue", payload: e });
  }, []);
  return (
    <Box>
      <RadioGroup
        values={[
          { label: "Melee", value: "melee" },
          { label: "Ranged", value: "ranged" },
        ]}
        label="Range"
        name="range"
        onChange={(x) => dispatch({ type: "setRange", payload: x })}
        currentValue={state.range}
      ></RadioGroup>
      <RadioGroup
        values={[
          { label: "Weapon", value: "weapon" },
          { label: "Spell", value: "spell" },
        ]}
        label="Source"
        name="source"
        onChange={(x) => dispatch({ type: "setSource", payload: x })}
        currentValue={state.source}
      ></RadioGroup>
      <RadioGroup
        values={[
          { label: "Bonus", value: "bonus" },
          { label: "Reroll lower than", value: "rerollLowerThan" },
        ]}
        label="Attack roll effect"
        name="attackRollEffect"
        onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
        currentValue={state.effectType}
      ></RadioGroup>
      <FormRowVertical label="Value">
        <DiceSetForm onChange={handleValueFormStateUpdate}></DiceSetForm>
      </FormRowVertical>
    </Box>
  );
}
