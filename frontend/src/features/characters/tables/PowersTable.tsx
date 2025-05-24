import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";
import { Power } from "../../../models/power";
import { PowerSelectionForm } from "../../powers/PowerSelectionForm";
import { ParentObjectIdContext } from "../../../context/ParentObjectIdContext";
import { CharacterIdContext } from "../contexts/CharacterIdContext";
import { useContext } from "react";
import Modal from "../../../ui/containers/Modal";
import PowerForm from "../../powers/PowerForm";
import { HiEye } from "react-icons/hi2";

export default function PowersTable({ powers }: { powers: Power[] }) {
  const { characterId } = useContext(CharacterIdContext);
  return (
    <Menus>
      <Table
        header="Powers known"
        button="Select custom"
        columns="auto auto 0.01rem"
        modal={
          <ParentObjectIdContext.Provider
            value={{ objectId: characterId, objectType: "Character" }}
          >
            <PowerSelectionForm />
          </ParentObjectIdContext.Provider>
        }
      >
        <Table.Header>
          <div>Name</div>
          <div>Source</div>
          <div></div>
        </Table.Header>
        <Table.Body
          columnCount={3}
          data={powers}
          render={(power) => <PowersRow key={power.id} power={power} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function PowersRow({ power }: { power: Power }) {
  return (
    <Table.Row>
      <Cell>{power.name}</Cell>

      <Cell>{power.source.join(", ")}</Cell>
      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={power.id} />
          <Menus.List id={power.id}>
            <Modal.Open opens="open">
              <Menus.Button icon={<HiEye />} onClick={() => {}}>
                Open
              </Menus.Button>
            </Modal.Open>
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="open">
          <PowerForm powerId={power.id}></PowerForm>
        </Modal.Window>
      </Modal>
    </Table.Row>
  );
}
