import { useCallback, useContext, useEffect, useReducer } from "react";
import Box from "../../../../ui/containers/Box";
import FormRowVertical from "../../../../ui/forms/FormRowVertical";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../../DiceSetForm";
import { ValueEffect } from "../../valueEffect";
import Dropdown from "../../../../ui/forms/Dropdown";
import { rollMomentDropdown } from "../../rollMoment";
import { EffectContext } from "../../contexts/BlueprintOrInstanceContext";
import { EditModeContext } from "../../../../context/EditModeContext";

export type Effect = ValueEffect;

type Action = {
  type: "setValue" | "setRollMoment";
  payload: any;
};

export const initialState: Effect = {
  value: DiceSetExtendedDefaultValue,
  rollMoment: "OnCast",
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setValue":
      newState = { ...state, value: action.payload };
      break;
    case "setRollMoment":
      newState = { ...state, rollMoment: action.payload };
      break;
    default:
      newState = state;
      break;
  }
  console.log(newState);
  return newState;
};

export default function InitiativeEffectForm({
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
