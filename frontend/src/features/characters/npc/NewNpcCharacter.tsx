import { useReducer } from "react";
import Box from "../../../ui/containers/Box";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Dropdown from "../../../ui/forms/Dropdown";
import { useRaces } from ".././hooks/useRaces";
import { useClasses } from ".././hooks/useClass";
import Spinner from "../../../ui/interactive/Spinner";
import { CharacterInsertDto } from "../../../models/character";
import toast from "react-hot-toast";
import { useCreateNpcCharacter } from "../hooks/useCreateNpcCharacter";
import FormRow from "../../../ui/forms/FormRow";
const initialState = {
  name: "",
  startingClassId: 0,
  raceId: 0,
  strength: 0,
  dexterity: 0,
  constitution: 0,
  intelligence: 0,
  wisdom: 0,
  charisma: 0,
  isNpc: true,
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
type BooleanActions = {
  type: "setIsNpc";
  value: boolean;
};
type Actions = NumericActions | StringActions | BooleanActions;
const characterReducer = (
  state: typeof initialState,
  action: Actions
): CharacterInsertDto => {
  let newState;
  switch (action.type) {
    case "setName":
      newState = { ...state, name: action.value };
      break;
    case "setClass":
      newState = { ...state, startingClassId: action.value };
      break;
    case "setRace":
      newState = { ...state, raceId: action.value };
      break;
    case "setStr":
      newState = { ...state, strength: action.value };
      break;
    case "setDex":
      newState = { ...state, dexterity: action.value };
      break;
    case "setCon":
      newState = { ...state, constitution: action.value };
      break;
    case "setInt":
      newState = { ...state, intelligence: action.value };
      break;
    case "setWis":
      newState = { ...state, wisdom: action.value };
      break;
    case "setCha":
      newState = { ...state, charisma: action.value };
      break;
    case "setIsNpc":
      newState = { ...state, isNpc: action.value };
      break;
    default:
      newState = state;
      break;
  }
  return newState;
};
function NewNpcCharacter({ onCloseModal }) {
  const [state, dispatch] = useReducer(characterReducer, initialState);
  //query
  const { isLoading: isLoadingRaces, races, error: errorRaces } = useRaces();
  const {
    isLoading: isLoadingClasses,
    classes,
    error: errorClasses,
  } = useClasses();
  //mutation
  const { createNpcCharacter, isPending } = useCreateNpcCharacter(() => {
    toast.success("NpcCharacter created");
    onCloseModal();
    return;
  });
  //derived state
  const raceList = races
    ? races.map((race) => ({
        value: race.id,
        label: race.name,
      }))
    : [];
  const classList = classes
    ? classes.map((characterClass) => ({
        value: characterClass.id,
        label: characterClass.name,
      }))
    : [];
  if (errorRaces || errorClasses) {
    return "Error";
  }
  if (isLoadingRaces || isLoadingClasses || isPending) {
    return <Spinner />;
  }
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
          <Dropdown
            chosenValue={state.startingClassId.toString()}
            setChosenValue={(e) =>
              dispatch({ type: "setClass", value: Number(e) })
            }
            valuesList={classList}
          ></Dropdown>
        </FormRow>
        <FormRow label="Race">
          <Dropdown
            chosenValue={state.raceId.toString()}
            setChosenValue={(e) =>
              dispatch({ type: "setRace", value: Number(e) })
            }
            valuesList={raceList}
          ></Dropdown>
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
        <Button onClick={() => createNpcCharacter(state)}>
          Create Npc character
        </Button>
      </Box>
    </>
  );
}
export default NewNpcCharacter;
