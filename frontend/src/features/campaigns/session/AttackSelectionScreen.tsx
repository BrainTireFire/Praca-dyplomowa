import { DamageType } from "../../../models/damageType";
import { DiceSetString } from "../../../models/diceset";
import { WeaponAttack } from "../../../models/weaponattack";
import { ParticipanceData } from "../../../services/apiEncounter";
import { Cell } from "../../../ui/containers/Cell";
import Menus from "../../../ui/containers/Menus";
import RadioButton from "../../../ui/containers/RadioButton";
import Table from "../../../ui/containers/Table";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { useCharacter } from "../../characters/hooks/useCharacter";
import { ChangeModeAction, SetWeaponAttack } from "./SessionLayout";

export function AttackSelectionScreen({
  characterId,
  participanceData,
  dispatch,
  onCloseModal,
}: {
  characterId: number;
  participanceData: ParticipanceData;
  dispatch: React.Dispatch<SetWeaponAttack>;
  onCloseModal: () => {};
}) {
  console.log(characterId);
  const { isLoading, character, isError, error } = useCharacter(characterId);
  if (isLoading) {
    return <Spinner></Spinner>;
  }
  if (isError) {
    return <>{error}</>;
  }
  return (
    <SessionWeaponAttackTable
      weaponAttacks={character!.weaponAttacks}
      participanceData={participanceData}
      dispatch={dispatch}
      onCloseModal={onCloseModal}
    />
  );
}

AttackSelectionScreen.defaultProps = {
  onCloseModal: () => {},
};

export default function SessionWeaponAttackTable({
  weaponAttacks,
  participanceData,
  dispatch,
  onCloseModal,
}: {
  weaponAttacks: WeaponAttack[];
  participanceData: ParticipanceData;
  dispatch: React.Dispatch<SetWeaponAttack>;
  onCloseModal: () => {};
}) {
  return (
    <Menus>
      <Table header="Weapon attack" columns="1fr 1fr 1fr 1fr 1fr 2fr 2fr">
        <Table.Header>
          <div>Main</div>
          <div>Damage</div>
          <div>Attack bonus</div>
          <div>D. type</div>
          <div>Reach/Range</div>
          <div>Melee attack</div>
          <div>Ranged attack</div>
        </Table.Header>
        <Table.Body
          columnCount={7}
          data={weaponAttacks}
          render={(weaponAttack) => (
            <WeaponAttackRow
              key={weaponAttack.id}
              weaponAttack={weaponAttack}
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

function WeaponAttackRow({
  weaponAttack,
  participanceData,
  dispatch,
  onCloseModal,
}: {
  weaponAttack: WeaponAttack;
  participanceData: ParticipanceData;
  dispatch: React.Dispatch<SetWeaponAttack>;
  onCloseModal: () => {};
}) {
  const mainAttackAllowed =
    participanceData.totalActions - participanceData.actionsTaken > 0 ||
    participanceData.totalAttacksPerAction - participanceData.attacksMade > 0;
  const offhandAttackAllowed =
    participanceData.totalBonusActions - participanceData.bonusActionsTaken > 0;
  const mainAttackLabel = mainAttackAllowed ? "Select" : "No attacks left";
  const offhandAttackLabel = offhandAttackAllowed
    ? "Select"
    : "No bonus actions left";
  return (
    <Table.Row>
      <RadioButton checked={weaponAttack.main} readOnly={true} />

      <Cell>{DiceSetString(weaponAttack.damage)}</Cell>
      <Cell>{DiceSetString(weaponAttack.attackBonus)}</Cell>
      <Cell>{DamageType(weaponAttack.damageType)}</Cell>
      <Cell>
        {weaponAttack.reach ?? "-"}/{weaponAttack.range ?? "-"}
      </Cell>
      {weaponAttack.reach ? (
        <Button
          onClick={() => {
            dispatch({
              type: "SET_WEAPON_ATTACK",
              payload: {
                weaponId: weaponAttack.id,
                range: weaponAttack.reach!,
                isRanged: false,
              },
            });
            onCloseModal();
          }}
          size="small"
          disabled={
            weaponAttack.main ? !mainAttackAllowed : !offhandAttackAllowed
          }
        >
          {weaponAttack.main ? mainAttackLabel : offhandAttackLabel}
        </Button>
      ) : (
        <Cell>Not available</Cell>
      )}
      {weaponAttack.range ? (
        <Button
          onClick={() => {
            dispatch({
              type: "SET_WEAPON_ATTACK",
              payload: {
                weaponId: weaponAttack.id,
                range: weaponAttack.range!,
                isRanged: true,
              },
            });
            onCloseModal();
          }}
          size="small"
          disabled={
            weaponAttack.main ? !mainAttackAllowed : !offhandAttackAllowed
          }
        >
          {weaponAttack.main ? mainAttackLabel : offhandAttackLabel}
        </Button>
      ) : (
        <Cell>Not available</Cell>
      )}
    </Table.Row>
  );
}
