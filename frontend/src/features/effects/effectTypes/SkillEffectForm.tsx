import { useCallback, useEffect, useReducer } from "react";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import RadioGroup from "../../../ui/forms/RadioGroup";
import Dropdown from "../../../ui/forms/Dropdown";
import { skills, skillsDropdown } from "../skills";
import {
  DiceSetExtended,
  DiceSetExtendedDefaultValue,
  DiceSetForm,
} from "../DiceSetForm";
import styled from "styled-components";

export type Effect = {
  effectType: "bonus" | "rerollLowerThan" | "advantage";
  value: DiceSetExtended;
  skill: (typeof skills)[number];
};

type Action = {
  type: "setEffectType" | "setValue" | "setSkill";
  payload: any;
};

export const initialState: Effect = {
  effectType: "advantage",
  value: DiceSetExtendedDefaultValue,
  skill: "acrobatics",
};

const effectReducer = (state: Effect, action: Action): Effect => {
  let newState: Effect;
  switch (action.type) {
    case "setEffectType":
      newState = { ...state, effectType: action.payload };
      break;
    case "setValue":
      console.log("updated value in skill effect form");
      newState = { ...state, value: action.payload };
      break;
    case "setSkill":
      newState = { ...state, skill: action.payload };
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
  const [state, dispatch] = useReducer(effectReducer, effect);
  useEffect(() => {
    onChange(state);
  }, [state, onChange]);

  const handleValueFormStateUpdate = useCallback((e: DiceSetExtended) => {
    dispatch({ type: "setValue", payload: e });
  }, []);
  return (
    <Container>
      <Div1>
        <RadioGroup
          values={[
            { label: "Bonus", value: "bonus" },
            { label: "Advantage", value: "advantage" },
            { label: "Reroll lower than", value: "rerollLowerThan" },
          ]}
          label="Skill effect"
          name="skillEffect"
          onChange={(x) => dispatch({ type: "setEffectType", payload: x })}
          currentValue={state.effectType}
        ></RadioGroup>
      </Div1>
      <Div2>
        <FormRowVertical label="Skill">
          <Dropdown
            chosenValue={state.skill}
            setChosenValue={(e) => dispatch({ type: "setSkill", payload: e })}
            valuesList={skillsDropdown}
          ></Dropdown>
        </FormRowVertical>
      </Div2>
      <Div3>
        <DiceSetForm onChange={handleValueFormStateUpdate}></DiceSetForm>
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