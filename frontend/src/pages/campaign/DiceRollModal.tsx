import styled from "styled-components";
import Heading from "../../ui/text/Heading";
import { useState } from "react";
import Button from "../../ui/interactive/Button";
import Box from "../../ui/containers/Box";

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
  gap: 20px;
`;

function DiceRollModal() {
  const [dice, setDice] = useState(diceData[0]);
  const [result, setResult] = useState(0);

  const handleChange = (e) => {
    const selectedSides = Number(e.target.value);
    const selectedDice = diceData.find((dice) => dice.sides === selectedSides);
    setDice(selectedDice);
    setResult(0);
  };

  const handleClick = () => {
    const result = Math.floor(Math.random() * dice.sides + 1);
    setResult(result);
  };

  return (
    <>
      <Container>
        <Heading as="h2">Select Dice</Heading>
        <select style={{ borderRadius: "5px" }} onChange={handleChange}>
          {diceData.map((e) => (
            <option value={e.sides}>{e.name}</option>
          ))}
        </select>
        <Button onClick={handleClick}>Roll the dice</Button>

        <Box style={{ textAlign: "center", width: "270px", height: "63px" }}>
          {result !== 0 && (
            <span>
              The result of rolling {dice.name} is: {result}
            </span>
          )}
        </Box>
      </Container>
    </>
  );
}

export default DiceRollModal;
