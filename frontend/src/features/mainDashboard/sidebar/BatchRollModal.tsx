import styled from "styled-components";
import Input from "../../../ui/forms/Input";
import Heading from "../../../ui/text/Heading";
import Button from "../../../ui/interactive/Button";
import Box from "../../../ui/containers/Box";
import { useEffect, useRef, useState } from "react";
import { DiceSet } from "../../../models/diceset";
import { rollDice } from "../../../services/apiDice";

const Container = styled.div`
  display: grid;
  grid-template-columns: 5vw 9vw;
  align-items: center;
  align-content: center;
  gap: 8px;
`;

const initialDiceSet: DiceSet = {
  d20: 0,
  d12: 0,
  d10: 0,
  d8: 0,
  d6: 0,
  d4: 0,
  d100: 0,
  flat: 0,
};

function BatchRollModal() {
  const [diceSet, setDiceSet] = useState<DiceSet>(initialDiceSet);
  const [results, setResults] = useState<DiceSet>(initialDiceSet);
  const [diceSetSnapshot, setDiceSetSnapshot] = useState<DiceSet>();
  const sum = useRef(0);

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

  const handleRoll = async () => {
    const data = await rollDice(diceSet);
    setResults(data);
    setDiceSetSnapshot(diceSet);
    sum.current = Object.entries(data).reduce(
      (acc, [, value]) => (value ? acc + value : acc),
      0
    );
  };

  return (
    <Container>
      {Object.keys(initialDiceSet)
        .filter((diceType) => diceType !== "flat") // "flat" is not needed in this component
        .map((diceType) => (
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

      {diceSetSnapshot && (
        <Box
          style={{
            display: "flex",
            flexDirection: "column",
            gridColumn: "1/3",
          }}
        >
          {Object.entries(results).map(([diceType, value]) =>
            value ? (
              <p key={diceType}>{`Rolling ${diceType} - ${
                diceSetSnapshot[diceType as keyof DiceSet]
              } times: ${value}`}</p>
            ) : null
          )}
          Sum of the rolls equals to : {sum.current}
        </Box>
      )}
    </Container>
  );
}

export default BatchRollModal;
