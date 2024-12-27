import { useCallback, useContext, useEffect, useReducer } from "react";
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
import { ValueEffect } from "../valueEffect";
import { rollMomentDropdown } from "../rollMoment";
import { EffectContext } from "../contexts/BlueprintOrInstanceContext";
import { EditModeContext } from "../../../context/EditModeContext";

export type Effect = ValueEffect & {
  effectType: {
    abilityEffect: "Bonus" | "RerollLowerThan" | "Advantage";
    abilityEffect_Ability: (typeof abilities)[number];
  };
};

type Action = {
  type: "setEffectType" | "setValue" | "setAbility" | "setRollMoment";
  payload: any;
};

export const initialState: Effect = {
  effectType: { abilityEffect: "Bonus", abilityEffect_Ability: "STRENGTH" },
  value: DiceSetExtendedDefaultValue,
  rollMoment: "OnCast",
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
    case "setRollMoment":
      newState = { ...state, rollMoment: action.payload };
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
  const { editMode } = useContext(EditModeContext);
  const disableUpdate = !editMode;
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
        disabled={disableUpdate}
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
      <FormRowVertical label="Ability">
        <Dropdown
          disabled={disableUpdate}
          chosenValue={state.effectType.abilityEffect_Ability}
          setChosenValue={(e) => dispatch({ type: "setAbility", payload: e })}
          valuesList={abilitiesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
