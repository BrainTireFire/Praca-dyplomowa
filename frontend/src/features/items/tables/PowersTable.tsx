import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import { PowerListItem } from "../../../models/power";
import { PowerSelectionFormItem } from "./PowerSelectionFormItem";
import { ParentObjectIdContext } from "../../../context/ParentObjectIdContext";
import { ItemIdContext } from "../contexts/ItemIdContext";
import { useContext } from "react";

export default function PowersTable({ powers }: { powers: PowerListItem[] }) {
  const { itemId } = useContext(ItemIdContext);
  return (
    <Menus>
      <Table
        header="Powers available when equipped"
        button="Add new"
        columns="1fr"
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
        </Table.Header>
        <Table.Body
          data={powers}
          render={(power) => <PowerRow key={power.id} power={power} />}
          columnCount={1}
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
    </Table.Row>
  );
}
