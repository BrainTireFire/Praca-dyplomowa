import React from "react";
import Table from "../../ui/containers/Table";
import Modal from "../../ui/containers/Modal";
import Menus from "../../ui/containers/Menus";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import styled from "styled-components";
import RadioButton from "../../ui/containers/RadioButton";

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-grey-600);
`;

export default function ClassRow({ characterClass }) {
  return (
    <Table.Row>
      <Cell>{characterClass.name}</Cell>

      <Cell>{characterClass.level}</Cell>
    </Table.Row>
  );
}
