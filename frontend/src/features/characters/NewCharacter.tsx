import { useReducer } from "react";
import Box from "../../ui/containers/Box";
import FormRow from "../../ui/forms/FormRow";
import Input from "../../ui/forms/Input";
import Button from "../../ui/interactive/Button";

const initialState = {
  name: "",
  class: 0,
  race: 0,
  strength: 0,
  dexterity: 0,
  constitution: 0,
  intelligence: 0,
  wisdom: 0,
  charisma: 0,
};

type NumericActions = {
  type:
    | "setClass"
    | "setRace"
    | "setStr"
    | "setDex"
    | "setCon"
    | "setInt"
    | "setWis"
    | "setCha";
  value: number;
};

type StringActions = {
  type: "setName";
  value: string;
};

type Actions = NumericActions | StringActions;

const characterReducer = (
  state: typeof initialState,
  action: Actions
): typeof initialState => {
  switch (action.type) {
    case "setName":
      return { ...state, name: action.value };
    case "setClass":
      return { ...state, class: action.value };
    case "setRace":
      return { ...state, race: action.value };
    case "setStr":
      return { ...state, strength: action.value };
    case "setDex":
      return { ...state, dexterity: action.value };
    case "setCon":
      return { ...state, constitution: action.value };
    case "setInt":
      return { ...state, intelligence: action.value };
    case "setWis":
      return { ...state, wisdom: action.value };
    case "setCha":
      return { ...state, charisma: action.value };
    default:
      return state;
  }
};

function NewCharacter() {
  const [state, dispatch] = useReducer(characterReducer, initialState);

  return (
    <>
      <Box>
        <FormRow label="Character name">
          <Input
            value={state.name}
            onChange={(e) =>
              dispatch({ type: "setName", value: e.target.value })
            }
          ></Input>
        </FormRow>
        <FormRow label="Starting class">
          <Input
            type="number"
            value={state.class}
            onChange={(e) =>
              dispatch({ type: "setClass", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow label="Race">
          <Input
            type="number"
            value={state.race}
            onChange={(e) =>
              dispatch({ type: "setRace", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow label="Strength">
          <Input
            type="number"
            value={state.strength}
            onChange={(e) =>
              dispatch({ type: "setStr", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow label="Dexterity">
          <Input
            type="number"
            value={state.dexterity}
            onChange={(e) =>
              dispatch({ type: "setDex", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow label="Constitution">
          <Input
            type="number"
            value={state.constitution}
            onChange={(e) =>
              dispatch({ type: "setCon", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow label="Intelligence">
          <Input
            type="number"
            value={state.intelligence}
            onChange={(e) =>
              dispatch({ type: "setInt", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow label="Wisdom">
          <Input
            type="number"
            value={state.wisdom}
            onChange={(e) =>
              dispatch({ type: "setWis", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow label="Charisma">
          <Input
            type="number"
            value={state.charisma}
            onChange={(e) =>
              dispatch({ type: "setCha", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <Button>Create character</Button>
      </Box>
    </>
  );
}

export default NewCharacter;
