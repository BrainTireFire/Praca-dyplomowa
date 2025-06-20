import { HiEye } from "react-icons/hi2";
import { DamageType } from "../../../models/damageType";
import { DiceSetString } from "../../../models/diceset";
import { WeaponAttack } from "../../../models/weaponattack";
import {
  ParticipanceData,
  WeaponAttackDto,
} from "../../../services/apiEncounter";
import { Cell } from "../../../ui/containers/Cell";
import Menus from "../../../ui/containers/Menus";
import Modal from "../../../ui/containers/Modal";
import RadioButton from "../../../ui/containers/RadioButton";
import Table from "../../../ui/containers/Table";
import Button from "../../../ui/interactive/Button";
import Spinner from "../../../ui/interactive/Spinner";
import { useCharacter } from "../../characters/hooks/useCharacter";
import { useGetWeaponAttacks } from "../hooks/useGetWeaponAttacks";
import { ChangeModeAction, SetWeaponAttack } from "./SessionLayout";
import { EditModeContext } from "../../../context/EditModeContext";
import ItemForm from "../../items/ItemForm";
import styled from "styled-components";

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
  const { isLoading, weaponAttacks, isError, error } = useGetWeaponAttacks(
    characterId,
    encounterId
  );
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
      <Table
        header="Weapon attack"
        columns="auto auto auto auto auto auto auto auto auto"
      >
        <Table.Header>
          <div>Main</div>
          <div>Weapon</div>
          <div>Damage</div>
          <div>Attack bonus</div>
          <div>D. type</div>
          <div>Reach/Range</div>
          <div>Melee attack</div>
          <div>Ranged attack</div>
          <div></div>
        </Table.Header>
        <Table.Body
          columnCount={9}
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
  const mainAttackLabel = weaponAttack.requiredWeaponAttackAvailable
    ? "Select"
    : "No attacks left";
  const offhandAttackLabel = weaponAttack.requiredWeaponAttackAvailable
    ? "Select"
    : "No bonus actions left";
  return (
    <Table.Row>
      <Cell>
        <RadioButton checked={weaponAttack.main} readOnly={true} />
      </Cell>

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
          disabled={!weaponAttack.requiredWeaponAttackAvailable}
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
          disabled={!weaponAttack.requiredWeaponAttackAvailable}
        >
          {weaponAttack.main ? mainAttackLabel : offhandAttackLabel}
        </Button>
      ) : (
        <Cell>Not available</Cell>
      )}
      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={weaponAttack.id} />
          <Menus.List id={weaponAttack.id}>
            <Modal.Open opens="open">
              <Menus.Button icon={<HiEye />} onClick={() => {}}>
                Open
              </Menus.Button>
            </Modal.Open>
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="open">
          <EditModeContext.Provider value={{ editMode: false }}>
            <Container>
              <ItemForm itemId={weaponAttack.id}></ItemForm>
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
  height: 90vh;
  max-height: 90vh;
  max-width: 80vw;
  overflow-y: hidden;
`;
