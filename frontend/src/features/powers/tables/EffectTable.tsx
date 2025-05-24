import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import Modal from "../../../ui/containers/Modal";
import { HiEye, HiTrash } from "react-icons/hi2";
import { Cell } from "../../../ui/containers/Cell";
import RadioButton from "../../../ui/containers/RadioButton";
import { EffectBlueprintListItem } from "../models/effectBlueprint";
import EffectBlueprintForm, {
  initialState,
} from "../../effects/EffectBlueprintForm";
import Spinner from "../../../ui/interactive/Spinner";
import { useCreateEffectBlueprint } from "../hooks/useCreateEffectBlueprint";
import { useDeleteEffectBlueprint } from "../hooks/useDeleteEffectBlueprint";
import ConfirmDelete from "../../../ui/containers/ConfirmDelete";
import { useContext } from "react";
import { PowerIdContext } from "../contexts/PowerIdContext";
import { EditModeContext } from "../../../context/EditModeContext";

export default function EffectTable({
  effects,
  powerId,
}: {
  effects: EffectBlueprintListItem[];
  powerId: number;
}) {
  const { createEffectBlueprint, isPending } = useCreateEffectBlueprint(
    () => {},
    powerId
  );
  if (isPending) {
    return <Spinner></Spinner>;
  }
  return (
    <PowerIdContext.Provider
      value={{
        powerId: powerId,
      }}
    >
      <Menus>
        <Table
          header="Effects"
          button="Add new"
          columns="auto auto auto 0.01rem"
          // buttonOnClick={() => createEffectBlueprint(initialState)}
          modal={<EffectBlueprintForm effectId={null}></EffectBlueprintForm>}
        >
          <Table.Header>
            <div>Saved</div>
            <div>Level</div>
            <div>Name</div>
            <div></div>
          </Table.Header>
          <Table.Body
            data={effects}
            columnCount={4}
            render={(effect) => <EffectRow key={effect.id} effect={effect} />}
          />
          <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
        </Table>
      </Menus>
    </PowerIdContext.Provider>
  );
}

function EffectRow({ effect }: { effect: EffectBlueprintListItem }) {
  const { powerId } = useContext(PowerIdContext);
  const { editMode } = useContext(EditModeContext);
  const { deleteEffectBlueprint, isPending } = useDeleteEffectBlueprint(
    () => {},
    powerId
  );
  return (
    <Table.Row>
      <RadioButton checked={effect.savingThrowSuccess} />
      <Cell>{effect.resourceLevel}</Cell>
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
            { editMode && 
              <Modal.Open opens="delete">
                <Menus.Button icon={<HiTrash />} onClick={() => {}}>
                  Delete
                </Menus.Button>
              </Modal.Open>
            }
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="delete">
          <ConfirmDelete
            resourceName="effect blueprint"
            disabled={isPending}
            onConfirm={() => {
              deleteEffectBlueprint(effect.id);
            }}
          />
        </Modal.Window>
        <Modal.Window name="open">
          <EffectBlueprintForm effectId={effect.id}></EffectBlueprintForm>
        </Modal.Window>
      </Modal>
    </Table.Row>
  );
}
