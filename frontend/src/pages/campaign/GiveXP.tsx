import { useRef, useState } from "react";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import { PlayerSelect } from "../../ui/interactive/PlayerSelect";
import Input from "../../ui/forms/Input";
import Button from "../../ui/interactive/Button";
import styled from "styled-components";

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
  gap: 20px;
`;

const playersList = [
  {
    name: "First",
    id: 1,
    xp: 20,
  },
  {
    name: "Second",
    id: 2,
    xp: 15,
  },
  {
    name: "Third",
    id: 3,
    xp: 24,
  },
  {
    name: "Fourth",
    id: 4,
    xp: 56,
  },
  {
    name: "Fifth",
    id: 5,
    xp: 74,
  },
];

function GiveXP() {
  const [players, setPlayers] = useState(playersList);
  const inputXPRef = useRef(0);

  const handleClick = () => {
    const inputXP = Number(inputXPRef.current.value);
    setPlayers(players.map((e) => ({ ...e, xp: e.xp + inputXP })));
  };

  return (
    <Container>
      <Heading as="h4">Give experience points</Heading>
      <Box>
        <p>Select players:</p>
        {players.map((e) => (
          <PlayerSelect player={e} key={e.id}></PlayerSelect>
        ))}
      </Box>
      <Heading as="h1">Amount of XP</Heading>
      <Input ref={inputXPRef} placeholder="Type amount of XP"></Input>
      <Button onClick={handleClick}>Give Points</Button>
    </Container>
  );
}

export default GiveXP;
