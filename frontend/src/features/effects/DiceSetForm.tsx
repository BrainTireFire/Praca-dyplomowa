import { useEffect, useReducer, useState } from "react";
import { DiceSet, DiceSetDefaultValue } from "../../models/diceset";
import Box from "../../ui/containers/Box";
import FormRowVertical from "../../ui/forms/FormRowVertical";
import Input from "../../ui/forms/Input";
import { abilities, AbilitiesLabelMap } from "./abilities";
import { skills, SkillsLabelMap } from "./skills";
import { ReusableTable } from "../../ui/containers/ReusableTable";
import styled, { css } from "styled-components";
import { AdditionalValueForm } from "./AdditionalValueForm";
import Button from "../../ui/interactive/Button";

export type DiceSetExtended = DiceSet & { additionalValues: AdditionalValue[] };

export const DiceSetExtendedDefaultValue = {
  ...DiceSetDefaultValue,
  additionalValues: [],
};

const HorizontalContainer = styled.div`
  display: flex;
  flex-direction: row;
`;

export type AdditionalValue = {
  Id: number | null; // Id in database
  AdditionalValueType: (typeof AdditionalValueTypes)[number];
  LevelsInClassId: number;
  ClassName: string;
  Ability: (typeof abilities)[number];
  Skill: (typeof skills)[number];
};

const initialAdditionalValue: AdditionalValue = {
  Id: null,
  AdditionalValueType: "LevelsInClass",
  LevelsInClassId: 0,
  ClassName: "",
  Ability: "strength",
  Skill: "acrobatics",
};

export const AdditionalValueTypes = [
  "LevelsInClass",
  "TotalLevel",
  "AbilityScoreModifier",
  "SkillBonus",
] as const;

export const AdditionalValueTypeLabelMap = {
  LevelsInClass: "Levels in class",
  TotalLevel: "Total level",
  AbilityScoreModifier: "Ability score modifier",
  SkillBonus: "Skill bonus",
};
type Action =
  | DiceAction
  | PostAdditionalValueAction
  | UpdateAdditionalValueAction
  | RemoveAdditionalValueAction;

type DiceAction = {
  type:
    | "setD100"
    | "setD20"
    | "setD12"
    | "setD10"
    | "setD8"
    | "setD6"
    | "setD4"
    | "setFlat";
  payload: number;
};

type UpdateAdditionalValueAction = {
  type: "updateAdditionalValue";
  index: number;
  payload: AdditionalValue;
};
type PostAdditionalValueAction = {
  type: "postAdditionalValue";
  payload: AdditionalValue;
};
type RemoveAdditionalValueAction = {
  type: "removeAdditionalValue";
  index: number;
};

const initialState: DiceSetExtended = DiceSetExtendedDefaultValue;

export function DiceSetForm({
  onChange,
}: {
  onChange: (updatedState: DiceSetExtended) => void;
}) {
  const [selectedAdditionalValueIndex, setSelectedAdditionalValueIndex] =
    useState<number | null>(null);
  const effectReducer = (
    state: DiceSetExtended,
    action: Action
  ): DiceSetExtended => {
    let newState: DiceSetExtended = DiceSetExtendedDefaultValue;
    switch (action.type) {
      case "setD100":
        newState = { ...state, d100: action.payload };
        break;
      case "setD20":
        newState = { ...state, d20: action.payload };
        break;
      case "setD12":
        newState = { ...state, d12: action.payload };
        break;
      case "setD10":
        newState = { ...state, d10: action.payload };
        break;
      case "setD8":
        newState = { ...state, d8: action.payload };
        break;
      case "setD6":
        newState = { ...state, d6: action.payload };
        break;
      case "setD4":
        newState = { ...state, d4: action.payload };
        break;
      case "setFlat":
        newState = { ...state, flat: action.payload };
        break;
      case "updateAdditionalValue":
        newState = {
          ...state,
          additionalValues: state.additionalValues.map((item, index) =>
            index === action.index ? action.payload : item
          ),
        };
        break;
      case "postAdditionalValue":
        let newAdditionalValues1 = [
          ...state.additionalValues,
          (action as PostAdditionalValueAction).payload,
        ].map((item) => {
          return item;
        });
        newState = {
          ...state,
          additionalValues: newAdditionalValues1,
        };
        break;
      case "removeAdditionalValue":
        let newAdditionalValues2 = [
          ...state.additionalValues.slice(
            0,
            (action as RemoveAdditionalValueAction).index
          ),
          ...state.additionalValues.slice(
            (action as RemoveAdditionalValueAction).index + 1
          ),
        ].map((item) => {
          return item;
        });
        newState = {
          ...state,
          additionalValues: newAdditionalValues2,
        };
        setSelectedAdditionalValueIndex(null);
        break;
      default:
        newState = state;
        break;
    }
    console.log(newState);
    return newState;
  };
  const [state, dispatch] = useReducer(effectReducer, initialState);
  useEffect(() => {
    onChange(state);
  }, [state, onChange]);
  const selectedAdditionalValue = state.additionalValues.find(
    (_value, index) => index === selectedAdditionalValueIndex
  );
  const selectRow = (row: any) => {
    setSelectedAdditionalValueIndex(row.id);
  };
  return (
    <Box>
      <HorizontalContainer>
        <FormRowVertical
          label="d100"
          customStyles={css`
            max-width: 14%;
            width: 7rem;
            min-width: 5rem;
          `}
        >
          <Input
            type="number"
            value={state.d100}
            onChange={(e) =>
              dispatch({ type: "setD100", payload: Number(e.target.value) })
            }
          ></Input>
        </FormRowVertical>
        <FormRowVertical
          label="d20"
          customStyles={css`
            max-width: 14%;
            width: 7rem;
            min-width: 5rem;
          `}
        >
          <Input
            type="number"
            value={state.d20}
            onChange={(e) =>
              dispatch({ type: "setD20", payload: Number(e.target.value) })
            }
          ></Input>
        </FormRowVertical>
        <FormRowVertical
          label="d12"
          customStyles={css`
            max-width: 14%;
            width: 7rem;
            min-width: 5rem;
          `}
        >
          <Input
            type="number"
            value={state.d12}
            onChange={(e) =>
              dispatch({ type: "setD12", payload: Number(e.target.value) })
            }
          ></Input>
        </FormRowVertical>
        <FormRowVertical
          label="d10"
          customStyles={css`
            max-width: 14%;
            width: 7rem;
            min-width: 5rem;
          `}
        >
          <Input
            type="number"
            value={state.d10}
            onChange={(e) =>
              dispatch({ type: "setD10", payload: Number(e.target.value) })
            }
          ></Input>
        </FormRowVertical>
        <FormRowVertical
          label="d8"
          customStyles={css`
            max-width: 14%;
            width: 7rem;
            min-width: 5rem;
          `}
        >
          <Input
            type="number"
            value={state.d8}
            onChange={(e) =>
              dispatch({ type: "setD8", payload: Number(e.target.value) })
            }
          ></Input>
        </FormRowVertical>
        <FormRowVertical
          label="d6"
          customStyles={css`
            max-width: 14%;
            width: 7rem;
            min-width: 5rem;
          `}
        >
          <Input
            type="number"
            value={state.d6}
            onChange={(e) =>
              dispatch({ type: "setD6", payload: Number(e.target.value) })
            }
          ></Input>
        </FormRowVertical>
        <FormRowVertical
          label="d4"
          customStyles={css`
            max-width: 14%;
            width: 7rem;
            min-width: 5rem;
          `}
        >
          <Input
            type="number"
            value={state.d4}
            onChange={(e) =>
              dispatch({ type: "setD4", payload: Number(e.target.value) })
            }
          ></Input>
        </FormRowVertical>
      </HorizontalContainer>
      <Button
        onClick={() =>
          dispatch({
            type: "postAdditionalValue",
            payload: { ...initialAdditionalValue },
          })
        }
        customStyles={css`
          margin: 5px;
        `}
      >
        Add additional value
      </Button>
      <Button
        onClick={() =>
          dispatch({
            type: "removeAdditionalValue",
            index: selectedAdditionalValueIndex as number,
          })
        }
        disabled={selectedAdditionalValueIndex == null}
        customStyles={css`
          margin: 5px;
        `}
      >
        Remove selected additional value
      </Button>
      <ReusableTable
        tableRowsColomns={{
          Type: "AdditionalValueType",
          Value: "Value",
        }}
        data={state.additionalValues.map((additionalValue, index) => {
          let value: string | null;

          // Determine the value based on AdditionalValueType
          switch (additionalValue.AdditionalValueType) {
            case "LevelsInClass":
              value = additionalValue.ClassName;
              break;
            case "TotalLevel":
              value = additionalValue.ClassName;
              break;
            case "AbilityScoreModifier":
              value = AbilitiesLabelMap[additionalValue.Ability];
              break;
            case "SkillBonus":
              value = SkillsLabelMap[additionalValue.Skill];
              break;
            default:
              value = null; // Handle unexpected types
          }

          return {
            id: index,
            AdditionalValueType:
              AdditionalValueTypeLabelMap[additionalValue.AdditionalValueType],
            Value: value,
          };
        })}
        isSelectable={true}
        onSelect={selectRow}
      ></ReusableTable>
      {selectedAdditionalValue ? (
        <AdditionalValueForm
          value={selectedAdditionalValue}
          onChange={(x) => {
            dispatch({
              type: "updateAdditionalValue",
              index: selectedAdditionalValueIndex as number,
              payload: x,
            });
          }}
        ></AdditionalValueForm>
      ) : null}
    </Box>
  );
}