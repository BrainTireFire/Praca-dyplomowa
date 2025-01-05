import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";
import { Power } from "../../../models/power";
import { PreparedPowerSelectionForm } from "./PreparedPowerSelectionForm";
import { ClassPowersToPrepareTabs } from "../ClassPowersToPrepareTabs";

export default function ReadyPowerTable({ powers }: { powers: Power[] }) {
  return (
    <Menus>
      <Table
        header="Ready powers"
        button="Select"
        columns="1fr"
        modal={<ClassPowersToPrepareTabs />}
      >
        <Table.Header>
          <div>Name</div>
        </Table.Header>
        <Table.Body
          data={powers}
          render={(power) => <ReadyPowersRow key={power.id} power={power} />}
          columnCount={1}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function ReadyPowersRow({ power }: { power: Power }) {
  return (
    <Table.Row>
      <Cell>{power.name}</Cell>
    </Table.Row>
  );
}
