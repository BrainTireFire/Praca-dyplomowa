import { useCallback, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import { abilities, abilitiesDropdown } from "../abilities";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";

export type Effect = {
  effectType: {
    abilityEffect: "Bonus" | "RerollLowerThan" | "Advantage";
    abilityEffect_Ability: (typeof abilities)[number];
  };
  value: DiceSetExtended;
};

type Action = {
  type: "setEffectType" | "setValue" | "setAbility";
  payload: any;
};

export const initialState: Effect = {
  effectType: { abilityEffect: "Bonus", abilityEffect_Ability: "STRENGTH" },
  value: DiceSetExtendedDefaultValue,
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setEffectType":
      newState = {
        ...state,
        effectType: { ...state.effectType, abilityEffect: action.payload },
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
          abilityEffect_Ability: action.payload,
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

export default function AbilityEffectForm({
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
          { label: "Reroll lower than", value: "RerollLowerThan" },
        ]}
        label="Ability effect"
        name="abilityEffect"
        onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
        currentValue={state.effectType.abilityEffect}
      ></RadioGroup>
      <FormRowVertical label="Value">
        <DiceSetForm
          onChange={handleValueFormStateUpdate}
          diceSet={effect.value}
        ></DiceSetForm>
      </FormRowVertical>
      <FormRowVertical label="Ability">
        <Dropdown
          chosenValue={state.effectType.abilityEffect_Ability}
          setChosenValue={(e) => dispatch({ type: "setAbility", payload: e })}
          valuesList={abilitiesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
