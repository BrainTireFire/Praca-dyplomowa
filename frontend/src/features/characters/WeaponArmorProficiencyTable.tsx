import React from "react";
import Menus from "../../ui/containers/Menus";
import Table from "../../ui/containers/Table";
import EquipmentRow from "./EquipmentRow";
import styled from "styled-components";
import ProficiencyRow from "./ProficiencyRow";

const tools = [
  {
    id: 1,
    Name: "Longsword",
  },
  {
    id: 2,
    Name: "Shields",
  },
  {
    id: 3,
    Name: "Heavy Armor",
  },
];

export default function ToolProficiencyTable() {
  return (
    <Menus>
      <Table
        header="Weapon proficiency"
        button="Choices available"
        columns="1fr 3.2rem"
      >
        <Table.Body
          data={tools}
          render={(tool) => <ProficiencyRow key={tool.id} item={tool} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}
