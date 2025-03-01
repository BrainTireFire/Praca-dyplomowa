import { DamageType } from "../../../models/damageType";
import { DiceSetString } from "../../../models/diceset";
import { Power } from "../../../models/session/VirtualBoardProps";
import { WeaponAttack } from "../../../models/weaponattack";
import { PowerForEncounterDto } from "../../../services/apiCharacters";
import { ParticipanceData } from "../../../services/apiEncounter";
import { Cell } from "../../../ui/containers/Cell";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { useCharacter } from "../../characters/hooks/useCharacter";
import { useGetPowers } from "../hooks/useGetPowers";
import { SetPower } from "./SessionLayout";

export function PowerSelectionScreen({
  characterId,
  participanceData,
  dispatch,
  onCloseModal,
}: {
  characterId: number;
  participanceData: ParticipanceData;
  dispatch: React.Dispatch<SetPower>;
  onCloseModal: () => {};
}) {
  console.log(characterId);
  const { isLoading, powers, isError, error } = useGetPowers(characterId);
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
        columns="1fr 6fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr"
      >
        <Table.Header>
          <div>Name</div>
          <div>Description</div>
          <div>Resource</div>
          <div>Min Resource Level</div>
          <div>Action Type</div>
          <div>Range</div>
          <div>Max Targets</div>
          <div>Area Shape</div>
          <div>Area Size</div>
          <div>Castable By</div>
          <div>Power Type</div>
          <div>Target Type</div>
          <div></div>
        </Table.Header>
        <Table.Body
          columnCount={13}
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
  console.log(participanceData);
  console.log(power);
  const actionsLeft =
    participanceData.totalActions - participanceData.actionsTaken > 0;
  const bonusActionsLeft =
    participanceData.totalBonusActions - participanceData.bonusActionsTaken > 0;
  const actionOrBonusAction = power.actionTypeRequired === "Action";
  const buttonLabel = actionOrBonusAction
    ? actionsLeft
      ? "Select"
      : "No actions left"
    : bonusActionsLeft
    ? "Select"
    : "No bonus actions left";
  const disabled =
    (actionOrBonusAction ? !actionsLeft : !bonusActionsLeft) ||
    !(
      power.vocalComponentRequirementSatisfied &&
      power.somaticComponentRequirementSatisfied &&
      power.requiredResourceAvailable &&
      power.requiredMaterialComponentsAvailable
    );
  console.log(actionOrBonusAction ? !actionsLeft : !bonusActionsLeft);

  return (
    <Table.Row>
      <Cell>{power.name}</Cell>
      <Cell>{power.description}</Cell>
      <Cell>{power.resourceName ?? "-"}</Cell>
      <Cell>{power.minimumResourceLevel ?? "-"}</Cell>
      <Cell>{power.actionTypeRequired ?? "-"}</Cell>
      <Cell>{power.range ?? "-"}</Cell>
      <Cell>{power.maxTargets ?? "-"}</Cell>
      <Cell>{power.areaShape ?? "-"}</Cell>
      <Cell>{power.areaSize ?? "-"}</Cell>
      <Cell>{power.castableBy ?? "-"}</Cell>
      <Cell>{power.powerType ?? "-"}</Cell>
      <Cell>{power.targetType ?? "-"}</Cell>

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
            },
          });
          onCloseModal();
        }}
      >
        {buttonLabel}
      </Button>
    </Table.Row>
  );
}
