import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import Modal from "../../../ui/containers/Modal";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import { Effect } from "../../../models/effect";

export default function EffectTable({ effects }: { effects: Effect[] }) {
  return (
    <Menus>
      <Table
        header="Temporary effects"
        button="Add new"
        columns="1fr 1fr 1fr 1fr 0.01rem"
      >
        <Table.Header>
          <div>Name</div>
          <div>Time left</div>
          <div>Source</div>
          <div>Target</div>
          <div></div>
        </Table.Header>
        <Table.Body
          data={effects}
          columnCount={5}
          render={(effect) => <EffectRow key={effect.id} effect={effect} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-secondary-text);
`;

function EffectRow({ effect }: { effect: Effect }) {
  return (
    <Table.Row>
      <Cell>{effect.name}</Cell>

      <Cell>{effect.turnsLeft}</Cell>

      <Cell>{effect.source}</Cell>
      <Cell>{effect.target}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={effect.id} />
          <Menus.List id={effect.id}>
            <Menus.Button icon={<HiEye />} onClick={() => alert("Test")}>
              Test 1
            </Menus.Button>

            <Menus.Button
              icon={<HiArrowDownOnSquare />}
              onClick={() => alert("Test")}
            >
              Test 2
            </Menus.Button>

            <Menus.Button
              icon={<HiArrowUpOnSquare />}
              onClick={() => alert("Test")}
            >
              Test 3
            </Menus.Button>

            <Modal.Open opens="delete">
              <Menus.Button icon={<HiTrash />}>Test 4</Menus.Button>
            </Modal.Open>
          </Menus.List>
        </Menus.Menu>
        <Modal.Window name="delete">
          {/* <ConfirmDelete
            resourceName="equipment"
            disabled={isDeleting}
            onConfirm={() => {
              deleteBooking(bookingId);
            }}
          /> */}
        </Modal.Window>
      </Modal>
    </Table.Row>
  );
}
