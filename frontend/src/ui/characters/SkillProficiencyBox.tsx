import { useContext } from "react";
import styled from "styled-components";
import { Skill } from "../../models/skill";
import toast from "react-hot-toast";
import { RollDto } from "../../services/apiCharacters";
import { CharacterIdContext } from "../../features/characters/contexts/CharacterIdContext";
import { useRollSkillDice } from "./hooks/useRollSkillDice";
import { skill } from "../../features/effects/skills";


const InputBox = styled.div`
  display: flex;
  justify-content: flex-start;
  gap: 0.7rem;
  padding: 0.5rem;
  margin-bottom: 0.2rem;
  align-items: center;
  width: auto;
  height: 2rem;
  background-color: rgba(var(--color-secondary-background-rgb), 0.05);
  border: 1px solid var(--color-border);
  white-space: nowrap;
  /* overflow: hidden; */
  font-size: 1rem;
  border-radius: var(--border-radius-lg);
  cursor: pointer;
`;

const Radio = styled.input`
  width: 10px;
  height: 10px;
  appearance: none;
  border: 2px solid var(--color-button-primary);
  border-radius: 4px;
  outline: none;

  &:checked {
    background-color: var(--color-button-primary);
    border-color: var(--color-button-primary);
  }
`;

export default function SkillProficiencyBox({
  item,
}: {
  item: Skill;
}) {
  let onSuccess = (roll: RollDto) => {
    if(roll.executed) toast.success(`Roll outcome: ${roll.roll + roll.modifier}`);
    else toast.success(`Requested roll with DMs assistance`);
  }
  const { characterId } = useContext(CharacterIdContext);
  const {rollSkillDice} = useRollSkillDice(onSuccess, characterId!, item.name as skill);
  return (
    <InputBox key={item.name} onClick={() => rollSkillDice()}>
      <Radio
        //   label="Option 1"
        type="radio"
        checked={item.proficient}
        //   checked={selectedOption === "option1"}
        //   onChange={handleRadioChange}
      />
      <div>{item.value}</div>
      {"ability" in item && <div>{item.ability}</div>}
      <div>{item.name}</div>
    </InputBox>
  );
}
