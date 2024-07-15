import React from "react";
import Menus from "../../ui/containers/Menus";
import Table from "../../ui/containers/Table";
import EquipmentRow from "./EquipmentRow";
import styled from "styled-components";
import ProficiencyRow from "./ProficiencyRow";
import { ItemFamily } from "../../models/itemfamily";

export default function ToolProficiencyTable({
  toolFamilies,
}: {
  toolFamilies: ItemFamily[];
}) {
  return (
    <Menus>
      <Table
        header="Tool proficiency"
        button="Choices available"
        columns="1fr 3.2rem"
      >
        <Table.Body
          data={toolFamilies}
          render={(tool) => <ProficiencyRow key={tool.id} item={tool} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}
