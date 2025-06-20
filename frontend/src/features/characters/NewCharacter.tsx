import { useCallback, useReducer } from "react";
import Box from "../../ui/containers/Box";
import Input from "../../ui/forms/Input";
import Button from "../../ui/interactive/Button";
import Dropdown from "../../ui/forms/Dropdown";
import { useRaces } from "./hooks/useRaces";
import { useClasses } from "./hooks/useClass";
import Spinner from "../../ui/interactive/Spinner";
import { CharacterInsertDto } from "../../models/character";
import { useCreateCharacter } from "./hooks/useCreateCharacter";
import toast from "react-hot-toast";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import DisplayBox, { DisplayBoxContent } from "./DisplayBox";
import { FormRow, FormRow2 } from "../../ui/forms/FormRow";
import { DiceSetString } from "../../models/diceset";

const initialState: CharacterInsertDto = {
  name: "",
  startingClassId: null,
  raceId: null,
  strength: 1,
  dexterity: 1,
  constitution: 1,
  intelligence: 1,
  wisdom: 1,
  charisma: 1,
  isNpc: false,
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
    default:
      newState = state;
      break;
  }

  return newState;
};
const abilityErrorMessage = "Value must be in range of 1-20";

function NewCharacter({ onCloseModal }: { onCloseModal: () => void }) {
  const [state, dispatch] = useReducer(characterReducer, initialState);

  //query
  const { isLoading: isLoadingRaces, races, error: errorRaces } = useRaces();
  const {
    isLoading: isLoadingClasses,
    classes,
    error: errorClasses,
  } = useClasses();

  //mutation
  const { createCharacter, isPending } = useCreateCharacter(() => {
    toast.success("Character created");
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
  const classList = classes
    ? classes.map((characterClass) => ({
        value: characterClass.id.toString(),
        label: characterClass.name,
      }))
    : [];
  const isInBounds = useCallback(
    (ability: number) => ability <= 0 || ability > 20,
    []
  );

  const InvalidName =
    !state.name || state.name.length <= 0 || state.name.length > 40;
  const disableSave = useCallback(() => {
    return (
      InvalidName ||
      !state.raceId ||
      !state.startingClassId ||
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
    InvalidName,
    state.raceId,
    state.startingClassId,
    state.strength,
    state.wisdom,
  ]);

  if (errorRaces || errorClasses) {
    return "Error";
  }
  if (isLoadingRaces || isLoadingClasses || isPending) {
    return <Spinner />;
  }

  return (
    <>
      <Box>
        <FormRow
          label="Character name"
          error={
            InvalidName
              ? "Name must be filled in and cannot exceed 40 signs"
              : undefined
          }
        >
          <Input
            value={state.name}
            onChange={(e) =>
              dispatch({ type: "setName", value: e.target.value })
            }
          ></Input>
        </FormRow>
        <FormRow
          label="Starting class"
          error={!state.startingClassId ? "Select class" : undefined}
        >
          <Dropdown
            chosenValue={state.startingClassId?.toString() || null}
            setChosenValue={(e) =>
              dispatch({ type: "setClass", value: Number(e) })
            }
            valuesList={classList}
          ></Dropdown>
        </FormRow>
        <FormRow2 label="Chosen class details">
          <>
            <DisplayBox label="Main ability">
              <DisplayBoxContent>
                {classes.find((x) => x.id === state.startingClassId)
                  ?.mainAbility ?? "-"}
              </DisplayBoxContent>
            </DisplayBox>
            <DisplayBox label="Initial hitpoints">
              <DisplayBoxContent>
                {classes.find((x) => x.id === state.startingClassId)
                  ?.hitpoints ?? "-"}
              </DisplayBoxContent>
            </DisplayBox>
            <DisplayBox label="Hitdice">
              <DisplayBoxContent>
                {DiceSetString(
                  classes.find((x) => x.id === state.startingClassId)?.hitDice
                )}
              </DisplayBoxContent>
            </DisplayBox>
          </>
        </FormRow2>
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
            onClick={() => createCharacter(state)}
          >
            Create character
          </Button>
        </FormRowVertical>
      </Box>
    </>
  );
}

export default NewCharacter;

NewCharacter.defaultProps = {
  onCloseModal: () => {},
};
