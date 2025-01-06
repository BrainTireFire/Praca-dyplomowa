import styled from "styled-components";
import Box from "../../../ui/containers/Box";
import Button from "../../../ui/interactive/Button";

type characterInitiative = {
  id: number;
  name: string;
  playerName: string;
  placeInQueue: number;
};

const testData = [
  {
    id: 1,
    name: "Thaldrin",
    playerName: "Alice",
    placeInQueue: 1,
  },
  {
    id: 2,
    name: "Kael'thas",
    playerName: "Bob",
    placeInQueue: 2,
  },
  {
    id: 3,
    name: "Arwen",
    playerName: "Charlie",
    placeInQueue: 3,
  },
  {
    id: 4,
    name: "Drogon",
    playerName: "Diana",
    placeInQueue: 4,
  },
  {
    id: 5,
    name: "Fenrir",
    playerName: "Eve",
    placeInQueue: 5,
  },
];

export function InitiativeQueue() {
  return (
    <>
      {testData
        .sort((a, b) => a.placeInQueue - b.placeInQueue)
        .map((item) => (
          <InititativeTile item={item} key={item.id}></InititativeTile>
        ))}
    </>
  );
}

function InititativeTile({ item }: { item: characterInitiative }) {
  return (
    <Tile>
      <TileCell1>
        <span>Name: {item.name}</span>
        <br></br>
        <span>Controlled by: {item.playerName}</span>
      </TileCell1>
      <TileCell2>
        <Button size="small">Move up</Button>
        <Button size="small">Set as active</Button>
        <Button size="small">Move up</Button>
      </TileCell2>
      <TileCell3>
        <Button size="small">Display character sheet</Button>
      </TileCell3>
      <TileCell4>
        <Button size="small" variation="danger">
          Remove
        </Button>
      </TileCell4>
    </Tile>
  );
}

const Tile = styled(Box)`
  display: grid;
  grid-template-columns: 60% auto;
  grid-template-rows: auto auto;
  gap: 10px;
`;

const TileCell1 = styled.div`
  grid-column: 1;
  grid-row: 1;
`;
const TileCell2 = styled.div`
  grid-column: 2;
  grid-row: 1;
  display: flex;
  flex-direction: column;
  gap: 5px;
`;
const TileCell3 = styled.div`
  grid-column: 1;
  grid-row: 2;
`;
const TileCell4 = styled.div`
  grid-column: 2;
  grid-row: 2;
`;
