import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import Modal from "../../../ui/containers/Modal";
import { HiEye, HiTrash } from "react-icons/hi2";
import { Effect } from "../../../models/effect";
import { Cell } from "../../../ui/containers/Cell";
import ConfirmDelete from "../../../ui/containers/ConfirmDelete";
import { CharacterIdContext } from "../contexts/CharacterIdContext";
import { useContext } from "react";
import EffectInstanceForm from "../../effects/EffectInstanceForm";
import { EffectParentObjectIdContext } from "../../../context/EffectParentObjectIdContext";
import { EditModeContext } from "../../../context/EditModeContext";
import { useUnlinkConstantEffectInstance } from "../hooks/useUnlinkConstantEffectInstance";
import styled from "styled-components";
import { ItemContext } from "../../../context/ItemContext";

export default function ConstantEffectTable({
  effects,
}: {
  effects: Effect[];
}) {
  const { characterId } = useContext(CharacterIdContext);
  return (
    <Menus>
      <Table
        header="Constant effects"
        button="Add new"
        columns="auto auto auto 0.01rem"
        // buttonOnClick={() => createConstantEffectInstance(initialState)}
        modal={
          <EffectParentObjectIdContext.Provider
            value={{ objectId: characterId, objectType: "CharacterConstant" }}
          >
            <Container>
              <EffectInstanceForm effectId={null}></EffectInstanceForm>
            </Container>
          </EffectParentObjectIdContext.Provider>
        }
      >
        <Table.Header>
          <Cell>Name</Cell>
          <Cell>Source</Cell>
          <Cell>Target</Cell>
          <Cell></Cell>
        </Table.Header>
        <Table.Body
          data={effects}
          columnCount={4}
          render={(effect) => (
            <ConstantEffectRow key={effect.id} effect={effect} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function ConstantEffectRow({ effect }: { effect: Effect }) {
  const { characterId } = useContext(CharacterIdContext);
  const { editMode } = useContext(EditModeContext);
  const { unlinkEffectInstance, isPending } = useUnlinkConstantEffectInstance(
    () => {},
    characterId as number
  );
  return (
    <Table.Row>
      <Cell>{effect.name}</Cell>

      <Cell>{effect.source}</Cell>
      <Cell>{effect.target}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={effect.id} itemCount={2}/>
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
              unlinkEffectInstance(effect.id);
            }}
          />
        </Modal.Window>
        <Modal.Window name="open">
          <EffectParentObjectIdContext.Provider
            value={{ objectId: characterId, objectType: effect.affectsCharacter ? "CharacterConstant" : "ItemItself" }}
          >
            <ItemContext.Provider value={{objectType: effect.affectsCharacter ? "notapplies" : "Weapon"}}>
              <Container>
                <EffectInstanceForm effectId={effect.id}></EffectInstanceForm>
              </Container>
            </ItemContext.Provider>
          </EffectParentObjectIdContext.Provider>
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