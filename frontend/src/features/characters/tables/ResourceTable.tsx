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
import RadioButton from "../../../ui/containers/RadioButton";
import { Resource } from "../../../models/resource";

export default function ResourceTable({
  resources,
}: {
  resources: Resource[];
}) {
  return (
    <Menus>
      <Table
        header="Resources"
        button="Add new"
        columns="1fr 1fr 1fr 1fr 3.2rem"
      >
        <Table.Header>
          <Cell>Name</Cell>
          <Cell>Count</Cell>
          <Cell>Source</Cell>
          <Cell>Refresh</Cell>
          <Cell></Cell>
        </Table.Header>
        <Table.Body
          data={resources}
          columnCount={6}
          render={(resource) => (
            <ResourceRow key={resource.id} resource={resource} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}

function ResourceRow({ resource }: { resource: Resource }) {
  return (
    <Table.Row>
      <Cell>{resource.name}</Cell>

      <Cell>{`${resource.left}/${resource.total}`}</Cell>

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
