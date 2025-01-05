import { useContext, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Dropdown from "../../../ui/forms/Dropdown";

import { movementCost, movementCostsDropdown } from "../movementCosts";
import { EditModeContext } from "../../../context/EditModeContext";

export type Effect = {
  effectType: {
    movementCostEffect: movementCost;
  };
};

type Action = {
  type: "setMovementCost";
  payload: movementCost;
};

export const initialState: Effect = {
  effectType: {
    movementCostEffect: "High",
  },
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setMovementCost":
      newState = {
        ...state,
        effectType: {
          ...state.effectType,
          movementCostEffect: action.payload as movementCost,
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

export default function MovementCostEffectForm({
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
      <FormRowVertical label="Movement cost">
        <Dropdown
          disabled={disableUpdate}
          chosenValue={state.effectType.movementCostEffect}
          setChosenValue={(e) =>
            dispatch({ type: "setMovementCost", payload: e as movementCost })
          }
          valuesList={movementCostsDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
