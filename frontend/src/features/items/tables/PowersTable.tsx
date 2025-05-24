import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import { PowerListItem } from "../../../models/power";
import { PowerSelectionFormItem } from "./PowerSelectionFormItem";
import { ParentObjectIdContext } from "../../../context/ParentObjectIdContext";
import { ItemIdContext } from "../contexts/ItemIdContext";
import { useContext } from "react";
import Modal from "../../../ui/containers/Modal";
import PowerForm from "../../powers/PowerForm";
import { HiEye } from "react-icons/hi2";

export default function PowersTable({ powers }: { powers: PowerListItem[] }) {
  const { itemId } = useContext(ItemIdContext);
  return (
    <Menus>
      <Table
        header="Powers available when equipped"
        button="Select"
        columns="1fr 0.01rem"
        modal={
          <ParentObjectIdContext.Provider
            value={{ objectId: itemId, objectType: "Item" }}
          >
            <PowerSelectionFormItem />
          </ParentObjectIdContext.Provider>
        }
      >
        <Table.Header>
          <div>Name</div>
          <div></div>
        </Table.Header>
        <Table.Body
          data={powers}
          render={(power) => <PowerRow key={power.id} power={power} />}
          columnCount={2}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-secondary-text);
`;

function PowerRow({ power }: { power: PowerListItem }) {
  return (
    <Table.Row>
      <Cell>{power.name}</Cell>
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
