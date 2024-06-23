import React from "react";
import Menus from "../../ui/containers/Menus";
import Table from "../../ui/containers/Table";
import EquipmentRow from "./EquipmentRow";
import styled from "styled-components";
import ToolProficiencyRow from "./ToolProficiencyRow";

const tools = [
  {
    id: 1,
    Name: "Tool 1",
  },
  {
    id: 2,
    Name: "Tool 2",
  },
  {
    id: 3,
    Name: "Tool 3",
  },
];

export default function ToolProficiencyTable() {
  return (
    <Menus>
      <Table
        header="Tool proficiency"
        button="Choices available"
        columns="1fr 3.2rem"
      >
        <Table.Body
          data={tools}
          render={(tool) => <ToolProficiencyRow key={tool.id} tool={tool} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}
