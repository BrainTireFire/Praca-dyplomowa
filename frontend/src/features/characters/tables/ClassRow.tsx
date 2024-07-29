import React from "react";
import styled from "styled-components";
import Table from "../../../ui/containers/Table";

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-secondary-text);
`;

export default function ClassRow({ characterClass }) {
  return (
    <Table.Row>
      <Cell>{characterClass.name}</Cell>

      <Cell>{characterClass.level}</Cell>
    </Table.Row>
  );
}
