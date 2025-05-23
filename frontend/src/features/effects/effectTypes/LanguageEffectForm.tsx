import { useContext, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import { useItemFamiliesForEffect } from "../hooks/useItemFamiliesForEffect";
import Spinner from "../../../ui/interactive/Spinner";
import { EditModeContext } from "../../../context/EditModeContext";
import { EffectContext } from "../contexts/BlueprintOrInstanceContext";
import { useLanguages } from "../hooks/useLanguages";



export type Effect = {
  effectType: {
  };
  languageId: number | null;
};

type Action = {
  type: "setLanguage";
  payload: number;
};

export const initialState: Effect = {
  effectType: {
  },
  languageId: null,
};

export default function LanguageEffectForm({
  onChange,
  effect,
}: {
  onChange: (updatedState: Effect) => void;
  effect: Effect;
}) {
  const effectContext = useContext(EffectContext);
  const { isLoading, languages, error } =
    useLanguages(effectContext);
  const localLanguages = languages?.map((x) => {
    return { id: x.id, name: x.name };
  });
  const effectReducer = (state: Effect, action: Action): Effect => {
    let newState: Effect;
    switch (action.type) {
      case "setLanguage":
        newState = {
          ...state,
          languageId: Number(action.payload),
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

  const languagesDropdown = localLanguages
    ? localLanguages.map((x) => {
        return { value: x.id!.toString(), label: x.name };
      })
    : [];
  const { editMode } = useContext(EditModeContext);
  const disableUpdate = !editMode;
  if (isLoading) return <Spinner />;
  if (error) return <>Error</>;
  return (
    <Box>
      <FormRowVertical label="Language">
        <Dropdown
          disabled={
            disableUpdate
          }
          chosenValue={state.languageId?.toString() ?? null}
          setChosenValue={(e) =>
            dispatch({ type: "setLanguage", payload: Number(e) })
          }
          valuesList={languagesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
