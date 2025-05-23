import styled from "styled-components";
import { Attribute } from "../../models/attribute";
import { useRollAbilityDice } from "./hooks/useRollAbilityDice";
import { useContext } from "react";
import { CharacterIdContext } from "../../features/characters/contexts/CharacterIdContext";

import toast from "react-hot-toast";
import { RollDto } from "../../services/apiCharacters";

const StyledDropdown = styled.div`
  display: flex;
  align-items: center;
  flex-direction: column;
  gap: 0.2rem;
`;

const Box = styled.div`
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  font-size: 3rem;
  padding-bottom: 0rem;

  border: 1px solid var(--color-border);
  border-radius: var(--border-radius-sm);
  width: 5rem;
  height: 5rem;
  cursor: pointer;
`;

const Header = styled.div`
  font-size: 1.2rem;
  font-weight: 500;
`;

const Circle = styled.div`
  position: absolute;
  bottom: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 2.5rem;
  height: 2.5rem;
  border: 1px solid var(--color-border);
  border-radius: 100%;
  font-size: 1.5rem;
`;

const Text = styled.p`
  /* border: 1px solid var(--color-border); */
  /* background-color: transparent; */
  position: absolute;
  top: 0;
  justify-content: center;
  align-items: center;
  line-height: 1.2;

  font-size: 2rem;
`;

function AttributeBox({ attribute }: { attribute: Attribute }) {
  let onSuccess = (roll: RollDto) => {
    if(roll.executed) toast.success(`Roll outcome: ${roll.roll + roll.modifier}`);
    else toast.success(`Requested roll with DMs assistance`);
  }
  const { characterId } = useContext(CharacterIdContext);
  const {rollAbilityDice} = useRollAbilityDice(onSuccess, characterId!, attribute.name);
  return (
    <StyledDropdown>
      <Header>{attribute.name}</Header>
      <Box onClick={() => rollAbilityDice()}>
        <Text>{attribute.value}</Text>
        <Circle>
          {attribute.modifier >= 0
            ? `+${attribute.modifier}`
            : `${attribute.modifier}`}
        </Circle>
      </Box>
    </StyledDropdown>
  );
}

// AttributeBox.Header = Header;
// AttributeBox.Box = Box;
// AttributeBox.Circle = Circle;
// AttributeBox.Input = Input;
// AttributeBox.Text = Text;

export default AttributeBox;
