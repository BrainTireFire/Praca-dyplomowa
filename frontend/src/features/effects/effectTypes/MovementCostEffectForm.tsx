import { useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Dropdown from "../../../ui/forms/Dropdown";

import { movementCost, movementCostsDropdown } from "../movementCosts";

export type Effect = {
  statusEffect: movementCost;
};

type Action = {
  type: "setMovementCost";
  payload: movementCost;
};

export const initialState: Effect = {
  statusEffect: "high",
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setMovementCost":
      newState = { ...state, statusEffect: action.payload as movementCost };
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

  return (
    <Box>
      <FormRowVertical label="Movement cost">
        <Dropdown
          chosenValue={state.statusEffect}
          setChosenValue={(e) =>
            dispatch({ type: "setMovementCost", payload: e as movementCost })
          }
          valuesList={movementCostsDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
