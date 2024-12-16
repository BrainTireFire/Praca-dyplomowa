import { useCallback, useContext, useEffect, useReducer } from "react";
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
import { ValueEffect } from "../valueEffect";
import { rollMoment, rollMomentDropdown } from "../rollMoment";
import { EffectContext } from "../contexts/BlueprintOrInstanceContext";

const sizeEffects = ["Bonus", "Change"] as const;

type sizeEffect = (typeof sizeEffects)[number];

export type Effect = ValueEffect & {
  effectType: {
    sizeEffect: sizeEffect;
    sizeEffect_SizeToSet: size;
  };
};

type Action = EffectAction | ValueAction | SizeAction | RollMomentAction;
type EffectAction = {
  type: "setEffectType";
  payload: sizeEffect;
};
type ValueAction = {
  type: "setValue";
  payload: DiceSetExtended;
};
type SizeAction = {
  type: "setSize";
  payload: size;
};
type RollMomentAction = {
  type: "setRollMoment";
  payload: string | null;
};

export const initialState: Effect = {
  effectType: { sizeEffect: "Bonus", sizeEffect_SizeToSet: "Medium" },
  value: DiceSetExtendedDefaultValue,
  rollMoment: "OnResolve",
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setEffectType":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          sizeEffect: action.payload as sizeEffect,
        },
      };
      break;
    case "setValue":
      newState = { ...state, value: action.payload as DiceSetExtended };
      break;
    case "setRollMoment":
      newState = { ...state, rollMoment: action.payload as rollMoment };
      break;
    case "setSize":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          sizeEffect_SizeToSet: action.payload as size,
        },
      };
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
  const effectContext = useContext(EffectContext);
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
          { label: "Bonus", value: "Bonus" },
          { label: "Change", value: "Change" },
        ]}
        label="Size effect"
        name="sizeEffect"
        onChange={(x) =>
          dispatch({ type: "setEffectType", payload: x as sizeEffect })
        }
        currentValue={state.effectType.sizeEffect}
      ></RadioGroup>
      {effectContext.effect === "Blueprint" && (
        <FormRowVertical label="Dice roll moment">
          <Dropdown
            valuesList={rollMomentDropdown}
            chosenValue={state.rollMoment}
            setChosenValue={(e) =>
              dispatch({ type: "setRollMoment", payload: e })
            }
          ></Dropdown>
        </FormRowVertical>
      )}
      <FormRowVertical label="Value">
        <DiceSetForm
          onChange={handleValueFormStateUpdate}
          diceSet={effect.value}
        ></DiceSetForm>
      </FormRowVertical>
      <FormRowVertical label="Size to set">
        <Dropdown
          setChosenValue={(x) =>
            dispatch({ type: "setSize", payload: x as size })
          }
          chosenValue={state.effectType.sizeEffect_SizeToSet}
          valuesList={sizesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
