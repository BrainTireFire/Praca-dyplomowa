import { useCallback, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import { abilities, abilitiesDropdown, ability } from "../abilities";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";
import { statusEffect } from "../statusEffects";

export type Effect = {
  effectType: {
    savingThrowEffect: SavingThrowEffect;
    savingThrowEffect_Ability: ability;
    savingThrowEffect_Condition: statusEffect | null;
    savingThrowEffect_Nature: SavingThrowEffect_Nature | null;
  };
  value: DiceSetExtended;
};

export type SavingThrowEffect =
  | "bonus"
  | "proficiency"
  | "advantage"
  | "rerollLowerThan";
export type SavingThrowEffect_Nature = "Physical" | "Magical";

type Action = {
  type: "setEffectType" | "setValue" | "setAbility";
  payload: any;
};

export const initialState: Effect = {
  effectType: {
    savingThrowEffect: "bonus",
    savingThrowEffect_Ability: "STRENGTH",
    savingThrowEffect_Condition: null,
    savingThrowEffect_Nature: null,
  },
  value: DiceSetExtendedDefaultValue,
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
  console.log(newState);
  return newState;
};

export default function SavingThrowEffectForm({
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
          onChange={handleValueFormStateUpdate}
          diceSet={effect.value}
        ></DiceSetForm>
      </FormRowVertical>
      <FormRowVertical label="Ability">
        <Dropdown
          chosenValue={state.effectType.savingThrowEffect_Ability}
          setChosenValue={(e) => dispatch({ type: "setAbility", payload: e })}
          valuesList={abilitiesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
