import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import Modal from "../../../ui/containers/Modal";
import { HiEye, HiTrash } from "react-icons/hi2";
import { Cell } from "../../../ui/containers/Cell";
import RadioButton from "../../../ui/containers/RadioButton";
import { EffectBlueprintListItem } from "../models/effectBlueprint";
import { useEffectBlueprint } from "../hooks/useEffectBlueprint";
import EffectBlueprintForm, {
  EffectBlueprint,
  initialState,
} from "../../effects/EffectBlueprintForm";
import Spinner from "../../../ui/interactive/Spinner";
import { useCreateEffectBlueprint } from "../hooks/useCreateEffectBlueprint";

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
    <Menus>
      <Table
        header="Effects"
        button="Add new"
        columns="1fr 1fr 1fr 0.01rem"
        buttonOnClick={() => createEffectBlueprint(initialState)}
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
  );
}

function EffectRow({ effect }: { effect: EffectBlueprintListItem }) {
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
              <Menus.Button icon={<HiEye />}>Open</Menus.Button>
            </Modal.Open>
            <Modal.Open opens="delete">
              <Menus.Button icon={<HiTrash />}>Delete</Menus.Button>
            </Modal.Open>
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="delete">
          {/* <ConfirmDelete
            resourceName="equipment"
            disabled={isDeleting}
            onConfirm={() => {
              deleteBooking(bookingId);
            }}
          /> */}
        </Modal.Window>
        <Modal.Window name="open">
          <EffectBlueprintFormContainer
            effect={effect}
          ></EffectBlueprintFormContainer>
        </Modal.Window>
      </Modal>
    </Table.Row>
  );

  function EffectBlueprintFormContainer({
    effect,
  }: {
    effect: EffectBlueprintListItem;
  }) {
    const { isLoading, effectBlueprint, error } = useEffectBlueprint(effect.id);
    if (isLoading) {
      return <Spinner></Spinner>;
    }
    if (error) {
      console.log(error);
      return <>Error</>;
    }
    console.log(effectBlueprint);
    return (
      <EffectBlueprintForm
        effectBlueprint={effectBlueprint as EffectBlueprint}
      ></EffectBlueprintForm>
    );
  }
}
