import { useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import { useItemFamilies } from "../hooks/useItemFamilies";
import Spinner from "../../../ui/interactive/Spinner";

const effectTypes = [
  "martialWeapons",
  "simpleWeapons",
  "shields",
  "specificItemFamily",
] as const;

type effectType = (typeof effectTypes)[number];

type itemFamily = {
  id: number;
  name: string;
};

export type Effect = {
  effectType: effectType;
  itemFamily: itemFamily | null;
};

type Action = {
  type: "setEffectType" | "setItemFamily";
  payload: effectType | number;
};

export const initialState: Effect = {
  effectType: "martialWeapons",
  itemFamily: null,
};

export default function ProficiencyEffectForm({
  onChange,
  effect,
}: {
  onChange: (updatedState: Effect) => void;
  effect: Effect;
}) {
  const { isLoading, itemFamilies, error } = useItemFamilies();
  const localItemFamilies = itemFamilies?.map((x) => {
    return { id: x.id, name: x.name };
  });
  const effectReducer = (state: Effect, action: Action): Effect => {
    let newState: Effect;
    switch (action.type) {
      case "setEffectType":
        newState = { ...state, effectType: action.payload as effectType };
        break;
      case "setItemFamily":
        newState = {
          ...state,
          itemFamily: localItemFamilies?.find((x) => x.id === action.payload)!,
        };
        break;
      default:
        newState = state;
        break;
    }
    console.log(newState);
    return newState;
  };
  const [state, dispatch] = useReducer(effectReducer, effect);
  useEffect(() => {
    onChange(state);
  }, [state, onChange]);

  const itemFamiliesDropdown = localItemFamilies
    ? localItemFamilies.map((x) => {
        return { value: x.id.toString(), label: x.name };
      })
    : [];
  if (isLoading) return <Spinner />;
  if (error) return <>Error</>;
  return (
    <Box>
      <RadioGroup
        values={[
          { label: "Martial weapons", value: "martialWeapons" },
          { label: "Simple weapons", value: "simpleWeapons" },
          { label: "Shields", value: "shields" },
          { label: "Specific item family", value: "specificItemFamily" },
        ]}
        label="Proficiency"
        name="proficiency"
        onChange={(x) =>
          dispatch({ type: "setEffectType", payload: x as effectType })
        }
        currentValue={state.effectType}
      ></RadioGroup>
      <FormRowVertical label="Item family">
        <Dropdown
          chosenValue={state.itemFamily ? state.itemFamily.id.toString() : ""}
          setChosenValue={(e) =>
            dispatch({ type: "setItemFamily", payload: Number(e) })
          }
          valuesList={itemFamiliesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
