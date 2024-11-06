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
  effectType: "bonus" | "multiplier";
  value: DiceSetExtended;
};

type Action = {
  type: "setEffectType" | "setValue";
  payload: any;
};

export const initialState: Effect = {
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
    default:
      newState = state;
      break;
  }
  console.log(newState);
  return newState;
};

export default function MovementEffectForm({
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
          { label: "Bonus", value: "bonus" },
          { label: "Multiplier", value: "multiplier" },
        ]}
        label="Movement effect"
        name="movementEffect"
        onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
        currentValue={state.effectType}
      ></RadioGroup>
      <FormRowVertical label="Value">
        <DiceSetForm onChange={handleValueFormStateUpdate}></DiceSetForm>
      </FormRowVertical>
    </Box>
  );
}
