import React from "react";
import styled from "styled-components";
import Table from "../../../ui/containers/Table";
import { Cell } from "../../../ui/containers/Cell";
import { CharacterClass } from "../../../models/characterclass";
import { DiceSetString } from "../../../models/diceset";

export default function ClassRow({ characterClass } : {characterClass: CharacterClass}) {
  return (
    <Table.Row>
      <Cell>{characterClass.name}</Cell>

      <Cell>{characterClass.level}</Cell>
      <Cell>{characterClass.mainAbility ?? '-'}</Cell>
      <Cell>{characterClass.difficultyClass ?? '-'}</Cell>
      <Cell>{characterClass.attackBonus ?? '-'}</Cell>
    </Table.Row>
  );
}
