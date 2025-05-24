import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import Modal from "../../../ui/containers/Modal";
import { HiEye, HiTrash } from "react-icons/hi2";
import { Cell } from "../../../ui/containers/Cell";
import ConfirmDelete from "../../../ui/containers/ConfirmDelete";
import { useContext } from "react";
import { ItemIdContext } from "../contexts/ItemIdContext";
import { useDeleteEffectInstance } from "../hooks/useDeleteEffectInstance";
import { EffectBlueprintListItem } from "../models/effectBlueprint";
import EffectInstanceForm from "../../effects/EffectInstanceForm";
import { EffectParentObjectIdContext } from "../../../context/EffectParentObjectIdContext";
import { EditModeContext } from "../../../context/EditModeContext";
import styled from "styled-components";

export default function EffectTable({
  effects,
}: {
  effects: EffectBlueprintListItem[];
}) {
  const { itemId } = useContext(ItemIdContext);
  return (
    <Menus>
      <Table
        header="Effects on wearer"
        button="Add new"
        columns="1fr 0.01rem"
        // buttonOnClick={() => createEffectInstance(initialState)}
        modal={
          <EffectParentObjectIdContext.Provider
            value={{ objectId: itemId, objectType: "ItemWearer" }}
          >
            <Container>
              <EffectInstanceForm effectId={null}></EffectInstanceForm>
            </Container>
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
  const { editMode } = useContext(EditModeContext);
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
            {editMode && (
              <Modal.Open opens="delete">
                <Menus.Button icon={<HiTrash />} onClick={() => {}}>
                  Delete
                </Menus.Button>
              </Modal.Open>
            )}
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
            value={{ objectId: itemId, objectType: "ItemWearer" }}
          >
            <Container>
              <EffectInstanceForm effectId={effect.id}></EffectInstanceForm>
            </Container>
          </EffectParentObjectIdContext.Provider>
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