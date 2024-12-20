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
  effectType: {
    attackRollEffect_Range: "melee" | "ranged";
    attackRollEffect_Source: "weapon" | "spell";
    attackRollEffect_Type: "bonus" | "rerollLowerThan";
  };
  value: DiceSetExtended;
};

type Action = {
  type: "setEffectType" | "setValue" | "setRange" | "setSource";
  payload: any;
};

export const initialState: Effect = {
  effectType: {
    attackRollEffect_Range: "melee",
    attackRollEffect_Source: "weapon",
    attackRollEffect_Type: "bonus",
  },
  value: DiceSetExtendedDefaultValue,
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
        currentValue={state.effectType.attackRollEffect_Range}
      ></RadioGroup>
      <RadioGroup
        values={[
          { label: "Weapon", value: "weapon" },
          { label: "Spell", value: "spell" },
        ]}
        label="Source"
        name="source"
        onChange={(x) => dispatch({ type: "setSource", payload: x })}
        currentValue={state.effectType.attackRollEffect_Source}
      ></RadioGroup>
      <RadioGroup
        values={[
          { label: "Bonus", value: "bonus" },
          { label: "Reroll lower than", value: "rerollLowerThan" },
        ]}
        label="Attack roll effect"
        name="attackRollEffect"
        onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
        currentValue={state.effectType.attackRollEffect_Type}
      ></RadioGroup>
      <FormRowVertical label="Value">
        <DiceSetForm
          onChange={handleValueFormStateUpdate}
          diceSet={effect.value}
        ></DiceSetForm>
      </FormRowVertical>
    </Box>
  );
}
