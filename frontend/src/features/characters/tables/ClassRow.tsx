import React from "react";
import styled from "styled-components";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";

export default function ClassRow({ characterClass }) {
  return (
    <Table.Row>
      <Cell>{characterClass.name}</Cell>

      <Cell>{characterClass.level}</Cell>
    </Table.Row>
  );
}
