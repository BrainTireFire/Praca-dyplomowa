import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";
import { Power } from "../../../models/power";
import { PreparedPowerSelectionForm } from "./PreparedPowerSelectionForm";
import { ClassPowersToPrepareTabs } from "../ClassPowersToPrepareTabs";
import Modal from "../../../ui/containers/Modal";
import PowerForm from "../../powers/PowerForm";
import { HiEye } from "react-icons/hi2";
import { DiceSetString } from "../../../models/diceset";

export default function ReadyPowerTable({ powers }: { powers: Power[] }) {
  return (
    <Menus>
      <Table
        header="Ready powers"
        button="Select"
        columns="auto auto 0.01rem"
        modal={<ClassPowersToPrepareTabs />}
      >
        <Table.Header>
          <div>Name</div>
          <div>Difficulty class / Attack bonus</div>
          <div></div>
        </Table.Header>
        <Table.Body
          data={powers}
          render={(power) => <ReadyPowersRow key={power.id} power={power} />}
          columnCount={3}
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
      <Cell>{!!power.difficultyClass ? "DC" + power.difficultyClass : !!power.attackBonus ? DiceSetString(power.attackBonus) : "-"}</Cell>
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
