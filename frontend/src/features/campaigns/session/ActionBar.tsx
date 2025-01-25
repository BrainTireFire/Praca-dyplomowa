import styled, { css } from "styled-components";
import Box from "../../../ui/containers/Box";
import FormRowVertical from "../../../ui/forms/FormRowVertical";
import Input from "../../../ui/forms/Input";
import Button from "../../../ui/interactive/Button";
import { ControlState, ControlStateActions } from "./SessionLayout";
import useRollInitiative from "../hooks/useRollInitiative";
import { Encounter } from "../../../models/encounter/Encounter";
import { HubConnection } from "@microsoft/signalr";
import Spinner from "../../../ui/interactive/Spinner";
import { useIsItMyTurn } from "../hooks/useIsItMyTurn";
import { useIsGm } from "../hooks/useIsGM";
import { useContext, useEffect, useReducer } from "react";
import { ControlledCharacterContext } from "./context/ControlledCharacterContext";
import { useParticipanceData } from "../hooks/useParticipanceData";
import { ParticipanceData } from "../../../services/apiEncounter";
import Heading from "../../../ui/text/Heading";
import ButtonGroup from "../../../ui/interactive/ButtonGroup";
import useMoveCharacter from "../hooks/useMoveCharacter";
import useUpdateParticipanceData from "../hooks/useUpdateParticipanceData";
import useNextTurn from "../hooks/useNextTurn";
import Modal from "../../../ui/containers/Modal";
import { AttackSelectionScreen } from "./AttackSelectionScreen";
import { PowerSelectionScreen } from "./PowerSelectionScreen";

const initialParticipanceData: ParticipanceData = {
  actionsTaken: 0,
  bonusActionsTaken: 0,
  attacksMade: 0,
  movementUsed: 0,
  totalActions: 100, // Example pre-calculated total
  totalBonusActions: 50,
  totalAttacksPerAction: 200,
  totalMovement: 100,
};

export type ParticipanceAction =
  | { type: "ACTIONS_TAKEN"; payload: number }
  | { type: "BONUS_ACTIONS_TAKEN"; payload: number }
  | { type: "ATTACKS_MADE"; payload: number }
  | { type: "MOVEMENT_USED"; payload: number }
  | { type: "HITPOINTS_LEFT"; payload: number }
  | { type: "TEMPORARY_HITPOINTS_LEFT"; payload: number }
  | { type: "RESET_PARTICIPANCE_DATA"; payload: ParticipanceData };

export function participanceReducer(
  state: ParticipanceData = initialParticipanceData,
  action: ParticipanceAction
): ParticipanceData {
  switch (action.type) {
    case "ACTIONS_TAKEN":
      return {
        ...state,
        actionsTaken: action.payload,
      };
    case "BONUS_ACTIONS_TAKEN":
      return {
        ...state,
        bonusActionsTaken: action.payload,
      };
    case "ATTACKS_MADE":
      return {
        ...state,
        attacksMade: action.payload,
      };
    case "MOVEMENT_USED":
      return {
        ...state,
        movementUsed: action.payload,
      };
    case "HITPOINTS_LEFT":
      return {
        ...state,
        hitpoints: action.payload,
      };
    case "TEMPORARY_HITPOINTS_LEFT":
      return {
        ...state,
        temporaryHitpoints: action.payload,
      };
    case "RESET_PARTICIPANCE_DATA":
      return { ...action.payload };
    default:
      return state;
  }
}

export default function ActionBar({
  controlState,
  encounter,
  dispatch,
  connection,
}: {
  controlState: ControlState;
  encounter: Encounter;
  dispatch: React.Dispatch<ControlStateActions>;
  connection: HubConnection;
}) {
  const { rollInitiative, isPending } = useRollInitiative(encounter.id, () =>
    connection.invoke("SendRequeryInitiative")
  );
  const [controlledCharacterId] = useContext(ControlledCharacterContext);
  const { isLoading: isLoadingIsItMyTurn, isItMyTurn } = useIsItMyTurn(
    encounter.id,
    controlledCharacterId
  );
  const { isLoading: isLoadingIsGM, isGM } = useIsGm(encounter.id);
  const [participanceState, participanceStateDispatch] = useReducer(
    participanceReducer,
    initialParticipanceData
  );
  const { isLoading: isLoadingParticipanceData, participance } =
    useParticipanceData(encounter.id, controlledCharacterId);
  const { isPending: isPendingParticipanceDataUpdate, updateParticipanceData } =
    useUpdateParticipanceData(encounter.id, controlledCharacterId, () => {});
  const { isPending: isPendingNextTurnSetting, nextTurn } = useNextTurn(
    encounter.id,
    () => {}
  );
  useEffect(() => {
    if (!!participance) {
      participanceStateDispatch({
        type: "RESET_PARTICIPANCE_DATA",
        payload: participance,
      });
    }
  }, [participance, participanceStateDispatch]);
  const { isPending: isPendingMove, moveCharacter } = useMoveCharacter(
    encounter.id,
    controlledCharacterId,
    () => {
      dispatch({ type: "SET_PATH", payload: [] });
    }
  );
  if (
    isLoadingIsGM ||
    isLoadingIsItMyTurn ||
    isLoadingParticipanceData ||
    isPendingParticipanceDataUpdate ||
    isPendingNextTurnSetting ||
    isPendingMove
  ) {
    return <Spinner></Spinner>;
  }
  return (
    <>
      {!controlledCharacterId && (
        <Heading as="h1">No character selected</Heading>
      )}
      {controlledCharacterId && (
        <>
          <Heading as="h2">{participanceState.characterName}</Heading>
          <Heading as="h3">
            {isItMyTurn ? "Active turn!" : "Waiting for turn"}
          </Heading>
          <Container IsActive={!!isItMyTurn}>
            <Cell>
              <FormRowVertical label={"Actions taken"}>
                <span>
                  <Input
                    type="number"
                    size="small"
                    customStyles={css`
                      width: 5em;
                    `}
                    disabled={!isGM}
                    value={participanceState.actionsTaken}
                    onChange={(e) =>
                      participanceStateDispatch({
                        type: "ACTIONS_TAKEN",
                        payload: Number(e.target.value),
                      })
                    }
                  />
                  &nbsp;/ {participanceState.totalActions}
                </span>
              </FormRowVertical>
              <FormRowVertical label={"Bonus actions taken"}>
                <span>
                  <Input
                    type="number"
                    size="small"
                    customStyles={css`
                      width: 5em;
                    `}
                    disabled={!isGM}
                    value={participanceState.bonusActionsTaken}
                    onChange={(e) =>
                      participanceStateDispatch({
                        type: "BONUS_ACTIONS_TAKEN",
                        payload: Number(e.target.value),
                      })
                    }
                  />
                  &nbsp;/ {participanceState.totalBonusActions}
                </span>
              </FormRowVertical>
              {/* <FormRowVertical label={"Reactions taken"}>
          <span>
          <Input
          type="number"
          size="small"
          customStyles={css`
          width: 5em;
          `}
          disabled={!isGM}
          value={participanceState.actionsTaken}
          onChange={(e) =>
          participanceStateDispatch({
                  type: "ACTIONS_TAKEN",
                  payload: Number(e.target.value),
                })
                }
            />
            &nbsp;/ X
          </span>
        </FormRowVertical> */}
              <FormRowVertical label={"Attacks made"}>
                <span>
                  <Input
                    type="number"
                    size="small"
                    customStyles={css`
                      width: 5em;
                    `}
                    disabled={!isGM}
                    value={participanceState.attacksMade}
                    onChange={(e) =>
                      participanceStateDispatch({
                        type: "ATTACKS_MADE",
                        payload: Number(e.target.value),
                      })
                    }
                  />
                  &nbsp;/ {participanceState.totalAttacksPerAction}
                </span>
              </FormRowVertical>
            </Cell>
            <Cell>
              <span>Concentrates on: powerName</span>
              <Button
                size="small"
                customStyles={css`
                  height: 50px;
                `}
                disabled={!isItMyTurn}
              >
                Drop concentration
              </Button>
            </Cell>
            <Cell>
              <FormRowVertical label={"Movement used"}>
                <span>
                  <Input
                    type="number"
                    size="small"
                    customStyles={css`
                      width: 5em;
                    `}
                    disabled={!isGM}
                    value={participanceState.movementUsed}
                    onChange={(e) =>
                      participanceStateDispatch({
                        type: "MOVEMENT_USED",
                        payload: Number(e.target.value),
                      })
                    }
                  />
                  &nbsp;/ {participanceState.totalMovement}
                </span>
              </FormRowVertical>
              <FormRowVertical label={"Hitpoints"}>
                <span>
                  <Input
                    type="number"
                    size="small"
                    customStyles={css`
                      width: 5em;
                    `}
                    disabled={!isGM}
                    value={participanceState.hitpoints}
                    onChange={(e) =>
                      participanceStateDispatch({
                        type: "HITPOINTS_LEFT",
                        payload: Number(e.target.value),
                      })
                    }
                  />
                  &nbsp;/ {participanceState.maxHitpoints}
                </span>
              </FormRowVertical>
              <FormRowVertical label={"Temporary hitpoints"}>
                <span>
                  <Input
                    type="number"
                    size="small"
                    customStyles={css`
                      width: 5em;
                    `}
                    disabled={!isGM}
                    value={participanceState.temporaryHitpoints}
                    onChange={(e) =>
                      participanceStateDispatch({
                        type: "TEMPORARY_HITPOINTS_LEFT",
                        payload: Number(e.target.value),
                      })
                    }
                  />
                  &nbsp;
                </span>
              </FormRowVertical>
              {isGM && (
                <Button
                  size="small"
                  customStyles={css`
                    height: 50px;
                  `}
                  disabled={!isGM}
                  onClick={() => updateParticipanceData(participanceState)}
                >
                  Update
                </Button>
              )}
            </Cell>
            <Cell>
              {controlState.mode === "Idle" && (
                <ButtonGroup>
                  <Modal>
                    <Modal.Open opens="attackSelection">
                      <Button
                        size="small"
                        customStyles={css`
                          height: 50px;
                        `}
                        disabled={!isItMyTurn}
                      >
                        Weapon attack
                      </Button>
                    </Modal.Open>
                    <Modal.Window name="attackSelection">
                      {controlledCharacterId && (
                        <AttackSelectionScreen
                          characterId={controlledCharacterId}
                          participanceData={participanceState}
                          dispatch={dispatch}
                        />
                      )}
                    </Modal.Window>
                  </Modal>
                  <Modal>
                    <Modal.Open opens="powerSelection">
                      <Button
                        size="small"
                        customStyles={css`
                          height: 50px;
                        `}
                        disabled={!isItMyTurn}
                      >
                        Cast power
                      </Button>
                    </Modal.Open>
                    <Modal.Window name="powerSelection">
                      {controlledCharacterId && (
                        <PowerSelectionScreen
                          characterId={controlledCharacterId}
                          participanceData={participanceState}
                          dispatch={dispatch}
                        />
                      )}
                    </Modal.Window>
                  </Modal>
                  <Button
                    size="small"
                    customStyles={css`
                      height: 50px;
                    `}
                    onClick={() =>
                      dispatch({ type: "CHANGE_MODE", payload: "Movement" })
                    }
                    disabled={!isItMyTurn}
                  >
                    Plot movement
                  </Button>
                </ButtonGroup>
              )}
              {controlState.mode === "Movement" && (
                <Button onClick={() => moveCharacter(controlState.path)}>
                  Confirm movement
                </Button>
              )}
              {controlState.mode === "PowerCast" && (
                <Button
                  disabled={
                    controlState.powerSelected
                      ? controlState.powerSelected.maxTargets! -
                          controlState.powerTargets.length <
                          0 || controlState.powerTargets.length === 0
                      : true
                  }
                  onClick={() =>
                    dispatch({
                      type: "POWER_CAST_OVERLAY_DATA",
                      payload: { targetIds: controlState.powerTargets },
                    })
                  }
                >
                  Confirm power target selection (targets:{" "}
                  {controlState.powerTargets.length}/
                  {controlState.powerSelected?.maxTargets})
                </Button>
              )}
              <Button
                size="small"
                customStyles={css`
                  height: 50px;
                `}
                onClick={() =>
                  dispatch({ type: "CHANGE_MODE", payload: "Idle" })
                }
                disabled={!isItMyTurn}
              >
                Idle
              </Button>
              <Button
                size="small"
                customStyles={css`
                  height: 50px;
                `}
                disabled={!isItMyTurn}
                onClick={() => nextTurn()}
              >
                Next turn
              </Button>
              {isPending && <Spinner />}
              {!isPending && isGM && (
                <Button
                  size="small"
                  customStyles={css`
                    height: 50px;
                  `}
                  onClick={() => rollInitiative()}
                  disabled={!isGM}
                >
                  Roll initiative
                </Button>
              )}
            </Cell>
          </Container>
        </>
      )}
    </>
  );
}

const Container = styled(Box)<TileProperties>`
  border-radius: 0;
  width: 100%;
  height: 1fr;
  display: grid;
  grid-template-columns: auto auto;
  grid-template-rows: auto auto;
  overflow: auto;
  gap: 5px;
  border: ${(props) =>
    props.IsActive ? css`3px solid red` : css`1px solid var(--color-border)`};
`;

const Cell = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  gap: 10px;
  align-items: center;
`;

type TileProperties = {
  IsActive: boolean;
};
