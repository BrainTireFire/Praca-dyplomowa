import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import { CharacterClass } from "../../../models/characterclass";
import ClassRow from "./ClassRow";

const characterClasses = [
  {
    id: 1,
    name: "Fighter",
    level: 1,
  },
  {
    id: 2,
    name: "Paladin",
    level: 2,
  },
];

export default function ClassTable({
  characterClasses,
}: {
  characterClasses: CharacterClass[];
}) {
  return (
    <Menus>
      <Table header="Class" button="Level up" columns="1fr 1fr">
        <Table.Header>
          <div>Name</div>
          <div>Level</div>
        </Table.Header>
        <Table.Body
          data={characterClasses}
          render={(characterClass) => (
            <ClassRow key={characterClass.id} characterClass={characterClass} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}