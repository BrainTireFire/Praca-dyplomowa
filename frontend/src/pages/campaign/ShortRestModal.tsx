import styled from "styled-components";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { PlayerSelect } from "../../ui/interactive/PlayerSelect";
import { useState } from "react";

const Container = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-gap: 15px;
  justify-content: center;
`;

const playersList = [
  { name: "First", id: 1, xp: 20, stamina: 50 },
  { name: "Second", id: 2, xp: 15, stamina: 50 },
  { name: "Third", id: 3, xp: 24, stamina: 50 },
  { name: "Fourth", id: 4, xp: 56, stamina: 45 },
  { name: "Fifth", id: 5, xp: 74, stamina: 50 },
];

export default function ShortRest() {
  const [players, setPlayers] = useState(playersList);
  const [selectedPlayers, setSelectedPlayers] = useState<number[]>([]);

  const handlePlayerSelect = (id: number) => {
    setSelectedPlayers((previousSelection) =>
      previousSelection.includes(id)
        ? previousSelection.filter((playerId) => id !== playerId)
        : [...previousSelection, id]
    );
  };

  const handleClick = () => {
    setPlayers((previous) =>
      previous.map((player) =>
        selectedPlayers.includes(player.id)
          ? { ...player, stamina: 100 }
          : player
      )
    );
  };

  return (
    <Container>
      <Heading as="h4" style={{ gridColumn: "1/3" }}>
        Short rest
      </Heading>
      <Box style={{ gridColumn: "1/2", gridRow: "2/5" }}>
        <p style={{ gridColumn: "1/2", marginBottom: "10px" }}>
          Select players:
        </p>
        {players.map((e) => (
          <PlayerSelect
            handlePlayerSelect={handlePlayerSelect}
            player={e}
            key={e.id}
            type="rest"
          ></PlayerSelect>
        ))}
      </Box>
      <div
        style={{
          gridRow: "2/4",
          display: "flex",
          flexDirection: "column",
        }}
      >
        <Heading as="h2" style={{ gridColumn: "2/3", marginBottom: "15px" }}>
          Pick strength dice
        </Heading>
        <Button style={{ gridColumn: "2/3" }}>Send notification</Button>
      </div>
      <Button
        onClick={handleClick}
        size="small"
        style={{ gridColumn: "2/3", gridRow: "4/5" }}
      >
        Rest
      </Button>
    </Container>
  );
}
