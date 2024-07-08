import styled from "styled-components";
import Box from "../../ui/containers/Box";
import Heading from "../../ui/text/Heading";
import Button from "../../ui/interactive/Button";
import { PlayerSelect } from "../../ui/interactive/PlayerSelect";

const Container = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-gap: 15px;
  justify-content: center;
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

export default function ShortRest() {
  return (
    <Container>
      <Heading as="h4" style={{ gridColumn: "1/3" }}>
        Short rest
      </Heading>
      <Box style={{ gridColumn: "1/2", gridRow: "2/5" }}>
        <p style={{ gridColumn: "1/2", marginBottom: "10px" }}>
          Select players:
        </p>
        {playersList.map((e) => (
          <PlayerSelect player={e} key={e.id}></PlayerSelect>
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
      <Button size="small" style={{ gridColumn: "2/3", gridRow: "4/5" }}>
        Accept
      </Button>
    </Container>
  );
}
