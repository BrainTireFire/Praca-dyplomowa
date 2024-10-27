import React, { useState } from "react";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";
import Modal from "../../../ui/containers/Modal";
import Menus from "../../../ui/containers/Menus";
import { Board } from "../../../models/map/Board";
import {
  HiArrowDownOnSquare,
  HiArrowUpOnSquare,
  HiEye,
  HiTrash,
} from "react-icons/hi2";

type BoardProps = {
  board: Board;
};

export default function EncounterMapRow({ board }: BoardProps) {
  const [pressed, setPressed] = useState<boolean>(false);

  function handleClick() {
    console.log("Clicked on board: ", board);
    setPressed(!pressed); // Toggle pressed state on click
  }

  return (
    <Table.Row onClick={handleClick} pressed={pressed}>
      <Cell>{board.name}</Cell>
      <Cell>{board.sizeX + " x " + board.sizeY}</Cell>
      <Cell></Cell>
    </Table.Row>
  );
}
