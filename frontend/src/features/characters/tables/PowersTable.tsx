import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import styled from "styled-components";
import { Cell } from "../../../ui/containers/Cell";
import Modal from "../../../ui/containers/Modal";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import { Power } from "../../../models/power";

const powers = [
  {
    id: 1,
    name: "Power 1",
    source: "Race",
    favourite: true,
  },
  {
    id: 2,
    name: "Power 2",
    source: "Class",
    favourite: false,
  },
  {
    id: 3,
    name: "Power 3",
    source: "Magic sock",
    favourite: true,
  },
];

export default function PowersTable({ powers }: { powers: Power[] }) {
  return (
    <Menus>
      <Table header="Powers" button="Add new" columns="1fr 1fr 3.2rem">
        <Table.Header>
          <div>Name</div>
          <div>Source</div>
          <div>*</div>
        </Table.Header>
        <Table.Body
          columnCount={3}
          data={powers}
          render={(power) => <PowersRow key={power.id} power={power} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function PowersRow({ power }: { power: Power }) {
  return (
    <Table.Row>
      <Cell>{power.name}</Cell>

      <Cell>{power.source.join(", ")}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={power.id} />
          <Menus.List id={power.id}>
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
