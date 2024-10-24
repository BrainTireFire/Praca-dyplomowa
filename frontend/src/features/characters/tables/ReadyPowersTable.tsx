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

export default function ReadyPowerTable({ powers }: { powers: Power[] }) {
  return (
    <Menus>
      <Table header="Ready powers" button="Add new" columns="1fr 3.2rem">
        <Table.Header>
          <div>Name</div>
        </Table.Header>
        <Table.Body
          data={powers}
          render={(power) => <ReadyPowersRow key={power.id} power={power} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function ReadyPowersRow({ power }: { power: Power }) {
  return (
    <Table.Row>
      <Cell>{power.name}</Cell>

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
