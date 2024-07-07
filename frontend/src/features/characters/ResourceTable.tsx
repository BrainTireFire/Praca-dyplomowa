import React from "react";
import Menus from "../../ui/containers/Menus";
import Table from "../../ui/containers/Table";
import EquipmentRow from "./EquipmentRow";
import styled from "styled-components";
import Modal from "../../ui/containers/Modal";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";
import RadioButton from "../../ui/containers/RadioButton";

const resources = [
  {
    id: 1,
    name: "Resource 1",
    count: "1/2",
    source: "Class",
    refresh: "Short rest",
  },
  {
    id: 2,
    name: "Resource 2",
    count: "3/4",
    source: "Race",
    refresh: "Long rest",
  },
  {
    id: 3,
    name: "Resource 3",
    count: "4/4",
    source: "Item",
    refresh: "Long rest",
  },
];

export default function ResourceTable() {
  return (
    <Menus>
      <Table
        header="Constant effects"
        button="Add new"
        columns="1fr 1fr 1fr 1fr 3.2rem"
      >
        <Table.Header>
          <div>Name</div>
          <div>Count</div>
          <div>Source</div>
          <div>Refresh</div>
        </Table.Header>
        <Table.Body
          data={resources}
          render={(resource) => (
            <ResourceRow key={resource.id} resource={resource} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

const Cell = styled.div`
  font-size: 1.2rem;
  font-weight: 600;
  color: var(--color-grey-600);
`;

function ResourceRow({ resource }) {
  return (
    <Table.Row>
      <Cell>{resource.name}</Cell>

      <Cell>{resource.count}</Cell>

      <Cell>{resource.source}</Cell>
      <Cell>{resource.refresh}</Cell>

      <Modal>
        <Menus.Menu>
          <Menus.Toggle id={resource.id} />
          <Menus.List id={resource.id}>
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
