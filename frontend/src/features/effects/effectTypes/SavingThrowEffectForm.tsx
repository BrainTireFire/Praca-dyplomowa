import { useCallback, useContext, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import { abilitiesDropdown, ability } from "../abilities";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";
import { statusEffect } from "../statusEffects";
import { ValueEffect } from "../valueEffect";
import { rollMoment, rollMomentDropdown } from "../rollMoment";
import { EffectContext } from "../contexts/BlueprintOrInstanceContext";
import { EditModeContext } from "../../../context/EditModeContext";

export type Effect = ValueEffect & {
  effectType: {
    savingThrowEffect: SavingThrowEffect;
    savingThrowEffect_Ability: ability;
    savingThrowEffect_Condition: statusEffect | null;
    savingThrowEffect_Nature: SavingThrowEffect_Nature | null;
  };
};

export type SavingThrowEffect =
  | "Bonus"
  | "Proficiency"
  | "Advantage"
  | "RerollLowerThan";
export type SavingThrowEffect_Nature = "Physical" | "Magical";

type Action = {
  type: "setEffectType" | "setValue" | "setAbility" | "setRollMoment";
  payload: any;
};

export const initialState: Effect = {
  effectType: {
    savingThrowEffect: "Bonus",
    savingThrowEffect_Ability: "STRENGTH",
    savingThrowEffect_Condition: null,
    savingThrowEffect_Nature: null,
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
        effectType: { ...state.effectType, savingThrowEffect: action.payload },
      };
      break;
    case "setValue":
      newState = { ...state, value: action.payload };
      break;
    case "setRollMoment":
      newState = { ...state, rollMoment: action.payload as rollMoment };
      break;
    case "setAbility":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          savingThrowEffect_Ability: action.payload,
        },
      };
      break;
    default:
      newState = state;
      break;
  }

  return newState;
};

export default function SavingThrowEffectForm({
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
          { label: "Bonus", value: "Bonus" },
          { label: "Advantage", value: "Advantage" },
          { label: "Proficiency", value: "Proficiency" },
          { label: "Reroll lower than", value: "RerollLowerThan" },
        ]}
        label="Saving throw effect"
        name="savingThrowEffect"
        onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
        currentValue={state.effectType.savingThrowEffect}
      ></RadioGroup>
      <FormRowVertical label="Value">
        <DiceSetForm
          disabled={disableUpdate}
          onChange={handleValueFormStateUpdate}
          diceSet={effect.value}
        ></DiceSetForm>
      </FormRowVertical>
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
      <FormRowVertical label="Ability">
        <Dropdown
          disabled={disableUpdate}
          chosenValue={state.effectType.savingThrowEffect_Ability}
          setChosenValue={(e) => dispatch({ type: "setAbility", payload: e })}
          valuesList={abilitiesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
