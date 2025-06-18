import { useState } from "react";
import {
  ImmaterialResourceSelection,
  PowerForEncounterDto,
} from "../../../services/apiEncounter";
import { ParticipanceData } from "../../../services/apiEncounter";
import { Cell } from "../../../ui/containers/Cell";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import Dropdown from "../../../ui/forms/Dropdown";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { useGetPowers } from "../hooks/useGetPowers";
import { SetPower } from "./SessionLayout";
import Modal from "../../../ui/containers/Modal";
import { HiEye } from "react-icons/hi2";
import PowerForm from "../../powers/PowerForm";
import { EditModeContext } from "../../../context/EditModeContext";
import styled from "styled-components";
import { DiceSetString } from "../../../models/diceset";

export function PowerSelectionScreen({
  characterId,
  encounterId,
  participanceData,
  dispatch,
  onCloseModal,
}: {
  characterId: number;
  encounterId: number;
  participanceData: ParticipanceData;
  dispatch: React.Dispatch<SetPower>;
  onCloseModal: () => {};
}) {
  console.log(characterId);
  const { isLoading, powers, isError, error } = useGetPowers(characterId, encounterId);
  if (isLoading) {
    return <Spinner></Spinner>;
  }
  if (isError) {
    return <>{error}</>;
  }
  return (
    <SessionPowersTable
      powers={powers!}
      participanceData={participanceData}
      dispatch={dispatch}
      onCloseModal={onCloseModal}
    />
  );
}

PowerSelectionScreen.defaultProps = {
  onCloseModal: () => {},
};

export default function SessionPowersTable({
  powers,
  participanceData,
  dispatch,
  onCloseModal,
}: {
  powers: PowerForEncounterDto[];
  participanceData: ParticipanceData;
  dispatch: React.Dispatch<SetPower>;
  onCloseModal: () => {};
}) {
  return (
    <Menus>
      <Table
        header="Power"
        columns="auto auto auto auto auto auto auto auto auto auto auto auto auto auto"
      >
        <Table.Header>
          <div>Name</div>
          <div>Description</div>
          <div>Resource</div>
          <div>Action Type</div>
          <div>Range</div>
          <div>Max Targets</div>
          <div>Area Shape</div>
          <div>Area Size</div>
          <div>Castable By</div>
          <div>Power Type</div>
          <div>Target Type</div>
          <div>Difficulty class / Attack bonus</div>
          <div>Cast option</div>
          <div></div>
          <div></div>
        </Table.Header>
        <Table.Body
          columnCount={15}
          data={powers}
          render={(power) => (
            <PowerRow
              key={power.id}
              power={power}
              participanceData={participanceData}
              dispatch={dispatch}
              onCloseModal={onCloseModal}
            />
          )}
        />
      </Table>
    </Menus>
  );
}

function PowerRow({
  power,
  participanceData,
  dispatch,
  onCloseModal,
}: {
  power: PowerForEncounterDto;
  participanceData: ParticipanceData;
  dispatch: React.Dispatch<SetPower>;
  onCloseModal: () => {};
}) {
  const [chosenLevel, setChosenLevel] =
    useState<ImmaterialResourceSelection | null>(null);

  const buttonLabelArray = [];
  if(power.actionTypeRequired === "Action" && !power.requiredActionAvailable){
    buttonLabelArray.push('No actions left');
  }
  if(power.actionTypeRequired === "BonusAction" && !power.requiredBonusActionAvailable){
    buttonLabelArray.push('No bonus actions left');
  }
  if(power.actionTypeRequired === "WeaponAttack" && !power.requiredWeaponAttackAvailable){
    buttonLabelArray.push('No attacks left');
  }
  if(!power.vocalComponentRequirementSatisfied){
    buttonLabelArray.push('Cannot speak');
  }
  if(!power.somaticComponentRequirementSatisfied){
    buttonLabelArray.push('Cannot make gestures');
  }
  if(!power.requiredResourceAvailable){
    buttonLabelArray.push('Required resource not available');
  }
  if(!power.requiredMaterialComponentsAvailable){
    buttonLabelArray.push('Required components not available');
  }
  if(chosenLevel == null && power.availableLevels.length > 0){
    buttonLabelArray.push('Power level not selected');
  }
  const disabled = buttonLabelArray.length > 0;

  let valuesList = power.availableLevels.map((x) => {
    return {
      value: `${x.powerLevel}/${x.resourceLevel}`,
      label: `Power level: ${x.powerLevel} / Resource level: ${x.resourceLevel}`,
    };
  });

  const stringifiedChosenLevel = chosenLevel
    ? `${chosenLevel.powerLevel}/${chosenLevel.resourceLevel}`
    : null;

  return (
    <Table.Row>
      <Cell>{power.name}</Cell>
      <Cell>{power.description}</Cell>
      <Cell>{power.resourceName ?? "-"}</Cell>
      <Cell>{power.actionTypeRequired ?? "-"}</Cell>
      <Cell>{power.range ?? "-"}</Cell>
      <Cell>{power.maxTargets ?? "-"}</Cell>
      <Cell>{power.areaShape ?? "-"}</Cell>
      <Cell>{power.areaSize ?? "-"}</Cell>
      <Cell>{power.castableBy ?? "-"}</Cell>
      <Cell>{power.powerType ?? "-"}</Cell>
      <Cell>{power.targetType ?? "-"}</Cell>
      <Cell>{power.powerType === "Saveable" ? "DC" + power.difficultyClass : power.powerType === "Attack" ? DiceSetString(power.attackBonus) : "-"}</Cell>
      <Cell>
        <Dropdown
          valuesList={valuesList}
          chosenValue={stringifiedChosenLevel}
          setChosenValue={(e) =>
            setChosenLevel(
              e
                ? {
                    powerLevel: Number(e.split("/")[0]),
                    resourceLevel: Number(e.split("/")[1]),
                  }
                : null
            )
          }
        ></Dropdown>
      </Cell>

      <Button
        disabled={disabled}
        onClick={() => {
          dispatch({
            type: "SET_POWER",
            payload: {
              powerId: power.id,
              range: power.range,
              maxTargets: power.maxTargets,
              areaShape: power.areaShape,
              areaSize: power.areaSize,
              targetType: power.targetType,
              chosenLevel: chosenLevel,
            },
          });
          onCloseModal();
        }}
      >
        {buttonLabelArray.length > 0 ? buttonLabelArray.join(', ') : 'Select'}
      </Button>
      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={power.id} />
          <Menus.List id={power.id}>
            <Modal.Open opens="open">
              <Menus.Button icon={<HiEye />} onClick={() => {}}>
                Open
              </Menus.Button>
            </Modal.Open>
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="open">
          <EditModeContext.Provider value={{editMode: false}}>
            <Container>
              <PowerForm powerId={power.id}></PowerForm>
            </Container>
          </EditModeContext.Provider>
        </Modal.Window>
      </Modal>
    </Table.Row>
  );
}


const Container = styled.div`
  display: flex;
  flex-direction: column;
  max-height: 90vh;
  max-width: 80vw;
  overflow-y: hidden;
`;