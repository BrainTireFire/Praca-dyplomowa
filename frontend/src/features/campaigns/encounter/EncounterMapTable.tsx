import React from "react";
import Menus from "../../../ui/containers/Menus";
import Table from "../../../ui/containers/Table";
import EncounterMapRow from "./EncounterMapRow";
import { Cell } from "../../../ui/containers/Cell";
import { useBoards } from "../../homebrew/maps/useBoards";
import Spinner from "../../../ui/interactive/Spinner";

export default function EncounterMapTable() {
  const { isLoading, boards } = useBoards();

  if (isLoading) {
    return <Spinner />;
  }

  if (!boards || boards.length === 0) {
    return <div>No maps available.</div>;
  }

  return (
    <Menus>
      <Table header="Maps" button="Add new" columns="1fr 1fr 0.01rem">
        <Table.Header>
          <Cell>Name</Cell>
          <Cell>Size</Cell>
          <Cell></Cell>
        </Table.Header>
        <Table.Body
          data={boards}
          columnCount={3}
          render={(board) => <EncounterMapRow key={board.id} board={board} />}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}
