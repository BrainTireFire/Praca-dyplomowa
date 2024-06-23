import React from "react";
import Menus from "../../ui/containers/Menus";
import Table from "../../ui/containers/Table";
import EquipmentRow from "./EquipmentRow";
import styled from "styled-components";
import LanguageRow from "./LanguageRow";

const languages = [
  {
    id: 1,
    Name: "Dwarvish",
  },
  {
    id: 2,
    Name: "Elvish",
  },
  {
    id: 3,
    Name: "Common",
  },
];

export default function KnownLanguagesTable() {
  return (
    <Menus>
      <Table
        header="Known languages"
        button="Choices available"
        columns="1fr 3.2rem"
      >
        <Table.Body
          data={languages}
          render={(language) => (
            <LanguageRow key={language.id} language={language} />
          )}
        />
        <Table.Footer>{/* <Pagination count={count} /> */}</Table.Footer>
      </Table>
    </Menus>
  );
}
