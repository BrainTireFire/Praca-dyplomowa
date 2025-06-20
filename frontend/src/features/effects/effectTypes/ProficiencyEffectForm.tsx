import { useContext, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import { useItemFamiliesForEffect } from "../hooks/useItemFamiliesForEffect";
import Spinner from "../../../ui/interactive/Spinner";
import { EditModeContext } from "../../../context/EditModeContext";
import { EffectContext } from "../contexts/BlueprintOrInstanceContext";

const proficiencyEffects = ["ItemType", "SpecificItemFamily"] as const;

type proficiencyEffect = (typeof proficiencyEffects)[number];

const itemTypes = [
  "Item",
  // "Tool", TODO
  "Clothing",
  "LightArmor",
  "MediumArmor",
  "HeavyArmor",
  "Shield",
  "SimpleRangedWeapon",
  "SimpleMeleeWeapon",
  "MartialRangedWeapon",
  "MartialMeleeWeapon",
] as const;

type itemType = (typeof itemTypes)[number];

export type Effect = {
  effectType: {
    proficiencyEffect: proficiencyEffect;
    itemType: itemType;
  };
  grantsProficiencyInItemFamilyId: number | null;
};

type Action = {
  type: "setProficiencyEffect" | "setItemType" | "setItemFamily";
  payload: proficiencyEffect | itemType | number;
};

export const initialState: Effect = {
  effectType: {
    proficiencyEffect: "ItemType",
    itemType: "Clothing",
  },
  grantsProficiencyInItemFamilyId: null,
};

export default function ProficiencyEffectForm({
  onChange,
  effect,
}: {
  onChange: (updatedState: Effect) => void;
  effect: Effect;
}) {
  const effectContext = useContext(EffectContext);
  const { isLoading, itemFamilies, error } =
    useItemFamiliesForEffect(effectContext);
  const localItemFamilies = itemFamilies?.map((x) => {
    return { id: x.id, name: x.name };
  });
  const effectReducer = (state: Effect, action: Action): Effect => {
    let newState: Effect;
    switch (action.type) {
      case "setProficiencyEffect":
        newState = {
          ...state,
          effectType: {
            ...state.effectType,
            proficiencyEffect: action.payload as proficiencyEffect,
          },
        };
        break;
      case "setItemType":
        newState = {
          ...state,
          effectType: {
            ...state.effectType,
            itemType: action.payload as itemType,
          },
        };
        break;
      case "setItemFamily":
        newState = {
          ...state,
          grantsProficiencyInItemFamilyId: Number(action.payload),
        };
        break;
      default:
        newState = state;
        break;
    }

    return newState;
  };
  const [state, dispatch] = useReducer(effectReducer, effect);
  useEffect(() => {
    onChange(state);
  }, [state, onChange]);

  const itemFamiliesDropdown = localItemFamilies
    ? localItemFamilies.map((x) => {
        return { value: x.id!.toString(), label: x.name };
      })
    : [];
  const { editMode } = useContext(EditModeContext);
  const disableUpdate = !editMode;
  if (isLoading) return <Spinner />;
  if (error) return <>Error</>;
  return (
    <Box>
      <RadioGroup
        disabled={disableUpdate}
        values={[
          { label: "Item type", value: "ItemType" },
          { label: "Specific item family", value: "SpecificItemFamily" },
        ]}
        label="Proficiency"
        name="proficiency"
        onChange={(x) =>
          dispatch({
            type: "setProficiencyEffect",
            payload: x as proficiencyEffect,
          })
        }
        currentValue={state.effectType.proficiencyEffect}
      ></RadioGroup>
      <RadioGroup
        disabled={
          state.effectType.proficiencyEffect !== "ItemType" || disableUpdate
        }
        values={[
          { label: "Item", value: "Item" },
          // { label: "Tool", value: "Tool" }, TODO
          { label: "Clothing", value: "Clothing" },
          { label: "Light armor", value: "LightArmor" },
          { label: "Medium armor", value: "MediumArmor" },
          { label: "Heavy armor", value: "HeavyArmor" },
          { label: "Shield", value: "Shield" },
          { label: "Simple ranged weapons", value: "SimpleRangedWeapon" },
          { label: "Simple melee weapons", value: "SimpleMeleeWeapon" },
          { label: "Martial ranged weapons", value: "MartialRangedWeapon" },
          { label: "Martial melee weapons", value: "MartialMeleeWeapon" },
        ]}
        label="Item type"
        name="Item type"
        onChange={(x) =>
          dispatch({
            type: "setItemType",
            payload: x as proficiencyEffect,
          })
        }
        currentValue={state.effectType.itemType}
      ></RadioGroup>
      <FormRowVertical label="Item family">
        <Dropdown
          disabled={
            state.effectType.proficiencyEffect !== "SpecificItemFamily" ||
            disableUpdate
          }
          chosenValue={
            state.grantsProficiencyInItemFamilyId?.toString() ?? null
          }
          setChosenValue={(e) =>
            dispatch({ type: "setItemFamily", payload: Number(e) })
          }
          valuesList={itemFamiliesDropdown}
        ></Dropdown>
      </FormRowVertical>
    </Box>
  );
}
