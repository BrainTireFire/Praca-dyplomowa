import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";
import { Power } from "../../../models/power";
import { PowerSelectionForm } from "../../powers/PowerSelectionForm";
import { ParentObjectIdContext } from "../../../context/ParentObjectIdContext";
import { CharacterIdContext } from "../contexts/CharacterIdContext";
import { useContext } from "react";

export default function PowersTable({ powers }: { powers: Power[] }) {
  const { characterId } = useContext(CharacterIdContext);
  return (
    <Menus>
      <Table
        header="Powers known"
        button="Select custom"
        columns="1fr 1fr"
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
        </Table.Header>
        <Table.Body
          columnCount={2}
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
    </Table.Row>
  );
}
