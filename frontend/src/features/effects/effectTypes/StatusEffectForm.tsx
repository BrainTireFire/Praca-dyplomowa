import { useContext, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Dropdown from "../../../ui/forms/Dropdown";
import { statusEffect, statusEffectDropdown } from "../statusEffects";
import { EditModeContext } from "../../../context/EditModeContext";

export type Effect = {
  effectType: {
    statusEffect: statusEffect;
  };
};

type Action = {
  type: "setStatusEffect";
  payload: statusEffect;
};

export const initialState: Effect = {
  effectType: {
    statusEffect: "Blinded",
  },
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setStatusEffect":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          statusEffect: action.payload as statusEffect,
        },
      };
      break;
    default:
      newState = state;
      break;
  }

  return newState;
};

export default function StatusEffectForm({
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

  const { editMode } = useContext(EditModeContext);
  const disableUpdate = !editMode;
  return (
    <Box>
      <FormRowVertical label="Status effect">
        <Dropdown
          disabled={disableUpdate}
          chosenValue={state.effectType.statusEffect}
          setChosenValue={(e) =>
            dispatch({ type: "setStatusEffect", payload: e as statusEffect })
          }
          valuesList={statusEffectDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
