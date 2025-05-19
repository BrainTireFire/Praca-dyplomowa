import { DamageType } from "../../../models/damageType";
import { DiceSetString } from "../../../models/diceset";
import { WeaponAttack } from "../../../models/weaponattack";
import { ParticipanceData, WeaponAttackDto } from "../../../services/apiEncounter";
import { Cell } from "../../../ui/containers/Cell";
import Menus from "../../../ui/containers/Menus";
import RadioButton from "../../../ui/containers/RadioButton";
import Table from "../../../ui/containers/Table";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { useCharacter } from "../../characters/hooks/useCharacter";
import { useGetWeaponAttacks } from "../hooks/useGetWeaponAttacks";
import { ChangeModeAction, SetWeaponAttack } from "./SessionLayout";

export function AttackSelectionScreen({
  characterId,
  encounterId,
  dispatch,
  onCloseModal,
}: {
  characterId: number;
  encounterId: number;
  dispatch: React.Dispatch<SetWeaponAttack>;
  onCloseModal: () => {};
}) {
  console.log(characterId);
  const { isLoading, weaponAttacks, isError, error } = useGetWeaponAttacks(characterId, encounterId);
  if (isLoading) {
    return <Spinner></Spinner>;
  }
  if (isError) {
    return <>{error}</>;
  }
  return (
    <SessionWeaponAttackTable
      weaponAttacks={weaponAttacks!}
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
  dispatch,
  onCloseModal,
}: {
  weaponAttacks: WeaponAttackDto[];
  dispatch: React.Dispatch<SetWeaponAttack>;
  onCloseModal: () => {};
}) {
  return (
    <Menus>
      <Table header="Weapon attack" columns="1fr 1fr 1fr 1fr 1fr 1fr 2fr 2fr">
        <Table.Header>
        <div>Main</div>
        <div>Weapon</div>
          <div>Damage</div>
          <div>Attack bonus</div>
          <div>D. type</div>
          <div>Reach/Range</div>
          <div>Melee attack</div>
          <div>Ranged attack</div>
        </Table.Header>
        <Table.Body
          columnCount={8}
          data={weaponAttacks}
          render={(weaponAttack) => (
            <WeaponAttackRow
              key={weaponAttack.id}
              weaponAttack={weaponAttack}
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
  dispatch,
  onCloseModal,
}: {
  weaponAttack: WeaponAttackDto;
  dispatch: React.Dispatch<SetWeaponAttack>;
  onCloseModal: () => {};
}) {
  
  const mainAttackLabel = weaponAttack.requiredWeaponAttackAvailable ? "Select" : "No attacks left";
  const offhandAttackLabel = weaponAttack.requiredWeaponAttackAvailable ? "Select" : "No bonus actions left";
  return (
    <Table.Row>
      <RadioButton checked={weaponAttack.main} readOnly={true} />

      <Cell>{weaponAttack.weaponName}</Cell>
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
            !weaponAttack.requiredWeaponAttackAvailable
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
            !weaponAttack.requiredWeaponAttackAvailable
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
