import { useCallback, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRow from "../../../ui/forms/FormRow";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import Dropdown from "../../../ui/forms/Dropdown";
import { useRaces } from ".././hooks/useRaces";
import Spinner from "../../../ui/interactive/Spinner";
import { CharacterInsertDto } from "../../../models/character";
import toast from "react-hot-toast";
import { useCreateNpcCharacter } from "../hooks/useCreateNpcCharacter";
import FormRowVertical from "../../../ui/forms/FormRowVertical";

const initialState: CharacterInsertDto = {
  name: "",
  raceId: null,
  strength: 1,
  dexterity: 1,
  constitution: 1,
  intelligence: 1,
  wisdom: 1,
  charisma: 1,
  isNpc: true,
};

type NumericActions = {
  type:
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
): CharacterInsertDto => {
  let newState;
  switch (action.type) {
    case "setName":
      newState = { ...state, name: action.value };
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
    default:
      newState = state;
      break;
  }
  console.log(newState);
  return newState;
};
const abilityErrorMessage = "Value must be in range of 1-20";

function NewNpcCharacter({ onCloseModal }) {
  const [state, dispatch] = useReducer(characterReducer, initialState);

  //query
  const { isLoading: isLoadingRaces, races, error: errorRaces } = useRaces();

  //mutation
  const { createNpcCharacter, isPending } = useCreateNpcCharacter(() => {
    toast.success("NpcCharacter created");
    onCloseModal();
    return;
  });

  //derived state
  const raceList = races
    ? races.map((race) => ({
        value: race.id.toString(),
        label: race.name,
      }))
    : [];

  const isInBounds = useCallback(
    (ability: number) => ability <= 0 || ability > 20,
    []
  );
  const disableSave = useCallback(() => {
    return (
      !state.name ||
      !state.raceId ||
      isInBounds(state.charisma) ||
      isInBounds(state.wisdom) ||
      isInBounds(state.intelligence) ||
      isInBounds(state.constitution) ||
      isInBounds(state.dexterity) ||
      isInBounds(state.strength)
    );
  }, [
    isInBounds,
    state.charisma,
    state.constitution,
    state.dexterity,
    state.intelligence,
    state.name,
    state.raceId,
    state.strength,
    state.wisdom,
  ]);

  if (errorRaces) {
    return "Error";
  }
  if (isLoadingRaces || isPending) {
    return <Spinner />;
  }
  return (
    <>
      <Box>
        <FormRow
          label="Character name"
          error={!state.name ? "Fill name" : undefined}
        >
          <Input
            value={state.name}
            onChange={(e) =>
              dispatch({ type: "setName", value: e.target.value })
            }
          ></Input>
        </FormRow>
        <FormRow label="Race" error={!state.raceId ? "Select race" : undefined}>
          <Dropdown
            chosenValue={state.raceId?.toString() || null}
            setChosenValue={(e) =>
              dispatch({ type: "setRace", value: Number(e) })
            }
            valuesList={raceList}
          ></Dropdown>
        </FormRow>
        <FormRow
          label="Strength"
          error={isInBounds(state.strength) ? abilityErrorMessage : undefined}
        >
          <Input
            type="number"
            min={1}
            max={20}
            value={state.strength}
            onChange={(e) =>
              dispatch({ type: "setStr", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow
          label="Dexterity"
          error={isInBounds(state.dexterity) ? abilityErrorMessage : undefined}
        >
          <Input
            type="number"
            min={1}
            max={20}
            value={state.dexterity}
            onChange={(e) =>
              dispatch({ type: "setDex", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow
          label="Constitution"
          error={
            isInBounds(state.constitution) ? abilityErrorMessage : undefined
          }
        >
          <Input
            type="number"
            min={1}
            max={20}
            value={state.constitution}
            onChange={(e) =>
              dispatch({ type: "setCon", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow
          label="Intelligence"
          error={
            isInBounds(state.intelligence) ? abilityErrorMessage : undefined
          }
        >
          <Input
            type="number"
            min={1}
            max={20}
            value={state.intelligence}
            onChange={(e) =>
              dispatch({ type: "setInt", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow
          label="Wisdom"
          error={isInBounds(state.wisdom) ? abilityErrorMessage : undefined}
        >
          <Input
            type="number"
            min={1}
            max={20}
            value={state.wisdom}
            onChange={(e) =>
              dispatch({ type: "setWis", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRow
          label="Charisma"
          error={isInBounds(state.charisma) ? abilityErrorMessage : undefined}
        >
          <Input
            type="number"
            min={1}
            max={20}
            value={state.charisma}
            onChange={(e) =>
              dispatch({ type: "setCha", value: Number(e.target.value) })
            }
          ></Input>
        </FormRow>
        <FormRowVertical
          error={disableSave() ? "Fill above form before saving" : undefined}
        >
          <Button
            disabled={disableSave()}
            onClick={() => createNpcCharacter(state)}
          >
            Create Non Playable Character
          </Button>
        </FormRowVertical>
      </Box>
    </>
  );
}
export default NewNpcCharacter;
