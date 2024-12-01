import { useCallback, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";

const hitpointEffects = ["TemporaryHitpoints", "HitpointMaximumBonus"] as const;

type hitpointEffect = (typeof hitpointEffects)[number];

export type Effect = {
  effectType: {
    hitpointEffect: hitpointEffect;
  };
  value: DiceSetExtended;
};

type Action = {
  type: "setEffectType" | "setValue";
  payload: hitpointEffect | DiceSetExtended;
};

export const initialState: Effect = {
  effectType: {
    hitpointEffect: "TemporaryHitpoints",
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
          hitpointEffect: action.payload as hitpointEffect,
        },
      };
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
          { label: "Temporary hitpoints", value: "TemporaryHitpoints" },
          { label: "Hitpoint maximum bonus", value: "HitpointMaximumBonus" },
        ]}
        label="Hitpoint effect"
        name="hitpointEffect"
        onChange={(x) =>
          dispatch({ type: "setEffectType", payload: x as hitpointEffect })
        }
        currentValue={state.effectType.hitpointEffect}
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
