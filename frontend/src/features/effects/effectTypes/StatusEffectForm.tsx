import { useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Dropdown from "../../../ui/forms/Dropdown";
import { statusEffect, statusEffectDropdown } from "../statusEffects";

export type Effect = {
  statusEffect: statusEffect;
};

type Action = {
  type: "setStatusEffect";
  payload: statusEffect;
};

export const initialState: Effect = {
  statusEffect: "blinded",
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setStatusEffect":
      newState = { ...state, statusEffect: action.payload as statusEffect };
      break;
    default:
      newState = state;
      break;
  }
  console.log(newState);
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

  return (
    <Box>
      <FormRowVertical label="Status effect">
        <Dropdown
          chosenValue={state.statusEffect}
          setChosenValue={(e) =>
            dispatch({ type: "setStatusEffect", payload: e as statusEffect })
          }
          valuesList={statusEffectDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}