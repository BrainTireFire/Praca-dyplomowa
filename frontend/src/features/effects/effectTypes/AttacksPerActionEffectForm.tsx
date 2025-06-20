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

const effectTypes = ["AttacksTotal", "AdditionalAttacks"] as const;

type effectType = (typeof effectTypes)[number];

export type Effect = ValueEffect & {
  effectType: { attackPerActionEffect: effectType };
};

type Action = {
  type: "setEffectType" | "setValue" | "setRollMoment";
  payload: effectType | DiceSetExtended | string | null;
};

export const initialState: Effect = {
  effectType: { attackPerActionEffect: "AttacksTotal" },
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
          attackPerActionEffect: action.payload as effectType,
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

  return newState;
};

export default function AttacksPerActionEffectForm({
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
          { label: "Attacks total", value: "AttacksTotal" },
          { label: "Additional attacks", value: "AdditionalAttacks" },
        ]}
        label="Attack per action effect"
        name="attackEffect"
        onChange={(x) =>
          dispatch({ type: "setEffectType", payload: x as effectType })
        }
        currentValue={state.effectType.attackPerActionEffect}
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
