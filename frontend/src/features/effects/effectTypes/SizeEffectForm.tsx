import { useCallback, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";
import { size, sizesDropdown } from "../sizes";
import Dropdown from "../../../ui/forms/Dropdown";

const effectTypes = ["bonus", "change"] as const;

type effectType = (typeof effectTypes)[number];

export type Effect = {
  effectType: effectType;
  value: DiceSetExtended;
  sizeToSet: size;
};

type Action = EffectAction | ValueAction | SizeAction;
type EffectAction = {
  type: "setEffectType";
  payload: effectType;
};
type ValueAction = {
  type: "setValue";
  payload: DiceSetExtended;
};
type SizeAction = {
  type: "setSize";
  payload: size;
};

export const initialState: Effect = {
  effectType: "bonus",
  value: DiceSetExtendedDefaultValue,
  sizeToSet: "medium",
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
    case "setSize":
      newState = { ...state, sizeToSet: action.payload as size };
      break;
    default:
      newState = state;
      break;
  }
  console.log(newState);
  return newState;
};

export default function SizeEffectForm({
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
          { label: "Change", value: "change" },
        ]}
        label="Size effect"
        name="sizeEffect"
        onChange={(x) =>
          dispatch({ type: "setEffectType", payload: x as effectType })
        }
        currentValue={state.effectType}
      ></RadioGroup>
      <FormRowVertical label="Value">
        <DiceSetForm onChange={handleValueFormStateUpdate}></DiceSetForm>
      </FormRowVertical>
      <FormRowVertical label="Size to set">
        <Dropdown
          setChosenValue={(x) =>
            dispatch({ type: "setSize", payload: x as size })
          }
          chosenValue={state.sizeToSet}
          valuesList={sizesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
