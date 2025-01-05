import { useCallback, useContext, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import { skill, skillsDropdown } from "../skills";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";
import styled from "styled-components";
import { ValueEffect } from "../valueEffect";
import { rollMoment, rollMomentDropdown } from "../rollMoment";
import { EffectContext } from "../contexts/BlueprintOrInstanceContext";
import { EditModeContext } from "../../../context/EditModeContext";

export type Effect = ValueEffect & {
  effectType: {
    skillEffect: "bonus" | "rerollLowerThan" | "advantage";
    skillEffect_Skill: skill;
  };
};

type Action = {
  type: "setEffectType" | "setValue" | "setSkill" | "setRollMoment";
  payload: any;
};

export const initialState: Effect = {
  effectType: {
    skillEffect: "advantage",
    skillEffect_Skill: "Acrobatics",
  },
  value: DiceSetExtendedDefaultValue,
  rollMoment: "OnCast",
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setEffectType":
      newState = {
        ...state,
        effectType: { ...state.effectType, skillEffect: action.payload },
      };
      break;
    case "setValue":
      console.log("updated value in skill effect form");
      newState = { ...state, value: action.payload };
      break;
    case "setRollMoment":
      newState = { ...state, rollMoment: action.payload as rollMoment };
      break;
    case "setSkill":
      newState = {
        ...state,
        effectType: { ...state.effectType, skillEffect_Skill: action.payload },
      };
      break;
    default:
      newState = state;
      break;
  }
  console.log(newState);
  return newState;
};

export default function SkillEffectForm({
  onChange,
  effect,
}: {
  onChange: (updatedState: Effect) => void;
  effect: Effect;
}) {
  const effectContext = useContext(EffectContext);
  const [state, dispatch] = useReducer(effectReducer, effect);
  useEffect(() => {
    onChange(state);
  }, [state, onChange]);

  const handleValueFormStateUpdate = useCallback((e: DiceSetExtended) => {
    dispatch({ type: "setValue", payload: e });
  }, []);
  const { editMode } = useContext(EditModeContext);
  const disableUpdate = !editMode;
  return (
    <Container>
      <Div1>
        <RadioGroup
          disabled={disableUpdate}
          values={[
            { label: "Bonus", value: "bonus" },
            { label: "Advantage", value: "advantage" },
            { label: "Reroll lower than", value: "rerollLowerThan" },
          ]}
          label="Skill effect"
          name="skillEffect"
          onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
          currentValue={state.effectType.skillEffect}
        ></RadioGroup>
      </Div1>
      <Div2>
        <FormRowVertical label="Skill">
          <Dropdown
            disabled={disableUpdate}
            chosenValue={state.effectType.skillEffect_Skill}
            setChosenValue={(e) => dispatch({ type: "setSkill", payload: e })}
            valuesList={skillsDropdown}
          ></Dropdown>
        </FormRowVertical>
      </Div2>
      <Div3>
        {effectContext.effect === "Blueprint" && (
          <FormRowVertical label="Dice roll moment">
            <Dropdown
              disabled={disableUpdate}
              valuesList={rollMomentDropdown}
              chosenValue={state.rollMoment}
              setChosenValue={(e) =>
                dispatch({ type: "setRollMoment", payload: e })
              }
            ></Dropdown>
          </FormRowVertical>
        )}
        <FormRowVertical label="Value">
          <DiceSetForm
            disabled={disableUpdate}
            onChange={handleValueFormStateUpdate}
            diceSet={effect.value}
          ></DiceSetForm>
        </FormRowVertical>
      </Div3>
    </Container>
  );
}

const Container = styled(Box)`
  display: grid;
  grid-template-columns: auto auto;
  grid-template-rows: auto 1fr;
  gap: 10px;
`;

const Div1 = styled.div`
  grid-column: 1;
  grid-row: 1;
`;

const Div2 = styled.div`
  grid-column: 1;
  grid-row: 2;
`;

const Div3 = styled.div`
  grid-column: 2;
  grid-row: 1 / 3; // Start on row 1 and end on row 3 to cover both rows
`;
