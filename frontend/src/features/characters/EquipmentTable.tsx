import React from "react";
import Menus from "../../ui/containers/Menus";
import Table from "../../ui/containers/Table";
import EquipmentRow from "./EquipmentRow";
import styled from "styled-components";

const equipments = [
  {
    id: 1,
    name: "Equipment 1",
    description: "Description for Equipment 1",
    otherField: "Other data for Equipment 1",
    gwiazda: "*",
  },
  {
    id: 2,
    name: "Equipment 2",
    description: "Description for Equipment 2",
    otherField: "Other data for Equipment 2",
    gwiazda: "*",
  },
  {
    id: 3,
    name: "Equipment 3",
    description: "Description for Equipment 3",
    otherField: "Other data for Equipment 3",
    gwiazda: "*",
  },
];

export default function EquipmentTable() {
  return (
    <Menus>
      <Table
        header="My Table Header"
        button="Add new"
        columns="1fr 1fr 1fr 1fr 3.2rem"
      >
        <Table.Header>
          <div>Name</div>
          <div>Slot</div>
          <div>Equipped</div>
          <div>Gwizdka</div>
          <div></div>
        </Table.Header>
        <Table.Body
          data={equipments}
          render={(equipment) => (
            <EquipmentRow key={equipment.id} equipment={equipment} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}