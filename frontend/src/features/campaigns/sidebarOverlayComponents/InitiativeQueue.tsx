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
import useNextTurn from "../hooks/useNextTurn";
import useRollInitiative from "../hooks/useRollInitiative";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import useDeleteParticipanceData from "../hooks/useDeleteParticipanceData";
import useMoveInQueue from "../hooks/useMoveInQueue";
import Modal from "../../../ui/containers/Modal";
import { CharacterIdContext } from "../../characters/contexts/CharacterIdContext";
import CharactersSheet from "../../characters/CharactersSheet";

type characterInitiative = {
  id: number;
  name: string;
  playerName: string;
  placeInQueue: number;
};

export function InitiativeQueue() {
  const { groupName } = useParams<{ groupName: string }>();
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
  const { isPending: isPendingNextTurn, nextTurn } = useNextTurn(
    Number(groupName),
    () => {}
  );
  const { isPending: isPendingRollInitiative, rollInitiative } =
    useRollInitiative(Number(groupName), () => {});
  if (
    isLoading ||
    isLoadingIsGM ||
    isPending ||
    isLoadingControllerCharacters ||
    isPendingRollInitiative
  ) {
    return <Spinner />;
  }
  return (
    <>
      {initiativeQueue &&
        initiativeQueue.map((item) => (
          <InitiativeTile
            item={item}
            key={item.characterId}
            isGM={isGM as boolean}
            handleChangeActiveTurn={setActiveTurn}
            controlled={
              (controlledCharacterIds as number[]).find(
                (x) => item.characterId === x
              ) !== undefined
            }
          ></InitiativeTile>
        ))}
      {isGM && (
        <ButtonGroup style={{ paddingTop: "5px" }}>
          <Button disabled={!isGM} onClick={() => nextTurn()}>
            Next turn
          </Button>
          <Button onClick={() => rollInitiative()} disabled={!isGM}>
            Roll initiative
          </Button>
        </ButtonGroup>
      )}
    </>
  );
}

function InitiativeTile({
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
  const { groupName } = useParams<{ groupName: string }>();
  const [controlledCharacterId, setControlledCharacterId] = useContext(
    ControlledCharacterContext
  );
  const { isPending: isPendingRemoval, deleteParticipanceData } =
    useDeleteParticipanceData(Number(groupName), item.characterId, () => {});
  const { isPending: isPendingOrderChange, moveInQueue } = useMoveInQueue(
    Number(groupName),
    item.characterId,
    () => {}
  );
  return (
    <Tile IsActive={item.activeTurn} IsNpc={item.isNpc}>
      <TileCell1>
        {item.isNpc ? (
          <SpanStyled>NPC Character</SpanStyled>
        ) : (
          <SpanStyled>PC Character</SpanStyled>
        )}
        <br></br>
        <span>Name: {item.name}</span>
        <br></br>
        <span>Controlled by: {item.playerName}</span>
        <br></br>
        <span>Order: {item.placeInQueue}</span>
        <br></br>
        <span>Initiative roll: {item.initiativeRollResult}</span>
        <br></br>
        <span>Successful death saves: {item.succeededDeathSaves}</span>
        <br></br>
        <span>Failed death saves: {item.failedDeathSaves}</span>
      </TileCell1>
      <TileCell2>
        {isGM && (
          <>
            <Button size="small" onClick={() => moveInQueue(true)}>
              Move up
            </Button>
            <Button
              size="small"
              onClick={() => handleChangeActiveTurn(item.characterId)}
            >
              Set active turn
            </Button>
            <Button size="small" onClick={() => moveInQueue(false)}>
              Move down
            </Button>
          </>
        )}
        <Button onClick={() => setControlledCharacterId(item.characterId)}>
          {controlled || isGM ? "Take control" : "Set focus"}
        </Button>
      </TileCell2>
      <TileCell3>
        <Modal>
          <Modal.Open opens="CharactersSheet">
            <Button size="small">Display character sheet</Button>
          </Modal.Open>
          <Modal.Window name="CharactersSheet">
            <CharacterIdContext.Provider
              value={{ characterId: item.characterId }}
            >
              <Container>
                <CharactersSheet />
              </Container>
            </CharacterIdContext.Provider>
          </Modal.Window>
        </Modal>
      </TileCell3>
      <TileCell4>
        {isGM && (
          <Button
            size="small"
            variation="danger"
            onClick={() => deleteParticipanceData()}
          >
            Remove
          </Button>
        )}
      </TileCell4>
    </Tile>
  );
}

const SpanStyled = styled.span`
  color: var(--color-header-text);
  font-weight: bold;
`;

const Tile = styled(Box)<TileProperties>`
  display: grid;
  grid-template-columns: 60% auto;
  grid-template-rows: auto auto;
  gap: 10px;
  border: ${(props) =>
    props.IsActive ? css`3px solid red` : css`1px solid var(--color-border)`};

  background-color: ${(props) =>
    props.IsNpc ? css`var(--backdrop-color-hover)` : ""};
`;
// border-color: ${(props) =>
//  props.IsActive
//   ? css`rgba(var(--color-border), 0.05)`
//    : css`rgba(var(--color-secondary-background-rgb), 1)`};

type TileProperties = {
  IsActive: boolean;
  IsNpc: boolean;
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

const Container = styled.div`
  display: flex;
  flex-direction: column;
  height: 90vh;
  width: 80vw;
  overflow-y: hidden;
`;
