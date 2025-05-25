import { useContext } from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";
import { Resource } from "../../../models/resource";
import { ParentObjectIdContext } from "../../../context/ParentObjectIdContext";
import { CharacterIdContext } from "../contexts/CharacterIdContext";
import { ResourceSelectionForm } from "../../immaterialResources/ResourceSelectionForm";

export default function ResourceTable({
  resources,
}: {
  resources: Resource[];
}) {
  const { characterId } = useContext(CharacterIdContext);
  return (
    <Menus>
      <Table
        header="Resources"
        button="Select"
        columns="auto auto auto auto auto"
        modal={
          <ParentObjectIdContext.Provider
            value={{ objectId: characterId, objectType: "Character" }}
          >
            <ResourceSelectionForm />
          </ParentObjectIdContext.Provider>
        }
      >
        <Table.Header>
          <Cell>Name</Cell>
          <Cell>Level</Cell>
          <Cell>Count</Cell>
          <Cell>Source</Cell>
          <Cell>Refresh</Cell>
        </Table.Header>
        <Table.Body
          data={resources}
          columnCount={5}
          render={(resource) => (
            <ResourceRow key={resource.id} resource={resource} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function ResourceRow({ resource }: { resource: Resource }) {
  return (
    <Table.Row>
      <Cell>{resource.name}</Cell>
      <Cell>{resource.level}</Cell>

      <Cell>{`${resource.left}/${resource.total}`}</Cell>

      <Cell>{resource.source}</Cell>
      <Cell>{resource.refresh}</Cell>
    </Table.Row>
  );
}
