import { useContext } from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import Modal from "../../../ui/containers/Modal";
import { HiEye, HiTrash } from "react-icons/hi2";
import { Effect } from "../../../models/effect";
import { Cell } from "../../../ui/containers/Cell";
import { EffectParentObjectIdContext } from "../../../context/EffectParentObjectIdContext";
import ConfirmDelete from "../../../ui/containers/ConfirmDelete";
import { CharacterIdContext } from "../contexts/CharacterIdContext";
import { useDeleteConstantEffectInstance } from "../hooks/useDeleteConstantEffectInstance";
import EffectInstanceForm from "../../effects/EffectInstanceForm";

export default function EffectTable({ effects }: { effects: Effect[] }) {
  const { characterId } = useContext(CharacterIdContext);
  return (
    <Menus>
      <Table
        header="Temporary effects"
        button="Add new"
        columns="1fr 1fr 1fr 1fr 0.01rem"
        modal={
          <EffectParentObjectIdContext.Provider
            value={{ objectId: characterId, objectType: "CharacterConstant" }}
          >
            <EffectInstanceForm
              effectId={null}
              isConstant={false}
            ></EffectInstanceForm>
          </EffectParentObjectIdContext.Provider>
        }
      >
        <Table.Header>
          <div>Name</div>
          <div>Time left</div>
          <div>Source</div>
          <div>Target</div>
          <div></div>
        </Table.Header>
        <Table.Body
          data={effects}
          columnCount={5}
          render={(effect) => <EffectRow key={effect.id} effect={effect} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function EffectRow({ effect }: { effect: Effect }) {
  const { characterId } = useContext(CharacterIdContext);
  const { deleteEffectInstance, isPending } = useDeleteConstantEffectInstance(
    () => {},
    characterId as number
  );
  return (
    <Table.Row>
      <Cell>{effect.name}</Cell>

      <Cell>{effect.turnsLeft}</Cell>

      <Cell>{effect.source}</Cell>
      <Cell>{effect.target}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={effect.id} />
          <Menus.List id={effect.id}>
            <Modal.Open opens="open">
              <Menus.Button icon={<HiEye />} onClick={() => {}}>
                Open
              </Menus.Button>
            </Modal.Open>
            <Modal.Open opens="delete">
              <Menus.Button icon={<HiTrash />} onClick={() => {}}>
                Delete
              </Menus.Button>
            </Modal.Open>
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="delete">
          <ConfirmDelete
            resourceName="effect instance"
            disabled={isPending}
            onConfirm={() => {
              deleteEffectInstance(effect.id);
            }}
          />
        </Modal.Window>
        <Modal.Window name="open">
          <EffectParentObjectIdContext.Provider
            value={{ objectId: characterId, objectType: "CharacterConstant" }}
          >
            <EffectInstanceForm
              effectId={effect.id}
              isConstant={false}
            ></EffectInstanceForm>
          </EffectParentObjectIdContext.Provider>
        </Modal.Window>
      </Modal>
    </Table.Row>
  );
}
