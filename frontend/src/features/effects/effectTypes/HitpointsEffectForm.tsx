import { useCallback, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";

const effectTypes = ["temporaryHitpoints", "hitpointMaximumBonus"] as const;

type effectType = (typeof effectTypes)[number];

export type Effect = {
  effectType: effectType;
  value: DiceSetExtended;
};

type Action = {
  type: "setEffectType" | "setValue";
  payload: effectType | DiceSetExtended;
};

export const initialState: Effect = {
  effectType: "temporaryHitpoints",
  value: DiceSetExtendedDefaultValue,
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setEffectType":
      newState = { ...state, effectType: action.payload as effectType };
      break;
    case "setValue":
      newState = { ...state, value: action.payload as DiceSetExtended };
      break;
    default:
      newState = state;
      break;
  }
  console.log(newState);
  return newState;
};

export default function HitpointEffectForm({
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
          { label: "Temporary hitpoints", value: "temporaryHitpoints" },
          { label: "Hitpoint maximum bonus", value: "hitpointMaximumBonus" },
        ]}
        label="Hitpoint effect"
        name="hitpointEffect"
        onChange={(x) =>
          dispatch({ type: "setEffectType", payload: x as effectType })
        }
        currentValue={state.effectType}
      ></RadioGroup>
      <FormRowVertical label="Value">
        <DiceSetForm onChange={handleValueFormStateUpdate}></DiceSetForm>
      </FormRowVertical>
    </Box>
  );
}
