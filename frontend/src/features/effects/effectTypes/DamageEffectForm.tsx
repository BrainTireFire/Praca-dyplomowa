import { useCallback, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";

const damageTypes = [
  "acid",
  "bludgeoning",
  "cold",
  "fire",
  "force",
  "lightning",
  "necrotic",
  "piercing",
  "poison",
  "psychic",
  "radiant",
  "slashing",
  "thunder",
] as const;

const damageTypesDropdown = [
  { value: "acid", label: "Acid" },
  { value: "bludgeoning", label: "Bludgeoning" },
  { value: "cold", label: "Cold" },
  { value: "fire", label: "Fire" },
  { value: "force", label: "Force" },
  { value: "lightning", label: "Lightning" },
  { value: "necrotic", label: "Necrotic" },
  { value: "piercing", label: "Piercing" },
  { value: "poison", label: "Poison" },
  { value: "psychic", label: "Psychic" },
  { value: "radiant", label: "Radiant" },
  { value: "slashing", label: "Slashing" },
  { value: "thunder", label: "Thunder" },
];

const effectTypes = ["DamageDealt", "RerollLowerThan", "DamageTaken"] as const;

type effectType = (typeof effectTypes)[number];
type damageType = (typeof damageTypes)[number];

export type Effect = {
  effectType: { damageEffect: effectType; damageEffect_DamageType: damageType };
  value: DiceSetExtended;
};

type Action = {
  type: "setEffectType" | "setDamageType" | "setValue";
  payload: effectType | damageType | DiceSetExtended;
};

export const initialState: Effect = {
  effectType: { damageEffect: "DamageDealt", damageEffect_DamageType: "acid" },
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
          damageEffect: action.payload as effectType,
        },
      };
      break;
    case "setDamageType":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          damageEffect_DamageType: action.payload as damageType,
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

export default function DamageEffectForm({
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
          { label: "Damage dealt", value: "DamageDealt" },
          { label: "Reroll lower than", value: "RerollLowerThan" },
          { label: "Damage taken", value: "DamageTaken" },
        ]}
        label="Damage effect"
        name="damageEffect"
        onChange={(x) =>
          dispatch({ type: "setEffectType", payload: x as effectType })
        }
        currentValue={state.effectType.damageEffect}
      ></RadioGroup>
      <FormRowVertical label="Damage type">
        <Dropdown
          chosenValue={state.effectType.damageEffect_DamageType}
          setChosenValue={(e) =>
            dispatch({ type: "setDamageType", payload: e as damageType })
          }
          valuesList={damageTypesDropdown}
        ></Dropdown>
      </FormRowVertical>
      <FormRowVertical label="Value">
        <DiceSetForm
          onChange={handleValueFormStateUpdate}
          diceSet={effect.value}
        ></DiceSetForm>
      </FormRowVertical>
    </Box>
  );
}
