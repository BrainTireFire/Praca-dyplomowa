import { useCallback, useContext, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";
import { ValueEffect } from "../valueEffect";
import Dropdown from "../../../ui/forms/Dropdown";
import { rollMoment, rollMomentDropdown } from "../rollMoment";
import { EffectContext } from "../contexts/BlueprintOrInstanceContext";
import { EditModeContext } from "../../../context/EditModeContext";

export type Effect = ValueEffect & {
  effectType: {
    attackRollEffect_Range: "Melee" | "Ranged";
    attackRollEffect_Source: "Weapon" | "Spell";
    attackRollEffect_Type: "Bonus" | "RerollLowerThan";
  };
};

type Action = {
  type:
    | "setEffectType"
    | "setValue"
    | "setRange"
    | "setSource"
    | "setRollMoment";
  payload: any;
};

export const initialState: Effect = {
  effectType: {
    attackRollEffect_Range: "Melee",
    attackRollEffect_Source: "Weapon",
    attackRollEffect_Type: "Bonus",
  },
  value: DiceSetExtendedDefaultValue,
  rollMoment: "OnCast",
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setEffectType":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          attackRollEffect_Type: action.payload,
        },
      };
      break;
    case "setValue":
      newState = { ...state, value: action.payload };
      break;
    case "setRollMoment":
      newState = { ...state, rollMoment: action.payload as rollMoment };
      break;
    case "setSource":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          attackRollEffect_Source: action.payload,
        },
      };
      break;
    case "setRange":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          attackRollEffect_Range: action.payload,
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

export default function AttackRollEffectForm({
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
  const { editMode } = useContext(EditModeContext);
  const disableUpdate = !editMode;
  return (
    <Box>
      <RadioGroup
        disabled={disableUpdate}
        values={[
          { label: "Melee", value: "Melee" },
          { label: "Ranged", value: "Ranged" },
        ]}
        label="Range"
        name="range"
        onChange={(x) => dispatch({ type: "setRange", payload: x })}
        currentValue={state.effectType.attackRollEffect_Range}
      ></RadioGroup>
      <RadioGroup
        disabled={disableUpdate}
        values={[
          { label: "Weapon", value: "Weapon" },
          { label: "Spell", value: "Spell" },
        ]}
        label="Source"
        name="source"
        onChange={(x) => dispatch({ type: "setSource", payload: x })}
        currentValue={state.effectType.attackRollEffect_Source}
      ></RadioGroup>
      <RadioGroup
        disabled={disableUpdate}
        values={[
          { label: "Bonus", value: "Bonus" },
          { label: "Reroll lower than", value: "RerollLowerThan" },
        ]}
        label="Attack roll effect"
        name="attackRollEffect"
        onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
        currentValue={state.effectType.attackRollEffect_Type}
      ></RadioGroup>
      {effectContext.effect === "Blueprint" && (
        <FormRowVertical label="Dice roll moment">
          <Dropdown
            disabled={disableUpdate}
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
          disabled={disableUpdate}
          onChange={handleValueFormStateUpdate}
          diceSet={effect.value}
        ></DiceSetForm>
      </FormRowVertical>
    </Box>
  );
}
