import styled from "styled-components";
import Input from "../../../ui/forms/Input";
import Heading from "../../../ui/text/Heading";
import Button from "../../../ui/interactive/Button";
import Box from "../../../ui/containers/Box";
import { useEffect, useRef, useState } from "react";
import { useAllDice } from "../useDice";
import Spinner from "../../../ui/interactive/Spinner";
import { DiceSet } from "../../../models/diceset";

const Container = styled.div`
  display: grid;
  grid-template-columns: 5vw 9vw;
  align-items: center;
  align-content: center;
  gap: 8px;
`;

const initialDiceSet: DiceSet = {
  d100: 0,
  d20: 0,
  d12: 0,
  d10: 0,
  d8: 0,
  d6: 0,
  d4: 0,
  flat: 0,
};

interface DiceResult {
  dice: string;
  rolls: number[];
}

function BatchRollModal() {
  const [diceSet, setDiceSet] = useState<DiceSet>(initialDiceSet);
  // const [results, setResults] = useState<DiceResult[]>([]);
  const showBox = useRef(false);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement>,
    diceType: keyof DiceSet
  ) => {
    let value = parseInt(e.target.value);
    if (isNaN(value)) {
      value = 0;
    } else {
      value = value < 1 ? 0 : value;
    }

    setDiceSet((previous) => ({
      ...previous,
      [diceType]: value,
    }));
  };

  const handleRoll = () => {
    //   const filteredResults = newResults.filter((e) => e.rolls.length !== 0);
    //   setResults(filteredResults);
    //   showBox.current = true;
    // };
  };

  return (
    <Container>
      {Object.keys(initialDiceSet).map((diceType) => (
        <div key={diceType} style={{ display: "contents" }}>
          <Heading as="h3">Dice {diceType}</Heading>
          <Input
            type="number"
            placeholder="Select number"
            style={{ height: "80%" }}
            onChange={(e) => handleChange(e, diceType as keyof DiceSet)}
          ></Input>
        </div>
      ))}
      <Button style={{ gridColumn: "1/3" }} onClick={handleRoll}>
        Roll the dice
      </Button>

      {showBox.current && (
        <Box>
          {results.map((e, index) => (
            <div key={index}>
              Result of rolling {e.dice} is: [{e.rolls.join(", ")}] and the sum
              is {e.rolls.reduce((acc, value) => acc + value, 0)}
            </div>
          ))}
        </Box>
      )}
    </Container>
  );
}

export default BatchRollModal;
