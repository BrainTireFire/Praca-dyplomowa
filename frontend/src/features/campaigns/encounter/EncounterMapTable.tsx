import React, { useState } from "react";
import Menus from "../../../ui/containers/Menus";
import EncounterMapRow from "./EncounterMapRow";
import { Cell } from "../../../ui/containers/Cell";
import { useBoards } from "../../homebrew/maps/useBoards";
import Spinner from "../../../ui/interactive/Spinner";
import { Board } from "../../../models/map/Board";
import styled from "styled-components";
import { TableNormal } from "../../../ui/containers/TableNormal";

export default function EncounterMapTable() {
  const { isLoading, boards } = useBoards();
  const [selected, setSelected] = useState(null);

  if (isLoading) {
    return <Spinner />;
  }

  if (!boards || boards.length === 0) {
    return <div>No maps available.</div>;
  }

  console.log(boards);

  return (
    <div>
      <TableNormal
        data={boards}
        selected={selected}
        setSelected={setSelected}
      />
    </div>
  );
}
