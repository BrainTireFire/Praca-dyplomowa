import { useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";

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

export type damageType = (typeof damageTypes)[number];

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

export type Effect = {
  effectType: {
    resistanceEffect:
      | "Resistance"
      | "Immunity"
      | "Vulnerability"
      | "Advantage"
      | "Disadvantage";
    resistanceEffect_DamageType: damageType;
  };
};

type Action = {
  type: "setEffectType" | "setDamageType";
  payload: any;
};

export const initialState: Effect = {
  effectType: {
    resistanceEffect: "Resistance",
    resistanceEffect_DamageType: "acid",
  },
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setEffectType":
      newState = {
        ...state,
        effectType: { ...state.effectType, resistanceEffect: action.payload },
      };
      break;
    case "setDamageType":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          resistanceEffect_DamageType: action.payload,
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

export default function ResistanceEffectForm({
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
  return (
    <Box>
      <RadioGroup
        values={[
          { label: "Resistance", value: "Resistance" },
          { label: "Immunity", value: "Immunity" },
          { label: "Vulnerability", value: "Vulnerability" },
          { label: "Advantage", value: "Advantage" },
          { label: "Disadvantage", value: "Disadvantage" },
        ]}
        label="Resistance effect"
        name="resistanceEffect"
        onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
        currentValue={state.effectType.resistanceEffect}
      ></RadioGroup>
      <FormRowVertical label="Damage type">
        <Dropdown
          chosenValue={state.effectType.resistanceEffect_DamageType}
          setChosenValue={(e) =>
            dispatch({ type: "setDamageType", payload: e })
          }
          valuesList={damageTypesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
