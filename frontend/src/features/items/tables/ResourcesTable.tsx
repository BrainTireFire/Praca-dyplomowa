import { useContext } from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import { ItemIdContext } from "../contexts/ItemIdContext";
import { ImmaterialResourceAmount } from "../../../models/immaterialResourceAmount";
import { ParentObjectIdContext } from "../../../context/ParentObjectIdContext";
import { ResourceSelectionForm } from "../../immaterialResources/ResourceSelectionForm";

export default function ResourcesTable({
  resources,
}: {
  resources: ImmaterialResourceAmount[];
}) {
  const { itemId } = useContext(ItemIdContext);
  return (
    <Menus>
      <Table
        header="Resources available when equipped"
        button="Select"
        columns="1fr 1fr 1fr"
        modal={
          <ParentObjectIdContext.Provider
            value={{ objectId: itemId, objectType: "Item" }}
          >
            <ResourceSelectionForm />
          </ParentObjectIdContext.Provider>
        }
      >
        <Table.Header>
          <div>Name</div>
          <div>Level</div>
          <div>Number</div>
        </Table.Header>
        <Table.Body
          data={resources}
          render={(resource: ImmaterialResourceAmount) => (
            <ResourceRow key={resource.blueprintId} resource={resource} />
          )}
          columnCount={3}
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

function ResourceRow({ resource }: { resource: ImmaterialResourceAmount }) {
  return (
    <Table.Row>
      <Cell>{resource.name}</Cell>
      <Cell>{resource.level}</Cell>
      <Cell>{resource.count}</Cell>
    </Table.Row>
  );
}
