import styled, { css } from "styled-components";
import Box from "../../../ui/containers/Box";
import Button from "../../../ui/interactive/Button";
import { useInitiativeQueue } from "../hooks/useInitiativeQueue";
import { useParams } from "react-router-dom";
import { InitiativeQueueItem } from "../../../services/apiEncounter";
import Spinner from "../../../ui/interactive/Spinner";
import { useIsGm } from "../hooks/useIsGM";
import useModifyInitiativeQueue from "../hooks/useModifyInitiativeQueue";
import useSetActiveTurn from "../hooks/useSetActiveTurn";
import { useContext } from "react";
import { ControlledCharacterContext } from "../session/context/ControlledCharacterContext";
import { useControlledCharacters } from "../hooks/useControlledCharacters";

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
  const { groupName } = useParams<{ groupName: string }>();
  console.log("group name: " + groupName);
  const { isLoading, initiativeQueue } = useInitiativeQueue(Number(groupName));
  const { isLoading: isLoadingIsGM, isGM } = useIsGm(Number(groupName));
  const { isPending, setActiveTurn } = useSetActiveTurn(
    Number(groupName),
    () => {}
  );
  const {
    isLoading: isLoadingControllerCharacters,
    characterIds: controlledCharacterIds,
  } = useControlledCharacters(Number(groupName));
  if (
    isLoading ||
    isLoadingIsGM ||
    isPending ||
    isLoadingControllerCharacters
  ) {
    return <Spinner />;
  }
  return (
    <>
      {initiativeQueue &&
        initiativeQueue.map((item) => (
          <InititativeTile
            item={item}
            key={item.characterId}
            isGM={isGM as boolean}
            handleChangeActiveTurn={setActiveTurn}
            controlled={
              (controlledCharacterIds as number[]).find(
                (x) => item.characterId === x
              ) !== undefined
            }
          ></InititativeTile>
        ))}
    </>
  );
}

function InititativeTile({
  item,
  isGM,
  handleChangeActiveTurn,
  controlled,
}: {
  item: InitiativeQueueItem;
  isGM: boolean;
  handleChangeActiveTurn: (characterId: number) => void;
  controlled: boolean;
}) {
  const [controlledCharacterId, setControlledCharacterId] = useContext(
    ControlledCharacterContext
  );
  return (
    <Tile IsActive={item.activeTurn}>
      <TileCell1>
        <span>Name: {item.name}</span>
        <br></br>
        <span>Controlled by: {item.playerName}</span>
        <br></br>
        <span>Order: {item.placeInQueue}</span>
        <br></br>
        <span>Initiative roll: {item.initiativeRollResult}</span>
      </TileCell1>
      <TileCell2>
        {isGM && (
          <>
            <Button size="small">Move up</Button>
            <Button
              size="small"
              onClick={() => handleChangeActiveTurn(item.characterId)}
            >
              Set active turn
            </Button>
            <Button size="small">Move down</Button>
          </>
        )}
        <Button onClick={() => setControlledCharacterId(item.characterId)}>
          {controlled || isGM ? "Take control" : "Set focus"}
        </Button>
        {controlledCharacterId}
      </TileCell2>
      <TileCell3>
        <Button size="small">Display character sheet</Button>
      </TileCell3>
      <TileCell4>
        {isGM && (
          <Button size="small" variation="danger">
            Remove
          </Button>
        )}
      </TileCell4>
    </Tile>
  );
}

const Tile = styled(Box)<TileProperties>`
  display: grid;
  grid-template-columns: 60% auto;
  grid-template-rows: auto auto;
  gap: 10px;
  border: ${(props) =>
    props.IsActive ? css`3px solid red` : css`1px solid var(--color-border)`};
`;
// border-color: ${(props) =>
//  props.IsActive
//   ? css`rgba(var(--color-border), 0.05)`
//    : css`rgba(var(--color-secondary-background-rgb), 1)`};

type TileProperties = {
  IsActive: boolean;
};

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
