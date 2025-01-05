import { useCallback, useContext, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";
import { ValueEffect } from "../valueEffect";
import { rollMoment, rollMomentDropdown } from "../rollMoment";
import { EffectContext } from "../contexts/BlueprintOrInstanceContext";
import { EditModeContext } from "../../../context/EditModeContext";

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

export type Effect = ValueEffect & {
  effectType: { damageEffect: effectType; damageEffect_DamageType: damageType };
};

type Action = {
  type: "setEffectType" | "setDamageType" | "setValue" | "setRollMoment";
  payload: effectType | damageType | DiceSetExtended | string | null;
};

export const initialState: Effect = {
  effectType: { damageEffect: "DamageDealt", damageEffect_DamageType: "acid" },
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
    case "setRollMoment":
      newState = { ...state, rollMoment: action.payload as rollMoment };
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
          disabled={disableUpdate}
          chosenValue={state.effectType.damageEffect_DamageType}
          setChosenValue={(e) =>
            dispatch({ type: "setDamageType", payload: e as damageType })
          }
          valuesList={damageTypesDropdown}
        ></Dropdown>
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
