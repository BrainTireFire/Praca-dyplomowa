import styled from "styled-components";
import Input from "../../ui/forms/Input";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import Box from "../../ui/containers/Box";
import { useRef, useState } from "react";

const diceData = [
  {
    sides: 4,
    name: "d4",
  },
  {
    sides: 6,
    name: "d6",
  },
  {
    sides: 8,
    name: "d8",
  },
  {
    sides: 10,
    name: "d10",
  },
  {
    sides: 12,
    name: "d12",
  },
  {
    sides: 20,
    name: "d20",
  },
];

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 12px;
`;

interface DiceResult {
  dice: string;
  rolls: number[];
}

function BatchRollModal() {
  const [diceCounts, setDiceCounts] = useState(diceData.map(() => 0));
  const [results, setResults] = useState<DiceResult[]>([]);
  const showBox = useRef(false);

  const handleChange = (index, value) => {
    const newCounts = [...diceCounts];
    newCounts[index] = isNaN(value) ? 0 : value < 1 ? 0 : value;
    setDiceCounts(newCounts);
  };

  const handleRoll = () => {
    if (diceCounts.reduce((acc, value) => acc + value, 0) === 0) return;
    showBox.current = true;

    const newResults = diceData.map((dice, index) => {
      const rolls = [];
      for (let i = 0; i < diceCounts[index]; i++) {
        rolls.push(Math.floor(Math.random() * dice.sides + 1));
      }
      return { dice: dice.name, rolls };
    });

    const filteredResults = newResults.filter((e) => e.rolls.length !== 0);
    setResults(filteredResults);
    showBox = true;
  };

  return (
    <Container>
      {diceData.map((e, index) => (
        <div style={{ gap: "1px" }}>
          <Heading as="h1">Dice {e.name}</Heading>
          <Input
            placeholder="Select number of dice"
            style={{ width: "200px" }}
            onChange={(e) => handleChange(index, Number(e.target.value))}
          ></Input>
        </div>
      ))}
      <Button onClick={handleRoll}>Roll the dice</Button>

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
