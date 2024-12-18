import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import Modal from "../../../ui/containers/Modal";
import { HiEye, HiTrash } from "react-icons/hi2";
import { Cell } from "../../../ui/containers/Cell";
import { initialState } from "../../effects/EffectBlueprintForm";
import Spinner from "../../../ui/interactive/Spinner";
import ConfirmDelete from "../../../ui/containers/ConfirmDelete";
import { useContext } from "react";
import { ItemIdContext } from "../contexts/ItemIdContext";
import { useCreateEffectInstance } from "../hooks/useCreateEffectInstance";
import { useDeleteEffectInstance } from "../hooks/useDeleteEffectInstance";
import { EffectBlueprintListItem } from "../models/effectBlueprint";
import EffectInstanceForm from "../../effects/EffectInstanceForm";
import { EffectParentObjectIdContext } from "../../../context/EffectParentObjectIdContext";

export default function EffectTable({
  effects,
}: {
  effects: EffectBlueprintListItem[];
}) {
  const { itemId } = useContext(ItemIdContext);
  const { createEffectInstance, isPending } = useCreateEffectInstance(() => {},
  itemId as number);
  if (isPending) {
    return <Spinner></Spinner>;
  }
  return (
    <Menus>
      <Table
        header="Effects"
        button="Add new"
        columns="1fr 0.01rem"
        // buttonOnClick={() => createEffectInstance(initialState)}
        modal={
          <EffectParentObjectIdContext.Provider
            value={{ objectId: itemId, objectType: "Item" }}
          >
            <EffectInstanceForm effectId={null}></EffectInstanceForm>
          </EffectParentObjectIdContext.Provider>
        }
      >
        <Table.Header>
          <div>Name</div>
          <div></div>
        </Table.Header>
        <Table.Body
          data={effects}
          columnCount={2}
          render={(effect) => <EffectRow key={effect.id} effect={effect} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function EffectRow({ effect }: { effect: EffectBlueprintListItem }) {
  const { itemId } = useContext(ItemIdContext);
  const { deleteEffectInstance, isPending } = useDeleteEffectInstance(() => {},
  itemId as number);
  return (
    <Table.Row>
      <Cell>{effect.name}</Cell>

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
            value={{ objectId: itemId, objectType: "Item" }}
          >
            <EffectInstanceForm effectId={effect.id}></EffectInstanceForm>
          </EffectParentObjectIdContext.Provider>
        </Modal.Window>
      </Modal>
    </Table.Row>
  );
}
